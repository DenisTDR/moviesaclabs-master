using MoviesACLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MoviesACLabs.Data;
using MoviesACLabs.Entities;
using AutoMapper;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MoviesACLabs.Controllers
{
    public class AwardsController:ApiController
    {
        private MoviesContext db = new MoviesContext();
        public IList<AwardModel> GetAwards()
        {
            var awards = db.Awards;
            var awardModels = Mapper.Map<IList<AwardModel>>(awards);
            return awardModels;
        }

        [Route("myAward/{id}")]
        public IHttpActionResult GetAward(int id)
        {
            var award = db.Awards.FirstOrDefault(a => a.Id == id);
            if (award == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AwardModel>(award));
        }
        [Route("anAward/{name}")]
        public IHttpActionResult GetAwardByName(string name)
        {
            var award = db.Awards.FirstOrDefault(a => a.Name == name);
            if (award == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<AwardModel>(award));
        }

        public IHttpActionResult PostAward(AwardModel award)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var awardToAdd = Mapper.Map<Award>(award);
            db.Awards.Add(awardToAdd);
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult PutAward(int id, AwardModel awardModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != awardModel.Id)
            {
                return BadRequest();
            }

            var award = Mapper.Map<Award>(awardModel);
            db.Entry(award).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!db.Awards.Any(a=>a.Id == id))
                {
                    return NotFound();
                }
                return InternalServerError();
            }

            return Ok();
        }

        public IHttpActionResult DeleteAward(int id)
        {
            var existingAward = db.Awards.FirstOrDefault(a => a.Id == id);
            if (existingAward == null)
            {
                return NotFound();
            }
            db.Awards.Remove(existingAward);
            db.SaveChanges();
            return Ok();
        }

        private void CopyProperties(object source, object target)
        {
            foreach(var prop in source.GetType().GetProperties())
            {
                object defaultValue = prop.PropertyType.IsValueType ? Activator.CreateInstance(prop.PropertyType) : null;
                if(prop.GetValue(source) != defaultValue)
                {
                    prop.SetValue(target, prop.GetValue(source));
                }
            }
        }
    }
}