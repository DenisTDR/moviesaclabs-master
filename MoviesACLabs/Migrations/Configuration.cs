using System.Collections.Generic;

namespace MoviesACLabs.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MoviesACLabs.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesACLabs.Data.MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MoviesACLabs.Data.MoviesContext";
        }

        protected override void Seed(MoviesACLabs.Data.MoviesContext context)
        {
            var actor1 = new Actor
            {
                Name = "Leonardo DiCaprio",
                DateOfBirth = new DateTime(1974, 11, 11),
                Revenue = 999999992
            };
            var actor2 = new Actor
            {
                Name = "Robert John Downey Jr.",
                DateOfBirth = new DateTime(1965, 4, 4),
                Revenue = 999999993
            };
            var actor3 = new Actor
            {
                Name = "Jennifer Lawrence",
                DateOfBirth = new DateTime(1990, 8, 15),
                Revenue = 999999991
            };

            var actors = new List<Actor>
            {
                actor1, actor2, actor3
            };

            actors.ForEach(a => context.Actors.AddOrUpdate(a));
            context.SaveChanges();

            var review1 = new Review
            {
                Comment = "My review",
                Rating = 5
            };

            context.Reviews.AddOrUpdate(review1);
            context.SaveChanges();

            var movie1 = new Movie { Title = "Hunger Games", Description = "A movie with starving people.", Actors = new List<Actor> { actor3 }, Reviews = new List<Review> { review1 } };
            var movie2 = new Movie { Title = "Iron Man", Description = "Pew Pew Pew", Actors = new List<Actor> { actor2 } };
            var movie3 = new Movie { Title = "Inception", Description = "A dream within a dream, within a dream... etc...", Actors = new List<Actor> { actor1, actor3 } };

            var movies = new List<Movie>
            {
                movie1, movie2, movie3
            };

            movies.ForEach(m => context.Movies.AddOrUpdate(m));
            context.SaveChanges();
        }
    }
}
