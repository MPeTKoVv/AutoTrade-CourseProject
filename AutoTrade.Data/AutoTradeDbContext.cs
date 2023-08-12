namespace AutoTrade.Web.Data
{
	using System.Reflection;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	
    using AutoTrade.Data.Models;

	public class AutoTradeDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AutoTradeDbContext(DbContextOptions<AutoTradeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

		public DbSet<EngineType> EngineTypes { get; set; } = null!;

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<Seller> Sellers { get; set; } = null!;

        public DbSet<Transmission> Transmissions { get; set; } = null!;

        public DbSet<Transaction> Transactions { get; set; } = null!;

        public DbSet<Wallet> Wallets { get; set; } = null!;

        public DbSet<CreditCard> CreditCards { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssebly = Assembly.GetAssembly(typeof(AutoTradeDbContext)) ??
                                     Assembly.GetExecutingAssembly();
            
            builder.ApplyConfigurationsFromAssembly(configAssebly);

            base.OnModelCreating(builder);
        }
    }
}