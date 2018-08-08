using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebClient
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

			//we’ve turned off the JWT claim type mapping to allow well-known claims (e.g. ‘sub’ and ‘idp’) to flow through unmolested
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services
			.AddAuthentication(options => //adds the authentication services to DI
			{
				//We are using a cookie as the primary means to authenticate a user (via "Cookies" as the DefaultScheme). We set the DefaultChallengeScheme to "oidc" because when we need the user to login, we will be using the OpenID Connect scheme.
				options.DefaultScheme = "Cookies";
				options.DefaultChallengeScheme = "oidc";
			})
			.AddCookie("Cookies")       //add the handler that can process cookies.
			.AddOpenIdConnect("oidc", options => //configure the handler that perform the OpenID Connect protocol. 
			{
				options.SignInScheme = "Cookies"; //is used to issue a cookie using the cookie handler once the OpenID Connect protocol is complete
				options.Authority = "http://localhost:5000/"; //indicates that we are trusting IdentityServer.
				options.RequireHttpsMetadata = false;
				options.ClientId = "vl";// the client_id that is defined in Config.cs file on IdentityServer
				options.SaveTokens = true;
				options.ClientSecret = "secret"; //used to persist the tokens from IdentityServer in the cookie
				options.ResponseType = "code id_token token"; //We are specifying that we want to use the hybrin flow. requests an authorization code and identity token
				options.Scope.Add("roles");

			});
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Adds HttpContextAccessor to use it to get access token when trying to reach an API
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
			app.UseAuthentication(); // to ensure the authentication services execute on each request
			
			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
