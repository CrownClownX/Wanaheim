using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wanaheim.Core.Domain;
using Wanaheim.Mapping.Dtos;

namespace Wanaheim.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<Player> SignUpPlayer(SignUpDto credentials);
        Task<UserDto> GetAuthorizedPlayerAsync(UserCredentialsDto credentials);
    }
}
