using AutoTrade.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace AutoTrade.Web.Data
{
    public class AutoTradeDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AutoTradeDbContext(DbContextOptions<AutoTradeDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Condition> Conditions { get; set; } = null!;

		public DbSet<EngineType> EngineTypes { get; set; } = null!;

		public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Car> Cars { get; set; } = null!;

        public DbSet<Seller> Sellers { get; set; } = null!;

        public DbSet<Transmission> Transmissions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssebly = Assembly.GetAssembly(typeof(AutoTradeDbContext)) ??
                                     Assembly.GetExecutingAssembly();
            
            builder.ApplyConfigurationsFromAssembly(configAssebly);

            base.OnModelCreating(builder);
        }
    }
}