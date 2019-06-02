using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using MoviesDBModels;
using MoviesDBModels.Providers;
using System.Threading.Tasks;
using DrinksNPicsAdmin.Models;
using DrinksNPicsAdmin.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrinksNPicsAdmin.Controllers
{
    public class MovieManController : Controller
    {
        private readonly MovieProvider _prov = new MovieProvider();
        private readonly CinemaService _cinemaService = new CinemaService();
        // GET
        public async Task<IActionResult> Index()
        {
            var current = _prov.GetMovieInfo(CancellationToken.None, 550);
            Console.WriteLine(current);
            await Task.WhenAll(current);
            ViewData["movie"] = current.Result.original_title;
            ViewData["movieDate"] = current.Result.release_date;
            return View();
        }

        public async Task<IActionResult> PopularMovies()
        {
            List<CatalogueMovie> currentCatalogue = await _cinemaService.GetCatalogue();
            HashSet<long> currentMovieIds = new HashSet<long>();
            foreach (var movie in currentCatalogue)
            {
                currentMovieIds.Add(movie.movieId);
            }
            
            
            var current = _prov.GetPopularMovies();
            Console.WriteLine(current);
            await Task.WhenAll(current);
            ViewData["movies"] = current.Result.results as List<Result>;
            List<Result> movies = current.Result.results as List<Result>;
            
            List<Result> newForCatalogue = new List<Result>();
            foreach (var movie in movies)
            {
                if (!currentMovieIds.Contains(movie.id))
                {
                    Console.WriteLine(movie.overview);
                    if (movie.overview.Length > 100)
                    {
                        movie.overview = movie.overview.Substring(0, 100);
                        movie.overview += "...";
                        Console.WriteLine("Fixing length...");
                        newForCatalogue.Add(movie);
                    }
                }
            }
            return View(newForCatalogue);
        }
        
        public async Task<IActionResult> CinemaCatalogue()
        {
            List<CatalogueMovie> catalogue = new List<CatalogueMovie>();
            catalogue = await _cinemaService.GetCatalogue();
            return View(catalogue);
        }

        public async Task<IActionResult> AddToCinemaCatalogue(long movieId)
        {
            Movie movieInfo = _prov.GetMovieInfo(CancellationToken.None, movieId).Result;
            CatalogueMovie newMovie = new CatalogueMovie()
            {
                id = Guid.NewGuid().ToString(),
                movieeDescription = movieInfo.overview,
                movieId = movieId,
                movieLength = movieInfo.runtime,
                movieRating = movieInfo.vote_average,
                movieTitle = movieInfo.original_title,
                releaseDate = DateTime.Parse(movieInfo.release_date),
            };

            await _cinemaService.AddMovieToCatalogue(newMovie);
            
            return RedirectToAction("CinemaCatalogue", "MovieMan");
        }

        public async Task<IActionResult> ListShowTimes()
        {
            List<RoomShowTimesViewModel> showTimesByRoom = new List<RoomShowTimesViewModel>();
            
            // Gets current rooms
            List<CinemaRoom> currentRooms = _cinemaService.GetCinemaRooms().Result;
            foreach (var room in currentRooms)
            {
                showTimesByRoom.Add( new RoomShowTimesViewModel()
                {
                    cinemaRoom =  room,
                    showTimes = new List<ShowTime>()
                });
            }
            return View(showTimesByRoom);
        }

        public async Task<IActionResult> AddShowTime()
        {
            List<CatalogueMovie> moviesFromCatalogue = _cinemaService.GetCatalogue().Result;
            List<CinemaRoom> avaliableRooms = await _cinemaService.GetCinemaRooms();
            
            List<SelectListItem> movieSelector = new List<SelectListItem>();
            List<SelectListItem> roomSelector = new List<SelectListItem>();
            
            // Gets movies and creates a selector with their ID
            foreach (var movie in moviesFromCatalogue)
            {
                movieSelector.Add( new SelectListItem()
                {
                    Text = movie.movieTitle,
                    Value = movie.movieId.ToString(),
                    Selected = false,
                });
            }

            foreach (var room in avaliableRooms)
            {
                roomSelector.Add(new SelectListItem()
                {
                    Text = "Room " + room.RoomNumber,
                    Value = room.Id,
                    Selected = false
                    
                });
            }
            
            // Adding items to view
            ShowtimeFormModel newShowTime = new ShowtimeFormModel()
            {
                movies = movieSelector,
                rooms = roomSelector,
                showTime = new ShowTime()
            };

            return View(newShowTime);
        }
        public async Task<IActionResult> CreateShowtime()
        {
            
            return View();
        }
        
    }
}