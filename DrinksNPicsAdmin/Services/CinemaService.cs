using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using DrinksNPicsAdmin.Models.Firebase;
using LiteDB;
using MoviesDBModels;

namespace DrinksNPicsAdmin.Services
{
    public class CinemaService
    {
        private const string APIKEY = "AIzaSyCSv35d6_nlMBkvVUlxTPYcHEmGWCVPmpg";
        private const string auth = "2EYmkOQ3GbMUlHVV8W0WtPSCsBrrYjpBERbC2FMz";
        
        public FirebaseClient firebaseClient { get; set; }

        public CinemaService()
        {
            firebaseClient = new FirebaseClient("https://drinksnpics.firebaseio.com/",
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(auth)
                });
        }
        
        public async Task AddCinemaRoom(CinemaRoom newRoom)
        {
            await firebaseClient.Child("Rooms/" + newRoom.Id).PutAsync<CinemaRoom>(newRoom);

        }
        
        public async Task<CinemaRoom> GetCinemaRoom(string roomId)
        {
            var rooms = await firebaseClient.Child("Rooms")
                .OrderByKey()
                .EqualTo(roomId)
                .OnceAsync<CinemaRoom>();

            foreach (var room in rooms)
            {
                return room.Object;
            }
            return null;
        }
        
        public async Task<List<CinemaRoom>> GetCinemaRooms()
        {
            var rooms = await firebaseClient.Child("Rooms")
                .OrderBy("RoomNumber")
                .OnceAsync<CinemaRoom>();
            List<CinemaRoom> cinemaRooms = new List<CinemaRoom>();
            foreach (var room in rooms)
            {
                cinemaRooms.Add(room.Object);
            }
            
            cinemaRooms = cinemaRooms.OrderBy(x => x.RoomNumber).ToList();

            return cinemaRooms;
        }
        
        public async Task AddFoodItem(FoodItem foodItem)
        {
            await firebaseClient.Child("FoodProducts/" + foodItem.id).PutAsync<FoodItem>(foodItem);
        }
        
        public async Task<List<FoodItem>> GetFoodItems()
        {
            var rooms = await firebaseClient.Child("FoodProducts").OnceAsync<FoodItem>();
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (var room in rooms)
            {
                foodItems.Add(room.Object);
            }

            return foodItems;
        }
        
        public async Task AddMovieToCatalogue(CatalogueMovie newMovie)
        {
            await firebaseClient.Child("Catalogue/" + newMovie.id).PutAsync<CatalogueMovie>(newMovie);
        }
        
        public async Task<List<CatalogueMovie>> GetCatalogue()
        {
            var rawCatalogue = await firebaseClient.Child("Catalogue").OnceAsync<CatalogueMovie>();
            List<CatalogueMovie> moviesInCatalogue = new List<CatalogueMovie>();
            foreach (var movie in rawCatalogue)
            {
                moviesInCatalogue.Add(movie.Object);
            }

            return moviesInCatalogue;
        }
        
        public async Task<CatalogueMovie> GetMovieFromCatalogue(string movieId)
        {
            var movies = await firebaseClient.Child("Catalogue")
                .OrderByKey()
                .EqualTo(movieId)
                .OnceAsync<CatalogueMovie>();

            foreach (var movie in movies)
            {
                return movie.Object;
            }
            return null;
        }
        
        public async Task<FoodItem> GetSnackProduct(string itemId)
        {
            var prods = await firebaseClient.Child("FoodProducts")
                .OrderByKey()
                .EqualTo(itemId)
                .OnceAsync<FoodItem>();

            foreach (var prod in prods)
            {
                return prod.Object;
            }
            return null;
        }
        
        public async Task<List<ShowTime>> GetShowTimesByRoom(string roomId)
        {
            List<ShowTime> st = new List<ShowTime>();
            var showTimes = await firebaseClient.Child("Showtime")
                .OrderByKey()
                .OnceAsync<ShowTime>();

            foreach (var showTime in showTimes)
            {
                if (showTime.Object.roomId == roomId)
                {
                    st.Add(showTime.Object);
                }
            }
            return st;
        }
        
        public async Task<List<ShowTime>> GetShowTimesByRoomForDate(string roomId, DateTime date)
        {
            DateTime dateQuery = date.Date;
            List<ShowTime> st = new List<ShowTime>();
            var showTimes = await firebaseClient.Child("Showtime")
                .OrderByKey()
                .OnceAsync<ShowTime>();

            foreach (var showTime in showTimes)
            {
                if (showTime.Object.roomId == roomId && showTime.Object.startDate.Date == dateQuery)
                {
                    st.Add(showTime.Object);
                }
            }
            return st;
        }
        
        public async Task AddShowtime(ShowTime newShowtime)
        {
            await firebaseClient.Child("Showtime/" + newShowtime.id).PutAsync<ShowTime>(newShowtime);
        }

        public async Task<List<Order>> GetOrders()
        {
            var rawOrders = await firebaseClient.Child("Orders").OnceAsync<Order>();
            List<Order> orders = new List<Order>();
            foreach (var order in rawOrders)
            {
                orders.Add(order.Object);
            }

            return orders;
        }        
    }
}