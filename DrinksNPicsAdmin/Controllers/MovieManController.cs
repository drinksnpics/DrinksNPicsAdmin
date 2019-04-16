using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using MoviesDBModels;
using MoviesDBModels.Providers;
using System.Threading.Tasks;

namespace DrinksNPicsAdmin.Controllers
{
    public class MovieManController : Controller
    {
        MovieProvider prov = new MovieProvider();
        // GET
        public async Task<IActionResult> Index()
        {
            var current = prov.GetMovieInfo(CancellationToken.None);
            Console.WriteLine(current);
            Task.WhenAll(current);
            ViewData["movie"] = current.Result.original_title;
            return View();
        }

        public async Task<IActionResult> PopularMovies()
        {
            var current = prov.GetPopularMovies();
            Console.WriteLine(current);
            Task.WhenAll(current);
            ViewData["movies"] = current.Result.results as List<Result>;
            List<Result> Movies = current.Result.results as List<Result>;
            foreach (var movie in Movies)
            {
                //movie.overview = movie.overview.Substring(0, 200);
            }
            return View(Movies);
        }
    }
}