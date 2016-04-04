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
    public class ReviewsController : ApiController
    {
        private MoviesContext db = new MoviesContext();

        // GET: api/Reviews/5
        [ResponseType(typeof(ReviewModel))]
        public IHttpActionResult GetReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            var reviewModel = Mapper.Map<ReviewModel>(review);

            return Ok(reviewModel);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReview(int id, ReviewModel reviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reviewModel.Id)
            {
                return BadRequest();
            }

            var review = Mapper.Map<Review>(reviewModel);

            db.Entry(review).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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

        // POST: api/Reviews
        [ResponseType(typeof(ReviewModel))]
        public IHttpActionResult PostReview(int movieId, ReviewModel reviewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = Mapper.Map<Review>(reviewModel);

            var movie = db.Movies.Find(movieId);

            review.Movie = movie;

            db.Reviews.Add(review);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        public IHttpActionResult DeleteReview(int id)
        {
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
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

        private bool ReviewExists(int id)
        {
            return db.Reviews.Any(e => e.Id == id);
        }
    }
}