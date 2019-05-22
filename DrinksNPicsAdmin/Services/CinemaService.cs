using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using DrinksNPicsAdmin.Models.Firebase;
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
        
        public async Task GetCinemaRooms(CinemaRoom NewRoom)
        {
            await firebaseClient.Child("Rooms").OnceAsync<CinemaRoom>();
        }
        

    }
}