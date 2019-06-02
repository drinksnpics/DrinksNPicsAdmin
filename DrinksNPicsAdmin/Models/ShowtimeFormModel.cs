using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DrinksNPicsAdmin.Models
{
    public class ShowtimeFormModel
    {
        public List<SelectListItem> movies { get; set; }
        public List<SelectListItem> rooms { get; set; }
    }
}