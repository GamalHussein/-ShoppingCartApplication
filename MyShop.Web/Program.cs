using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Data;
using MyShop.DataAccess.Implementation;
using MyShop.Entities.Repositores;
using Microsoft.AspNetCore.Identity;
using Utilties;
using Microsoft.AspNetCore.Identity.UI.Services;
using MyShop.Entities.Models;

namespace MyShop.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddRazorPages()
				.AddRazorRuntimeCompilation();
			builder.Services.AddDbContext<ApplicationDbContext>(
				option=>option.UseSqlServer(
					builder.Configuration.GetConnectionString("DefaultConnection")));

   builder.Services.AddIdentity<IdentityUser,IdentityRole>(
	             option=>option.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromHours(4))
				.AddDefaultTokenProviders()
				.AddDefaultUI()
				.AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddSingleton<IEmailSender, emailSendr>();

			var app = builder.Build();


			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseAuthorization();
			app.MapRazorPages();

			app.MapStaticAssets();
			app.MapControllerRoute(
				name: "default",
				pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}")
				.WithStaticAssets();
			app.MapControllerRoute(
				name: "Customer",
				pattern: "{area=Customer}/{controller=Category}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}
