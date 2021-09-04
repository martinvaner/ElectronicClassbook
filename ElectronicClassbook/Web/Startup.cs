using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess;
using DataAccess.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Hosting;
using Base;
using Base.Interfaces;
using DataAccess.Repository;
using DataAccess.Repository.Interfaces;
using Admin.Interfaces;
using Admin;
using Classbook.Interfaces;
using Classbook;
using Reports.Interfaces;
using Reports;
using Attendance.Interfaces;
using Attendance;
using jsreport.AspNetCore;
using jsreport.Local;
using jsreport.Binary;

namespace Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<EClassbookDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ElectronicClassbookDBConnection")));

			//Have to install NuGet for PostgreSQL in DataAccess project, have to create migrations for PostgreSQL in DataAccess project
			//services.AddDbContext<EClassbookDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection")));


			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => {
					options.LoginPath = "/Account/Login";
					options.AccessDeniedPath = "/Account/AccessDenied";
				});

			services.AddJsReport(new LocalReporting()
				.UseBinary(JsReportBinary.GetBinary())
				.AsUtility()
				.Create());

			services.AddControllersWithViews();

			services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
			services.AddTransient<IBaseRepository, BaseRepository>();
			services.AddTransient<IAdminRepository, AdminRepository>();
			services.AddTransient<IClassbookRepository, ClassbookRepository>();
			services.AddTransient<IReportsRepository, ReportsRepository>();
			services.AddTransient<IAttendanceRepository, AttendanceRepository>();

			services.AddTransient<IUserManager, UserManager>();
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<IAdminManager, AdminManager>();
			services.AddTransient<IClassbookManager, ClassbookManager>();
			services.AddTransient<Reports.Interfaces.IParentStudentManager, Reports.ParentStudentManager>();
			services.AddTransient<IReportManager, ReportManager>();
			services.AddTransient<Attendance.Interfaces.IParentManager, Attendance.ParentManager>();
			services.AddTransient<IClassTeacherManager, ClassTeacherManager>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCookiePolicy();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
