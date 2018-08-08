# IdentityServer4
IdentityServer4 is an OpenID Connect and OAuth2 framework for dotnet that helps us to create Authentication and Authorization Services and more. This repo contains an already functional Server and client that works with .Net Core 2.1


LoginWeb 
It´s the implementation of IdentityServer4.
Edit the AuthRepository.cs to implement you own user validation.


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
    
    
WebClient
(default port: https://localhost:44363)
Configure to use https and the 44363 port.
Its the client that will ask for the access_token and the identity_token to have access to profile information and to allow it to use protected Resources.


Client
Its a default client that uses the ClientCredetials flow.

For more information https://davidmatablog.wordpress.com/2018/07/19/securing-dotnetcore-2-1-web-api-with-identityserver4/
