using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Models
{
    public class PlaneModel
    {
        public int Id { get; set; }
        public int Seats { get; set; }
        public string Model { get; set; }
        //public AirlineModel Airline { get; set; }
    }
}