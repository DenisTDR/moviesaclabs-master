using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MoviesACLabs.Data;
using MoviesACLabs.Models;
using AutoMapper;
using MoviesACLabs.Entities;

namespace MoviesACLabs.Controllers
{
    public class ActorsController : ApiController
    {
        private MoviesContext db = new MoviesContext();

        // GET: api/Actors
        public IList<ActorModel> GetActors()
        {
            var actors = db.Actors;
            var actorsModel = Mapper.Map<IList<ActorModel>>(actors);
            return actorsModel;
        }
        
        // GET: api/Actors/5
        [ResponseType(typeof(ActorModel))]
        public IHttpActionResult GetActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            var actorModel = Mapper.Map<ActorModel>(actor);

            return Ok(actorModel);
        }

        // GET: api/Actors/5
        [ResponseType(typeof(IList<ActorModel>))]
        [Route("api/Actors/richActors/{revenue}")]
        public IHttpActionResult GetRichActors(int revenue)
        {
            var richActors = db.Actors.Where(a => a.Revenue >= revenue);

            var rez = Mapper.Map<IList<ActorModel>>(richActors);

            return Ok(rez);
        }

        // PUT: api/Actors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActor(int id, ActorModel actorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != actorModel.Id)
            {
                return BadRequest();
            }

            var actor = Mapper.Map<Actor>(actorModel);

            db.Entry(actor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Actors
        [ResponseType(typeof(ActorModel))]
        public IHttpActionResult PostActor(ActorModel actorModel)
        {
            /* if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             var actor = Mapper.Map<Actor>(actorModel);

             //var awards = Mapper.Map<IList<Award>>(actor.Awards);

             var awardsToAdd = new List<Award>();
             foreach(var aw in awards)
             {
                 var aw1 = db.Awards.FirstOrDefault(a => a.Id == aw.Id);
                 if(aw1 != null)
                 {
                     awardsToAdd.Add(aw1);
                 }
                 else
                 {
                     //aw.Actors.Add(actor);
                     awardsToAdd.Add(aw);
                     db.Awards.Add(aw);
                 }
             }


            // actor.Awards = awardsToAdd;


             db.Actors.Add(actor);
             db.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { id = actor.Id }, actor);*/
            return null;
        }

        // DELETE: api/Actors/5
        public IHttpActionResult DeleteActor(int id)
        {
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }

            db.Actors.Remove(actor);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActorExists(int id)
        {
            return db.Actors.Any(e => e.Id == id);
        }
    }
}