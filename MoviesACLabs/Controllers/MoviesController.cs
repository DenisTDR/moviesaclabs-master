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
    public class MoviesController : ApiController
    {
        private MoviesContext db = new MoviesContext();
        
        // GET: api/Movies
        public IList<MovieModel> GetMovies()
        {
            var movies = db.Movies.Include(m => m.Reviews).Include(m => m.Actors);

            var moviesModel = Mapper.Map<IList<MovieModel>>(movies);

            return moviesModel;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(MovieModel))]
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieModel = Mapper.Map<MovieModel>(movie);

            return Ok(movieModel);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovie(int id, MovieModel movieModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movieModel.Id)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<Movie>(movieModel);

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        [ResponseType(typeof(MovieModel))]
        public IHttpActionResult PostMovie(MovieModel movieModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = Mapper.Map<Movie>(movieModel);

            db.Movies.Add(movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
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

        private bool MovieExists(int id)
        {
            return db.Movies.Any(e => e.Id == id);
        }
    }
}