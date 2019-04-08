using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Wanaheim.Core;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Helpers.Interfaces;
using Wanaheim.Mapping.Dtos;

namespace Wanaheim.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtFactory _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthenticationController(UserManager<AppUser> userManager,
            IMapper mapper, IPlayerRepository repository, IUnitOfWork unitOfWork,
            IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Post([FromBody]SignUpDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(ModelState);
            }

            var player = new Player
            {
                Name = model.Name,
                UserId = userIdentity.Id
            };

            _repository.Add(player);
            await _unitOfWork.Complete();

            return Ok(player);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]UserCredentialsDto credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
            {
                return BadRequest("There is no such user or credentials are invalid");
            }

            string id = identity.Claims.Single(c => c.Type == "id").Value;

            var response = new UserDto
            {
                User = _mapper.Map<Player, PlayerDto>(await _repository.Get(p => p.UserId == id)),
                Token = await _jwtFactory.GenerateEncodedToken(credentials.Email, identity)
            };

            //var json = JsonConvert.SerializeObject(response, _serializerSettings);
            // return new OkObjectResult(json);
            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}