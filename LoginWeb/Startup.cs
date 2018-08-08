using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using LoginWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LoginWeb.Model;
using System.IO;
using IdentityServer4.Validation;
using IdentityServer4.Services;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using LoginWeb.Helper;


namespace LoginWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) {
            //Configuration = configuration;
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

			Configuration = builder.Build();

                }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			//Register the appsettings.json section with the model ConfigurationManager
			services.Configure<ConfigurationManager>(Configuration.GetSection("ConfigurationManager"));

			//services.AddDbContext<DBContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
			/*
             * The AddDeveloperSigningCredential extension creates temporary key material for signing tokens. 
             * Again this might be useful to get started, but needs to be replaced by some persistent key material for production scenarios
             * */

			services
				.AddMvcCore() //Adding mvc with no extra functionallity as the addmvc does.
				.AddJsonFormatters()//able to work with json.
				.AddRazorViewEngine(); //Able to work with Views
			//services.AddMvc();
			//dependency injection
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
			services.AddScoped<IProfileService, ProfileService>();


			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryClients(Config.GetClients())
				.AddInMemoryIdentityResources(Config.GetIdentityResources());
				//.AddTestUsers(Config.GetUser()); //adds support for the resource owner password grant //this does 3 things: 1.- register the user store, 2.- add a profile service ( you can add claims) 3.- add a resourceOwnerValidator (where you can validate if a user exits in your own database) All this just for Resource Owner Password Credenitals (OAuth2 only) Flow
				//.AddUserStorage(); //Custom Static method from IdentityServerBuilder which allows to go to our own verification user. (Check class IdentityServerBuilderExtensions)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(builder =>
				{
					builder.Run(async context =>
					{
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						var error = context.Features.Get<IExceptionHandlerFeature>();
						if (error != null)
						{
							string jsonError = JsonConvert.SerializeObject(Utilities.GetError(123, (int)HttpStatusCode.InternalServerError, "Error", error.Error.Message));
							context.Response.ContentType = "application/json; charset=utf-8";
							context.Response.AddApplicationError(jsonError);
							await context.Response.WriteAsync(jsonError);

						}
					});
				});// this will add the global exception handle
			}
			/**
             * registers the IdentityServer services in DI. 
             * It also registers an in-memory store for runtime state. 
             * This is useful for development scenarios. 
             * For production scenarios you need a persistent or shared store like a database or cache for that. See the EntityFramework quickstart for more information.
            */
			app.UseCors(builder => // This leaves the site wide open for cross-origin requests. For production this code should limit CORS. Cross-Origin Resources Sharing (CORS) deals with sharing of restircted resources requested from outside the domain which made the request.
			{
				builder.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod();
			});
            app.UseIdentityServer();
			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
		/*	app.UseMvc(routes =>
			{
				//routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
				routes.MapRoute("default", "{controller=Account}/{action=Login}/{url}");
			});
			*/
		}
    }
}
