using System.Collections.Generic;
using LiteDB;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesDBModels;

namespace DrinksNPicsAdmin.Models
{
    public class ShowtimeFormModel
    {
        public ShowTime showTime { get; set; }
        public List<SelectListItem> movies { get; set; }
        public List<SelectListItem> rooms { get; set; }
    }
}