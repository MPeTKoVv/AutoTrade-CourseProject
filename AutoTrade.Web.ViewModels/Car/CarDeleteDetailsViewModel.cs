using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Car
{
    public class CarDeleteDetailsViewModel
    {
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

    }
}
