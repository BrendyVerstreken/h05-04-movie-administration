using System.Collections.Generic;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Services.Interfaces
{
    public interface ISessionService
    {
        List<FavoriteMoviesViewModel> GetFavoriteMovies();
        void RemoveMovieFromFavoriteMovies(long id);
        void AddMovieToFavoriteMovies(Movie movie);
    }
}