using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DrinksNPicsAdmin.Services;
using MoviesDBModels;
using MoviesDBModels.Providers;
using NUnit.Framework;

namespace DrinksNPicsUT
{
    [TestFixture]
    public class Tests
    {
        private static readonly MovieProvider _movieService = new MovieProvider();
        private static readonly CinemaService _cinemaService = new CinemaService();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestApi()
        {
            Movie movie = await _movieService.GetMovieInfo(CancellationToken.None,  299534);
            Assert.That(movie.original_title, Is.EqualTo("Avengers: Endgame"));
        }

        [Test]
        public async Task TestFirebaseRooms()
        {
            CinemaRoom room1 = await _cinemaService.GetCinemaRoom("f5199186-9d85-4788-a8c9-a8ffc0129516");
            Assert.That(room1.Id, Is.EqualTo("f5199186-9d85-4788-a8c9-a8ffc0129516"));
            Assert.That(room1.Capacity, Is.EqualTo(50));
            Assert.That(room1.RoomNumber, Is.EqualTo(1));
        }
        
        [Test]
        public async Task TestCatalogueAndApi()
        {
            CatalogueMovie movieCatalogue = await _cinemaService.GetMovieFromCatalogue("-LgJx804gL-RZD_KzpdI");
            Movie movieAPI = await _movieService.GetMovieInfo(CancellationToken.None, 299534);
            Assert.That(movieCatalogue.movieLength, Is.EqualTo(movieAPI.runtime));
            Assert.That(movieCatalogue.releaseDate.Date, Is.EqualTo(DateTime.Parse(movieAPI.release_date).Date));
            Assert.That(movieCatalogue.movieRating, Is.EqualTo(movieAPI.vote_average));
        }
        
        [Test]
        public async Task TestOrder()
        {
            List<FoodItem> itemstoBuy = new List<FoodItem>();
            float unitPrice = 0;
            for (int i = 0; i < 10; i++)
            {
                FoodItem item = await _cinemaService.GetSnackProduct("-LgFXmrcfjPKhYFANQb7");
                Assert.That(item.productName, Is.EqualTo("Coke"));
                itemstoBuy.Add(item);
                unitPrice = item.price;

            }

            float total = 0.0f;
            
            foreach (var item in itemstoBuy)
            {
                total += item.price;
            }
            
            Assert.That(total, Is.EqualTo(unitPrice * 10).Within(0.01f));
        }
    }
}