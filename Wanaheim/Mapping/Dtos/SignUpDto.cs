using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wanaheim.Mapping.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must consist of at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Insert correct email")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must consist of at least 3 characters")]
        public string Name { get; set; }
    }
}
