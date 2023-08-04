using AutoTrade.Web.ViewModels.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Car
{
	public class CarDetailsViewModel : CarAllViewModel
	{
		public string Description { get; set; } = null!;

		public string Category { get; set; } = null!;

		public string EngineType { get; set; } = null!;

		public string Transmission { get; set; } = null!;

		public int Mileage { get; set; }

		public SellerInfoOnCarViewModel Seller { get; set; } = null!;
	}
}
