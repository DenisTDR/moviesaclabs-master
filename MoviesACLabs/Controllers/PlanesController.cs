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
    public class PlanesController : ApiController
    {
        private MoviesContext db = new MoviesContext();


        public IList<PlaneModel> GetPlanes()
        {
            var planes = db.Planes.ToList();

            var planesModel = Mapper.Map<IList<PlaneModel>>(planes);

            return planesModel;
        }

        public IHttpActionResult GetPlane(int id)
        {
            var plane = db.Planes.Find(id);

            if(plane == null)
            {
                return NotFound();
            }

            var planeModel = Mapper.Map<PlaneModel>(plane);

            return Ok(planeModel);
        }

        public IHttpActionResult PostPlane(PlaneModel planeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plane = Mapper.Map<Plane>(planeModel);

            //if (planeModel.Airline.Id != 0)
            //{
            //    var airline = db.Airlines.Find(planeModel.Airline.Id);
            //    if(airline == null)
            //    {
            //        return NotFound();
            //    }
            //    airline.Planes.Add(plane);

            //    db.Entry(airline).State = EntityState.Modified;
            //}
            db.Planes.Add(plane);

            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = plane.Id }, plane);
        }

        public IHttpActionResult PutPlane(int id, PlaneModel planeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != planeModel.Id)
            {
                return BadRequest();
            }

            var plane = Mapper.Map<Plane>(planeModel);

            db.Entry(plane).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaneExists(id))
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

        public IHttpActionResult DeletePlane(int id)
        {
            Plane plane = db.Planes.Find(id);

            if (plane == null)
            {
                return NotFound();
            }

            db.Planes.Remove(plane);
            db.SaveChanges();

            return Ok();
        }

        private bool PlaneExists(int id)
        {
            return db.Planes.Any(e => e.Id == id);
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
