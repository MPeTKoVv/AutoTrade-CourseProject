namespace AutoTrade.Web.ViewModels.Transaction
{
	using AutoMapper;
	using Data.Models;
	using Services.Mapping;

	public class TransactionViewModel : IMapFrom<Transaction>, IHaveCustomMappings
	{
		public string Id { get; set; } = null!;

		public string SellerId { get; set; } = null!;
		public string SellerFullName { get; set; } = null!;
		public string SellerEmail { get; set; } = null!;

		public string BuyerId { get; set; } = null!;
		public string BuyerFullName { get; set; } = null!;
		public string BuyerEmail { get; set; } = null!;

		public string CarMakeAndModel { get; set; } = null!;

		public decimal Amount { get; set; }

		public DateTime Date { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration
				.CreateMap<Transaction, TransactionViewModel>()
				.ForMember(d => d.SellerFullName, opt => opt.MapFrom(s => s.Seller.User.FirstName + " " + s.Seller.User.LastName))
				.ForMember(d => d.SellerEmail, opt => opt.MapFrom(s => s.Seller.User.Email))
				.ForMember(d => d.BuyerFullName, opt => opt.MapFrom(s => s.Buyer.FirstName + " " + s.Buyer.LastName))
				.ForMember(d => d.BuyerEmail, opt => opt.MapFrom(s => s.Buyer.Email))
				.ForMember(d => d.CarMakeAndModel, opt => opt.MapFrom(s => s.Car.Make + " " + s.Car.Model))
				.ForMember(d => d.Date, opt => opt.MapFrom(s => s.TransactionDate));
		}
	}
}
