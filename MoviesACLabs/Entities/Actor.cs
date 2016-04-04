using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Revenue { get; set; }

        public virtual IList<Movie> Movies { get; set; }

    }
}