using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Mapping.Dtos
{
    public class UserDto
    {
        public PlayerDto User { get; set; }
        public string Token { get; set; }
    }
}
