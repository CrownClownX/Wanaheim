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
using Wanaheim.Mapping.Dtos;
using Wanaheim.Services.Interfaces;

namespace Wanaheim.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthorizationService _authorization;

        public AuthenticationController(IAuthorizationService authorization)
        {
            _authorization = authorization;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Post([FromBody]SignUpDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var player = await _authorization.SignUpPlayer(model);

            if (player == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(player);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]UserCredentialsDto credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authorization.GetAuthorizedPlayerAsync(credentials);

            if (response == null)
            {
                return BadRequest("There is no such user or credentials are invalid");
            }

            return Ok(response);
        }
    }
}