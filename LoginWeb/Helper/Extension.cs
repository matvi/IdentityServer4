using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Helper
{
    public static class Extension
    {
		public static void AddApplicationError(this HttpResponse response, string error)
		{
			response.Headers.Add("Application-Error", error); //Add to the headers Application-Error
			response.Headers.Add("Access-Control-Expose-Headers", "Application-Error"); //we expose the header that we just created "application-Error"
			response.Headers.Add("Access-Control-Allow-Origin", "*"); //any origin will have access to this header
		}
	}
}
