using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.dto;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Data
{
    public class MovieRepository : IRepository
    {
        MovieContext context;
        public MovieRepository(MovieContext context)
        {
            this.context = context;
        }

        public bool AddActor(Actor actor)
        {
            try
            {
                context.Actors.Add(actor);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AddMovie(MovieDto movie)
        {
            try
            {
                context.Movies.Add(movie.Movie);
                foreach (var actorId in movie.Actors)
                {
                    context.MovieActors.Add(new MovieActor
                    {
                        Movie = movie.Movie,
                        Actor = context.Actors.Find(actorId)
                    });
                    context.SaveChanges();
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteActor(Actor actor)
        {
            try
            {
                context.Actors.Remove(actor);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteMovie(Movie movie)
        {

            try
            {
                context.Movies.Remove(movie);
                var movieActors = context.MovieActors
                    .Where(ma => ma.Movie == movie);
                context.MovieActors.RemoveRange(movieActors);
                int result = context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Actor GetActor(int id)
        {
            return context.Actors.Find(id);
        }

        public IEnumerable<Actor> GetActors()
        {
            return context.Actors.ToList();
        }

        public bool UpdateMovie(MovieDto movie)
        {
            try
            {
                context.Movies.Add(movie.Movie);
                context.SaveChanges();
                var movieActors = context.MovieActors
                    .Where(ma => ma.Movie == movie.Movie);
                context.MovieActors.RemoveRange(movieActors);
                context.SaveChanges();
                //remove all relations
                foreach (var actorId in movie.Actors)
                {
                    context.MovieActors.Add(new MovieActor
                    {
                        Movie = movie.Movie,
                        Actor = context.Actors.Find(actorId)
                    });
                    context.SaveChanges();
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Movie> GetMovies()
        {
            return context.Movies.ToList();
        }

        public bool UpdateActor(Actor actor)
        {
            try
            {
                context.Actors.Update(actor);
                int result = context.SaveChanges();//returns the number of records on which the actions are performed
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public object GetMovie(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMovie(object movie)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMoviesByActor(int actorId)
        {
                var movies = from m in context.Movies
                             join ma in context.MovieActors on m.Id equals ma.MovieId
                             where ma.ActorId == actorId
                             select m;
                return movies;

        }

        public IEnumerable<Movie> GetMoviesByGenre(Genre genre)
        {
            return context.Movies.Where(m => m.Genre == (Genre)genre);
        }

        public IEnumerable<Actor> GetActorsByMovie(int movieId)
        {
            var actors = from a in context.Actors
                         join ma in context.MovieActors on a.Id equals ma.ActorId
                         where ma.MovieId == movieId
                         select a;
            return actors;
        }
    }
}
