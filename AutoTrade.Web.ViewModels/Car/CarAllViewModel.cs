using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Car
{
	public class CarAllViewModel
	{
		public string Id { get; set; } = null!;
		public string Make { get; set; } = null!;
		public string Model { get; set; } = null!;
		public decimal Price { get; set; }
        public int Year { get; set; }
        public int Horsepower { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsForSale { get; set; }
    }
}
