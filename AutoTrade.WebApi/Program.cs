namespace AutoTrade.WebApi
{
    using Microsoft.EntityFrameworkCore;

    using Web.Data;
    using Web.Infrstructure.Extensions;
    using Services.Data.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AutoTradeDbContext>(opt =>
            opt.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(ICarService));

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("AutoTrade", policyBuilder =>
                {
                    policyBuilder.WithOrigins("https://localhost:7229")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("AutoTrade");

            app.Run();
        }
    }
}