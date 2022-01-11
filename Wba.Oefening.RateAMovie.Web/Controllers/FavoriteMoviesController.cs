using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Controllers
{
    public class FavoriteMoviesController : Controller
    {
        private readonly MovieContext _movieContext;
        private readonly ISessionService _sessionService;

        public FavoriteMoviesController(MovieContext movieContext,ISessionService sessionService)
        {
            _movieContext = movieContext;
            _sessionService = sessionService;
        }
        public IActionResult Add(long id)
        {
            _sessionService.AddMovieToFavoriteMovies(_movieContext.Movies.FirstOrDefault(m => m.Id == id));
            return RedirectToAction("List");
        }
        public IActionResult List()
        {
            var favoriteMovies = _sessionService.GetFavoriteMovies();
            //var favoriteMovies = JsonConvert
            //        .DeserializeObject<List<FavoriteMoviesViewModel>>(HttpContext.Session.GetString("Movies"));
            return View(favoriteMovies);
        }
        public IActionResult Remove(long id)
        {
            _sessionService.RemoveMovieFromFavoriteMovies(id);
            return RedirectToAction("List");
        }
    }
}
