﻿using AutoTrade.Data.Models;
using AutoTrade.Web.ViewModels.Car.Enums;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrade.Web.ViewModels.Car
{
    using static Common.GeneralApplicationConstants;

	public class AllCarsQueryModel
	{
        public AllCarsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.CarsPerPage = EntitiesPerPage;

            this.Categories = new HashSet<string>(); 
            this.Conditions = new HashSet<string>();
            this.EngineTypes = new HashSet<string>();
		}

		public string? Category { get; set; }

        public string? Condition { get; set; }

        [Display(Name = "Engine type")]
        public string? EngineType { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchString { get; set; }

		[Display(Name = "Car sorting")]
		public CarSorting CarSorting { get; set; }

        public int CurrentPage { get; set; }

		[Display(Name = "Cars per page")]
		public int CarsPerPage { get; set; }

        public int TotalCars { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Conditions { get; set; }

        public IEnumerable<string> EngineTypes { get; set; }

        public IEnumerable<CarAllViewModel> Cars { get; set; }
    }
}