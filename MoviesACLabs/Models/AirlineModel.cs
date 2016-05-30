using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Models
{
    public class AirlineModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public  IList<PlaneModel> Planes { get; set; }
    }
}