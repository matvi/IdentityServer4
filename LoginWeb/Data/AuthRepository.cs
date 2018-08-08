using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using LoginWeb.Helper;
using LoginWeb.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace LoginWeb.Data
{
	public class AuthRepository : IAuthRepository

	{
		static HttpClient client = new HttpClient();
		readonly IOptions<ConfigurationManager> _configurations;


		public AuthRepository(IOptions<ConfigurationManager> configurations)
		{
			_configurations = configurations;
		}
		
		/**
		 * In here you need to implement the logic you need to get the User Role. Usually you go to a database and extract the userRole
		 * */
		public  List<Role> GetUserRoles(int? userId)
		{
			List<Role> roles = new List<Role>(
				new Role[]{
				new Role() { RoleID = 1, RoleName = "Admin", Inactive = true },
				new Role() { RoleID = 2, RoleName = "PaidUser", Inactive = true}
				}
			);
			return roles;
		}

		/**
		 * In here you need to implement the logic you need to validate the user credentials. Usuarlly you go to a database to validate the user.
		 * */

		public  User ValidateUserAsync(string username, string password) //it may have an error for being async when in the interface it is not!
		{
			if (username.Equals("david") && password.Equals("123")){
				return new User()
				{
					Username = username,
					Password = password,
					SubjectId = "11110",
					IsActive = true
				};
				
			}
			return null;
			
		}

		
	}
}
