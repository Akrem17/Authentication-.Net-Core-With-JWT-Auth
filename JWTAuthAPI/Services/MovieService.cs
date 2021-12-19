using JWTAuthAPI.Models;
using JWTAuthAPI.Repositories;

namespace JWTAuthAPI.Services
{
    public class MovieService : IMovie
    {
        public Movie Create(Movie movie)
        {
            movie.Id = MovieRpository.Movies.Count + 1;
            MovieRpository.Movies.Add(movie);

            return movie;
        }

        public bool Delete(int id)
        {
            var movie = MovieRpository.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return false;
            }
            MovieRpository.Movies.Remove(movie);
            return true;

        }

        public Movie Get(int id)
        {
            var movie = MovieRpository.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
            {
                return null;
            }

            return movie;


        }

        public List<Movie> List()
        {
            var movies = MovieRpository.Movies;
            return movies;
        }

        public Movie Update(Movie movie)
        {
            var oldMovie = MovieRpository.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (oldMovie == null)
            {
                return null;
            }

            oldMovie.Title = movie.Title;
            oldMovie.Description = movie.Description;
            oldMovie.Rating = movie.Rating;

            return oldMovie;
        }

    }
}