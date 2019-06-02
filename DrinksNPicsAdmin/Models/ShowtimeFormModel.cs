using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesDBModels;

namespace DrinksNPicsAdmin.Models
{
    public class ShowtimeFormModel
    {
        public string roomId { get; set; }
        public string movieId { get; set; }
        
        public DateTime startDate { get; set; }
        public List<SelectListItem> movies { get; set; }
        public List<SelectListItem> rooms { get; set; }
    }
}