using System.Collections.Generic;
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
        
        public async Task AddCinemaRoom(CinemaRoom NewRoom)
        {
            await firebaseClient.Child("Rooms").PostAsync<CinemaRoom>(NewRoom);

        }
        
        public async Task<List<CinemaRoom>> GetCinemaRooms()
        {
            var rooms = await firebaseClient.Child("Rooms").OnceAsync<CinemaRoom>();
            List<CinemaRoom> cinemaRooms = new List<CinemaRoom>();
            foreach (var room in rooms)
            {
                cinemaRooms.Add(room.Object);
            }

            return cinemaRooms;
        }
        
        public async Task AddFoodItem(FoodItem foodItem)
        {
            await firebaseClient.Child("FoodProducts").PostAsync<FoodItem>(foodItem);

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

    }
}