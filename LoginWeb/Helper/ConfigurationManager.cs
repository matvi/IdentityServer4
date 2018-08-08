using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * This class matches the appsettings.json so it can map the configuration file and extract the configuration in other classes
 * 
 * 
 * */
namespace LoginWeb.Helper
{
    public class ConfigurationManager
    {
		public string TSWCustomEndpoint { get; set; }
		public string TSWProxyEndpoint { get; set; }
		public ICollection<string> RedirectUris { get; set; }
		public ICollection<string> PostLogoutRedirectUris { get; set; }
		public ICollection<string> AllowedCorsOrigins { get; set; }
		public int AccessTokenLifeTime { get; set; }
	}
}
