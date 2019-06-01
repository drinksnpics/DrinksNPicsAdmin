using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using DrinksNPicsAdmin.Services;
using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using MoviesDBModels;

namespace DrinksNPicsAdmin.Controllers
{
    public class CinemaController : Controller
    {
        public static CinemaService CbService = new CinemaService();
        // GET
        public IActionResult AddCinemaRoom()
        {
            CinemaRoom Room = new CinemaRoom();
            return View(Room);
        }
        
        [HttpPost]
        public IActionResult RegisterCinema(CinemaRoom NewRoom)
        {
            // Console.WriteLine(NewRoom.Capacity);
            NewRoom.Id = Guid.NewGuid().ToString();
            
            
            // Update Firebase
            CbService.AddCinemaRoom(NewRoom);
            return RedirectToAction("CinemaRooms", "Cinema");
        }

        public IActionResult AddFoodItem()
        {
            return View();
        }

        public async Task<IActionResult> CreateProduct(FoodItem newItem)
        {
            Console.WriteLine(newItem.productName);
            newItem.id = Guid.NewGuid().ToString();
            await CbService.AddFoodItem(newItem);
            return RedirectToAction("ListAllProducts", "Cinema");
        }

        public IActionResult FoodItemDetail(string id)
        {
            return View();
        }
        public IActionResult ListAllProducts()
        {
            List<FoodItem> products = CbService.GetFoodItems().Result;
            return View(products);
        }

        public IActionResult CinemaRooms()
        {
            List<CinemaRoom> cinemaRooms = CbService.GetCinemaRooms().Result;
            return View(cinemaRooms);
        }
    }
}