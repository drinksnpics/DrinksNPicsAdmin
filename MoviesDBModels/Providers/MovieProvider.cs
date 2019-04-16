using System;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MoviesDBModels.Providers
{ 
    public class MovieProvider
    {
        readonly HttpClient _client = new HttpClient();
        private const string _token = "a9329b6384547944359b072ff05a7031";

        public MovieProvider()
        {
            _client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }

        public async Task<Movie> GetMovieInfo(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            int id = 550;
            HttpResponseMessage response = await _client.GetAsync("movie/" + id.ToString() + "?api_key=" + _token);
            if (response.IsSuccessStatusCode)
            {
                var stringTask =  response.Content.ReadAsStringAsync();
                var res = await stringTask;
                return JsonConvert.DeserializeObject<Movie>(res);
                //Console.WriteLine(res);
            }

            return new Movie();
        }

        public async Task<PopularMovies>GetPopularMovies()
        {
            
            HttpResponseMessage response = await _client.GetAsync("discover/movie?sort_by=popularity.desc&api_key=a9329b6384547944359b072ff05a7031&primary_release_year=2019");
            if (response.IsSuccessStatusCode)
            {
                var stringTask =  response.Content.ReadAsStringAsync();
                var res = await stringTask;
                return JsonConvert.DeserializeObject<PopularMovies>(res);
                //Console.WriteLine(res);
            }

            return new PopularMovies();
        }
        
        
    }
}