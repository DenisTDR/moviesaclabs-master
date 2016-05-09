using MoviesACLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MoviesACLabs.Controllers
{
    public class AwardsController:ApiController
    {
        private static List<AwardModel> hardcodedAwards = null;
        private int lastId = 0;

        public AwardsController()
        {
            if(hardcodedAwards == null)
            {
                hardcodedAwards = new List<AwardModel>(new[] {
                    new AwardModel { Id = 0 , Name="Nume1"},
                    new AwardModel { Id = 1 , Name="Nume2"},
                    new AwardModel { Id = 2 , Name="Nume3"},
                    new AwardModel { Id = 3 , Name="Nume4"}
                });
                lastId = 3;
            }
        }
        public IList<AwardModel> GetAwards()
        {
            return hardcodedAwards;
        }

        [Route("myAward/{id}")]
        public AwardModel GetAward(int id)
        {
            var award = hardcodedAwards.FirstOrDefault(aw => aw.Id == id);
            return award;
        }

        public IHttpActionResult PostAward(AwardModel award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            award.Id = ++lastId;
            hardcodedAwards.Add(award);
            return Ok();
        }

        public IHttpActionResult PutAward(AwardModel award)
        {
            var existingAward = hardcodedAwards.FirstOrDefault(aw => aw.Id == award.Id);
            if (existingAward == null)
            {
                return NotFound();
            }
            CopyProperties(award, existingAward);
            return Ok();
        }

        public IHttpActionResult DeleteAward(int id)
        {
            var toDelete = hardcodedAwards.FirstOrDefault(aw => aw.Id == id);
            if(toDelete != null)
            {
                hardcodedAwards.Remove(toDelete);
                return Ok();
            }
            return NotFound();
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