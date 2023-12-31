namespace AutoTrade.Web
{
	using System.Reflection;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity;

	using Data;
	using AutoTrade.Data.Models;
	using Services.Mapping;
	using Services.Data.Interfaces;
	using Web.ViewModels.Home;
	using Web.Infrstructure.Extensions;
	using Web.Infrastructure.ModelBinders;

	using static Common.GeneralApplicationConstants;
	using AutoTrade.Web.Hubs;

	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<AutoTradeDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
				options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
				options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
				options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
				options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
			})
				.AddRoles<IdentityRole<Guid>>()
				.AddEntityFrameworkStores<AutoTradeDbContext>();

			builder.Services.AddApplicationServices(typeof(ICarService));

			builder.Services.AddMemoryCache();

			builder.Services.ConfigureApplicationCookie(cfg =>
			{
				cfg.LoginPath = "/User/Login";
				cfg.AccessDeniedPath = "/Home/Error/401";
			});

			builder.Services
				.AddControllersWithViews()
				.AddMvcOptions(options =>
				{
					options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
					options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
				});
			//builder.Services.AddSignalR();

			WebApplication app = builder.Build();

			AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error/500");
				app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			if (app.Environment.IsDevelopment())
			{
				app.SeedAdministrator(DevelopmentAdminEmail);
			}

			app.UseEndpoints(config =>
			{
				config.MapControllerRoute(
					name: "areas",
					pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);

				app.MapDefaultControllerRoute();
				app.MapRazorPages();
			});

			//app.MapHub<ChatHub>("/Chat");

			app.Run();
		}
	}
}