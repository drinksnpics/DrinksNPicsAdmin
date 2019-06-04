using System;
using System.Collections.Generic;

namespace MoviesDBModels
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ProductionCompany
    {
        public int id { get; set; }
        public string logo_path { get; set; }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class ProductionCountry
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class SpokenLanguage
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

    public class Movie
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        public long id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public List<ProductionCompany> production_companies { get; set; }
        public List<ProductionCountry> production_countries { get; set; }
        public string release_date { get; set; }
        public long revenue { get; set; }
        public int runtime { get; set; }
        public List<SpokenLanguage> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }
    
    public class Result
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public string title { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public List<int> genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
    }

    public class PopularMovies
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }

    public class CinemaRoom
    {
        public String Id { get; set; }
        public int Capacity { get; set; }
        public int RoomNumber { get; set; }

    }

    public class FoodItem
    {
        public String id { get; set; }
        public String productName { get; set; }
        
        public float price { get; set; }

        public String description { get; set; }

        public bool avaliable { get; set; }

        public String image_url { get; set; }
    }

    public class CatalogueMovie
    {
        public string id { get; set; }
        public long movieId { get; set; } // API handles id's as ints
        public string movieTitle { get; set; }
        public string movieeDescription { get; set; }
        public int movieLength { get; set; }
        public double movieRating { get; set; }
        public DateTime releaseDate { get; set; }

    }

    public class ShowTime
    {
        public string id { get; set; }
        
        // Foreign Keys
        public string movieId { get; set; }
        public string movieName { get; set; }
        public long movieAPIid { get; set; }
        
        public string roomId { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        
        public int avaliableSitting { get; set; }
        
    }

    public class RoomShowTimesViewModel
    {
        public CinemaRoom cinemaRoom { get; set; }
        public List<ShowTime> showTimes { get; set; }
        
    }

    public class CinemaDashBoard
    {
        public int totalRooms { get; set; }

        public int availableFoodItems { get; set; }
        public int totalFoodItems { get; set; }
    }

    public class Order
    {
        public int cantidad { get; set; }
        public float precio { get; set; }
        public String producto { get; set; }
    }

}