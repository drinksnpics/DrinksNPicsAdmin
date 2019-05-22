using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using DrinksNPicsAdmin.Models.Firebase;
using MoviesDBModels;


namespace DrinksNPicsAdmin.Services
{
    public class FirebaseService
    {
        private const string APIKEY = "AIzaSyCSv35d6_nlMBkvVUlxTPYcHEmGWCVPmpg";
        private const string auth = "2EYmkOQ3GbMUlHVV8W0WtPSCsBrrYjpBERbC2FMz";

        public FirebaseClient firebaseClient { get; set; }


        public FirebaseService()
        {
            firebaseClient = new FirebaseClient("https://drinksnpics.firebaseio.com/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(auth)
            });
        }
        
        public async Task<UserDP> getUser(string username)
        {
            UserDP _user = new UserDP();
            var indexs = await firebaseClient                
                .Child("users")
                .OrderByKey()
                .EqualTo(username)
                .OnceAsync<UserDP>();

            foreach (var index in indexs)
            {
                _user = index.Object;
                return _user;
            }
            return null;
        }

        public async Task AddCinemaRoom(CinemaRoom NewRoom)
        {
            var res = await firebaseClient.Child("Rooms").OrderByKey().OnceAsync<CinemaRoom>();
        }
    }
}