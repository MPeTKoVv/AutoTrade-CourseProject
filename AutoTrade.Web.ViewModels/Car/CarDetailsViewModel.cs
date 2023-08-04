using AutoTrade.Web.ViewModels.Dealer;
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

		public string Condition { get; set; } = null!;

        public string EngineType { get; set; } = null!;

		public int Mileage { get; set; }

		public DealerInfoOnCarViewModel Dealer { get; set; } = null!;
	}
}
