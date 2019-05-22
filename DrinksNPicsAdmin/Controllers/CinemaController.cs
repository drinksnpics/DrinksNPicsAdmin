using System;
using DrinksNPicsAdmin.Services;
using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using MoviesDBModels;

namespace DrinksNPicsAdmin.Controllers
{
    public class CinemaController : Controller
    {
        // GET
        public IActionResult AddCinemaRoom()
        {
            CinemaRoom Room = new CinemaRoom();
            return View(Room);
        }
        
        [HttpPost]
        public IActionResult RegisterCinema(CinemaRoom NewRoom)
        {
            Console.WriteLine(NewRoom.Capacity);
            NewRoom.Id = Guid.NewGuid().ToString();
            
            
            // Update Firebase
            CinemaService cbService = new CinemaService();
            cbService.AddCinemaRoom(NewRoom);
            return RedirectToAction("CinemaRooms", "Cinema");
        }
        
        public IActionResult CinemaRooms()
        {
            return View();
        }
    }
}