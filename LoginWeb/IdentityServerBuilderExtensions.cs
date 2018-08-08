using LoginWeb.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb
{
	/**
	 * Extends functionality of the IdentitySErver.
	 * In order to use it, uncomment the AddUserStorage in the startup.cs on the (services.AddIdentityServer())
	 * 
	 * */
	public static class IdentityServerBuilderExtensions
    {
		public static IIdentityServerBuilder AddUserStorage(this IIdentityServerBuilder builder)
		{
			builder.AddProfileService<ProfileService>(); //Adds custom claims to the token
			builder.Services.AddScoped<IAuthRepository, AuthRepository>(); // Will allow verify the user to our own implementation
			return builder;
		}
    }
}
