using System.ComponentModel.DataAnnotations;

namespace MoviesACLabs.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; }

        [Range(1,5)]
        public int Rating { get; set; }
    }
}