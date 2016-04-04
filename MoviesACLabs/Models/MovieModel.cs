using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Models
{
    public class MovieModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public IList<ActorModel> Actors { get; set; }

        public IList<ReviewModel> Reviews { get; set; }
    }
}