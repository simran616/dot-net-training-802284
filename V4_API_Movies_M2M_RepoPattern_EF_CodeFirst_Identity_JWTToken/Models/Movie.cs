using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Release Date")]
        [DateValidator]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "rating should between 1 and 10")]
        public decimal Rating { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; }
    }
}
