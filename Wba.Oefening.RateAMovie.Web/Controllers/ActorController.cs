using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Web.Services;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;
using Wba.Oefening.RateAMovie.Web.ViewModels;

namespace Wba.Oefening.RateAMovie.Web.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MovieContext _movieContext;
       

        public ActorsController(MovieContext movieContext)
        {
            _movieContext = movieContext;
            
        }
        public IActionResult Index()
        {
            
            ActorsIndexViewModel actorsIndexViewModel
                = new ActorsIndexViewModel();
            actorsIndexViewModel.Names = new List<string>();
            actorsIndexViewModel.Ids = new List<long?>();
            foreach (var actor in _movieContext.Actors)
            {
                actorsIndexViewModel.Names.Add($"{actor?.FirstName} {actor?.LastName}");
                actorsIndexViewModel.Ids.Add(actor?.Id);
            };
            return View(actorsIndexViewModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ActorsAddActorViewModel actorsAddActorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(actorsAddActorViewModel);
            }
            //save new actor
            var newActor = new Actor
            {
                FirstName = actorsAddActorViewModel?.Firstname,
                LastName = actorsAddActorViewModel?.Lastname
            };
            //add to context
            _movieContext.Actors.Add(newActor);
            //save
            try
            {
                _movieContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
            }
            TempData["Message"] = "Actor added";
            return RedirectToAction("Index", "Actors");
        }

        [HttpGet]
        public IActionResult ConfirmDelete(long Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public IActionResult Delete(long Id)
        {
            var deleteActor = _movieContext
                .Actors.FirstOrDefault(a => a.Id == Id);
            _movieContext.Actors.Remove(deleteActor);
            try
            {
                _movieContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
            }
            TempData["Message"] = "Actor deleted";
            return RedirectToAction("Index", "Actors");
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            ActorsAddActorViewModel actorsAddActorViewModel = new ActorsAddActorViewModel();
            actorsAddActorViewModel.Id = Id;
            var updateActor = _movieContext.Actors.FirstOrDefault(a => a.Id == Id);
            actorsAddActorViewModel.Firstname = updateActor?.FirstName;
            actorsAddActorViewModel.Lastname = updateActor?.LastName;
            return View(actorsAddActorViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ActorsAddActorViewModel actorsAddActorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(actorsAddActorViewModel);
            }
            //update Actor
            var updateActor = _movieContext.Actors
                .FirstOrDefault(a => a.Id == actorsAddActorViewModel.Id);
            updateActor.FirstName = actorsAddActorViewModel?.Firstname;
            updateActor.LastName = actorsAddActorViewModel?.Lastname;
            //savechanges
            try
            {
                _movieContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
            }
            TempData["Message"] = "Actor edited";
            return RedirectToAction("Index", "Actors");
        }
    }

}
