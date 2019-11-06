using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models
{
    public class MovieActor
    {
        [Key]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        [Key]
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}

