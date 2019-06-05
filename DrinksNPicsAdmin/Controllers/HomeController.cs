using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrinksNPicsAdmin.Models;
using DrinksNPicsAdmin.Services;
using MoviesDBModels;
using MoviesDBModels.Providers;

namespace DrinksNPicsAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieProvider _prov = new MovieProvider();
        private readonly CinemaService _cinemaService = new CinemaService();
        public async Task<IActionResult> Index()
        {
            CinemaDashBoard dashBoard = new CinemaDashBoard();
            // Get Total of rooms
            var rooms = await _cinemaService.GetCinemaRooms();
            dashBoard.totalRooms = rooms.Count;
            
            
            // Get FoodItem Info
            int availabeItems = 0;
            int totalItems = 0;
            List<FoodItem> foodItems = await _cinemaService.GetFoodItems();
            foreach (var item in foodItems)
            {
                if (item.avaliable)
                    availabeItems++;
                totalItems++;
            }

            float ticketsSales = 0f;
            int ticketOrders = 0;
            float foodSales = 0f;
            int foodOrders = 0;
            List<Order> orders = _cinemaService.GetOrders().Result;
            foreach (var item in orders)
            {
                if (item.precio == 70)
                {                    
                    ticketsSales += (item.precio*item.cantidad);
                    ticketOrders += item.cantidad;   
                }
                else
                {
                    foodSales += (item.precio*item.cantidad);
                    foodOrders++;  
                }
            }
            ViewData["ticketsSales"] = ticketsSales;
            ViewData["ticketOrders"] = ticketOrders;
            ViewData["foodSales"] = foodSales;
            ViewData["foodOrders"] = foodOrders;
            dashBoard.availableFoodItems = availabeItems;
            dashBoard.totalFoodItems = totalItems;            
            return View(dashBoard);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {                            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}