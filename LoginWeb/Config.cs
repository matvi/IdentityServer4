using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using LoginWeb.Helper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


/**
 * Since we are using the in-memory configuration for this walkthrough - all you need to do to add an API, is to create an object of type ApiResource and set the appropriate properties.
 * */
namespace LoginWeb
{
    public class Config
    {
	
		//Defining the API
		public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
			{
				new ApiResource("api1", "My API1",new List<string>{ "role" }), // by adding the role, we ensure that the access_token returned contains the role claim
				
				
			};
        }

		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResource("roles", "Your role(s)", new List< string>(){ "role" }) //We add a new claim to ask for when Conset page is shown
			};
		}
        //Defining the client
        /**
         * The next step is to define a client that can access this API.
           For this scenario, the client will not have an interactive user, and will authenticate using the so called client secret with IdentityServer
         **/

        public static IEnumerable<Client> GetClients()
        {
			return new List<Client>
			{
				 new Client
				{
					ClientId = "ro.client",
					//The resource owner password grant type allows to request tokens on behalf of a user by sending the user’s name and password to the token endpoint.
					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //Grant types are a way to specify how a client wants to interact with IdentityServer.
					ClientSecrets =
					{
						new Secret ("secret".Sha256())
					},
					AllowedScopes = { "api1", "tswTools" },
					
				},

				 new Client
				 {
					 ClientId = "ng",
					 ClientName = "Angular Client",
					 //The implicit grant type is optimized for browser-based applications. 
					 //Either for user authentication-only (both server-side and JavaScript applications), 
					 //or authentication and access token requests (JavaScript applications).
					 //In the implicit flow, all tokens are transmitted via the browser, and advanced features like refresh tokens are thus not allowed.
					 //AllowedGrantTypes = GrantTypes.Implicit, //Grant types are a way to specify how a client wants to interact with IdentityServer.
					 AllowedGrantTypes = GrantTypes.Implicit,
					 AllowAccessTokensViaBrowser = true,
					 RequireConsent = true,

					 RedirectUris = {
						 "https://localhost:44363/signin-oidc",
						 "http://localhost:4200/auth.html",
						 "http://localhost:4200/silent-renew.html",
					 }, //specifies allowed URIS to return the token (the angular client)
					 PostLogoutRedirectUris = { "http://localhost:4200/"}, //specifies allowed URIs to redirect when log out
					 AllowedCorsOrigins = { "http://localhost:4200"}, //Cross-Origin Resource Sharing (CORS) deals with sharing of restricted resources requested from outside the domain which made the request. 
					
                    // secret for authentication
                    ClientSecrets =
					{
						new Secret("secret".Sha256())
					},
					 // There are 4 standar socpes predefined in OpneID Connect: profile, email, address and phone
					 AllowedScopes =
					 {
						 IdentityServerConstants.StandardScopes.OpenId, //Specifies to the server that an openId request is being requested
						 IdentityServerConstants.StandardScopes.Profile, //This scope is used to request End-User default prifile claims (gender, birthday, profile, pcture, website, etc)
						 "api1",
						 "roles",
					 },
					 AccessTokenLifetime = 3600
				 },

				new Client
                {
                    ClientId = "client",

                    // No interactive user, use the clientid/secret for authentication
					//This is the simplest grant type and is used for server to server communication - tokens are always requested on behalf of a client, not a user.
                    AllowedGrantTypes = GrantTypes.ClientCredentials, //Grant types are a way to specify how a client wants to interact with IdentityServer.

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1", "otherapi" }
                },

				//hybrid flow
				//the hybrid flow is the best fit for a server side web application
				new Client
				{
					ClientName = "WebClient Login", //This is the name that is going to appear in the consesus screen when Loggin
					ClientId = "vl", //the identifier of the client
					 AllowAccessTokensViaBrowser = true,
					AllowedGrantTypes = GrantTypes.Hybrid,
					// the hybrid flow uses the redirection base, the token are delivered to the broser in the URI via redirection
					// that means that we need to add a valid URI this client is allowed to receive tokens
					RedirectUris = new List<string>()
					{
						"https://localhost:44363/signin-oidc"  //the host address of our mvc Web_client
					},
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					//The scopes that are allowed to be requested
					AllowedScopes = {
						IdentityServerConstants.StandardScopes.OpenId, //Specifies to the server that an openId request is being requested
						 IdentityServerConstants.StandardScopes.Profile, //This scope is used to request End-User default prifile claims (gender, birthday, profile, pcture, website, etc)
						 "api1",
						 "roles",
					},
					AccessTokenLifetime = 3600


				}

                
               
            };
        }

		
        public static List<TestUser> GetUser()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

					Claims = new List<Claim>
					{
						new Claim("role","paidUser")
					}
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password="password",
					Claims = new List<Claim>
					{
						new Claim("role","paidUser")
					}
				}
            };
        }
    }
}
