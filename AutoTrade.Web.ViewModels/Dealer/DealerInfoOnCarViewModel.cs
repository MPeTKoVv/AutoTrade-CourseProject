using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Dealer
{
	public class DealerInfoOnCarViewModel
    {
        public string Email{ get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
    }
}
