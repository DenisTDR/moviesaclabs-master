﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesACLabs.Entities
{
    public class Award
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int RandomNumber { get; set; }
        
        public int ActorId { get; set; }
        public virtual Actor Actor { get; set; }
    }
}