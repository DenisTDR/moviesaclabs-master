using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual IList<Actor> Actors { get; set; }

        public virtual IList<Review> Reviews { get; set; }
    }
}