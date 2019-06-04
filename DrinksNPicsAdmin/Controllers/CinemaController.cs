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
        public async Task<IActionResult> AddCinemaRoom(string id)
        {
            CinemaRoom Room = new CinemaRoom();
            if (id != null)
            {
                Room = await CbService.GetCinemaRoom(id);
            }
            return View(Room);
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterCinema(CinemaRoom NewRoom)
        {
            // Console.WriteLine(NewRoom.Capacity);
            if (NewRoom.Id == null)
            {
                NewRoom.Id = Guid.NewGuid().ToString();
            }
            
            
            
            // Update Firebase
            await CbService.AddCinemaRoom(NewRoom);
            return RedirectToAction("CinemaRooms", "Cinema");
        }

        public async Task<IActionResult> AddFoodItem(string id)
        {
            FoodItem item = new FoodItem();
            if (id != null)
            {
                item = await CbService.GetSnackProduct(id);
            }
            return View(item);
        }

        public async Task<IActionResult> CreateProduct(FoodItem newItem)
        {
            Console.WriteLine(newItem.productName);
            if (newItem.id == null)
            {
                newItem.id = Guid.NewGuid().ToString();
            }
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

        public async Task<IActionResult> CinemaRooms()
        {
            List<CinemaRoom> cinemaRooms = await CbService.GetCinemaRooms();
            return View(cinemaRooms);
        }

        public async Task<IActionResult> ListAllOrders()
        {
            List<Order> orders = CbService.GetOrders().Result;
            return View(orders);
        }

    }
}