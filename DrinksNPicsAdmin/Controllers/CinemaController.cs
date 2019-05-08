using System;
using Microsoft.AspNetCore.Mvc;
using MoviesDBModels;

namespace DrinksNPicsAdmin.Controllers
{
    public class CinemaController : Controller
    {
        // GET
        public IActionResult AddCinemaRoom()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult RegisterCinema(CinemaRoom NewRoom)
        {
            Console.WriteLine(NewRoom.Capacity);
            return RedirectToAction("CinemaRooms", "Cinema");
        }
        
        public IActionResult CinemaRooms()
        {
            return View();
        }
    }
}