using AutoMapper;
using MoviesACLabs.Data;
using MoviesACLabs.Entities;
using MoviesACLabs.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MoviesACLabs.Controllers
{
    public class AirlinesController : ApiController
    {
        private MoviesContext db = new MoviesContext();
        

        public IList<AirlineModel> GetAirlines()
        {
            var airlines = db.Airlines.Include(m => m.Planes);

            var airlinesModel = Mapper.Map<IList<AirlineModel>>(airlines);

            return airlinesModel;
        }


        [Route("api/Airlines/Filter")]
        [HttpGet]
        public IList<AirlineModel> AirlinesFilter()
        {
            var airlines = db.Airlines.Include(m => m.Planes).ToList();

            var goodAirlines = airlines.Where(air => Has2Vowels(air.Name));

            var airlinesModel = Mapper.Map<IList<AirlineModel>>(goodAirlines);            

            return airlinesModel;
        }

        private bool Has2Vowels(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            int c = 0;
            foreach(char lit in name)
            {
                if ("aeiou".Contains(lit))
                {
                    c++;
                }
            }
            return c == 2;
        }

        public IHttpActionResult GetMovie(int id)
        {
            var airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return NotFound();
            }

            var movieModel = Mapper.Map<AirlineModel>(airline);

            return Ok(movieModel);
        }


        
        public IHttpActionResult PostAirline(AirlineModel airlineModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var airline = Mapper.Map<Airline>(airlineModel);

            db.Airlines.Add(airline);
            db.SaveChanges();

            var airlineModel2 = Mapper.Map<AirlineModel>(airline);

            return Ok(airlineModel2);
        }

        public IHttpActionResult PutAirline(int id, AirlineModel airlineModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != airlineModel.Id)
            {
                return BadRequest();
            }

            var airline = Mapper.Map<Airline>(airlineModel);

            db.Entry(airline).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirlineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        


        public IHttpActionResult DeleteAirline(int id)
        {
            Airline airline = db.Airlines.Find(id);
            
            if(airline == null)
            {
                return NotFound();
            }
            foreach(Plane p in airline.Planes.ToList())
            {
                db.Planes.Remove(p);
            }

            db.Airlines.Remove(airline);
            db.SaveChanges();

            return Ok();
        }


        private bool AirlineExists(int id)
        {
            return db.Airlines.Any(e => e.Id == id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
