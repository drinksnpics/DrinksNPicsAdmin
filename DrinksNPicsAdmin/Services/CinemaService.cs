using System.Threading.Tasks;
using Firebase.Database;

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
        

    }
}