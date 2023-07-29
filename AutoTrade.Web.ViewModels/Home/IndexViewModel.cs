using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Home
{
	public class IndexViewModel
	{
        public string Id { get; set; } = null!;
        public string Make { get; set; } = null!;
		public string Model { get; set; } = null!;
        public int Year { get; set; }
        public int Horsepower { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
