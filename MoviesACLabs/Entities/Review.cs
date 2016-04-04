using System.ComponentModel.DataAnnotations;

namespace MoviesACLabs.Entities
{
    public class Review
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }

        public virtual Movie Movie { get; set; }
    }
}