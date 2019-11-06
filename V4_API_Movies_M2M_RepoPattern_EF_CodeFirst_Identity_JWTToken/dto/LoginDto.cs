using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]

        public String Email { get; set; }

        [Required]
        public String Password { get; set; }
    }
}
