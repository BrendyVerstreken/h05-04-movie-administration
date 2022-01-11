using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Services
{
    public class SessionService : ISessionService
    {
        //inject de contextaccessor om aan de HttpContext te raken
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<FavoriteMoviesViewModel> GetFavoriteMovies()
        {
            return JsonConvert
                    .DeserializeObject<List<FavoriteMoviesViewModel>>(_httpContextAccessor.HttpContext.Session.GetString("Movies"));
        }
        public void RemoveMovieFromFavoriteMovies(long id)
        {
            if (_httpContextAccessor.HttpContext.Session.Keys.Contains("Movies"))
            {
                List<FavoriteMoviesViewModel> favoriteMovies = JsonConvert
                    .DeserializeObject<List<FavoriteMoviesViewModel>>(_httpContextAccessor.HttpContext.Session.GetString("Movies"));
                favoriteMovies.RemoveAll(m => m.Id == id);
                _httpContextAccessor.HttpContext.Session.SetString("Movies", JsonConvert.SerializeObject(favoriteMovies));
            }
            if (_httpContextAccessor.HttpContext.Session.Keys.Contains("NumberOfMovies"))
            {
                int counter = (int)_httpContextAccessor.HttpContext.Session.GetInt32("NumberOfMovies");
                _httpContextAccessor.HttpContext.Session.SetInt32("NumberOfMovies", counter - 1);
            }
        }

        public void AddMovieToFavoriteMovies(Movie movie)
        {
            List<FavoriteMoviesViewModel> favoriteMovies = new();

            if (_httpContextAccessor.HttpContext.Session.Keys.Contains("Movies"))
            {
                favoriteMovies = JsonConvert
                    .DeserializeObject<List<FavoriteMoviesViewModel>>(_httpContextAccessor.HttpContext.Session.GetString("Movies"));
            }
            if (_httpContextAccessor.HttpContext.Session.Keys.Contains("NumberOfMovies"))
            {
                int counter = (int)_httpContextAccessor.HttpContext.Session.GetInt32("NumberOfMovies");
                _httpContextAccessor.HttpContext.Session.SetInt32("NumberOfMovies", counter + 1);
            }
            else
            {
                _httpContextAccessor.HttpContext.Session.SetInt32("NumberOfMovies", 1);
            }
            favoriteMovies.Add(new FavoriteMoviesViewModel { Id = movie.Id, Title = movie.Title });
            _httpContextAccessor.HttpContext.Session.SetString("Movies", JsonConvert.SerializeObject(favoriteMovies));
            }
        }
    }

