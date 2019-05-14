using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wanaheim.Core;
using Wanaheim.Core.Domain;
using Wanaheim.Core.Repository;
using Wanaheim.Mapping.Dtos;
using Wanaheim.Services.Interfaces;
using Wanaheim.Services.Settings;

namespace Wanaheim.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPlayerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtFactoryService _jwtFactory;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthorizationService(UserManager<AppUser> userManager,
            IMapper mapper, IPlayerRepository repository, IUnitOfWork unitOfWork,
            IJwtFactoryService jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
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

        public async Task<UserDto> GetAuthorizedPlayerAsync(UserCredentialsDto credentials)
        {
            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);
            if (identity == null)
            {
                return null;
            }

            string id = identity.Claims.Single(c => c.Type == "id").Value;

            var response = new UserDto
            {
                User = _mapper.Map<Player, PlayerDto>(await _repository.Get(p => p.UserId == id)),
                Token = await _jwtFactory.GenerateEncodedToken(credentials.Email, identity)
            };

            return response;
        }

        public async Task<Player> SignUpPlayer(SignUpDto credentials)
        {
            var userIdentity = _mapper.Map<AppUser>(credentials);
            var result = await _userManager.CreateAsync(userIdentity, credentials.Password);

            if (!result.Succeeded)
            {
                return null;
            }

            var player = new Player
            {
                Name = credentials.Name,
                UserId = userIdentity.Id
            };

            _repository.Add(player);
            await _unitOfWork.Complete();

            return player;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null)
                {
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
                    }
                }
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
