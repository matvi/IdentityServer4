using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
		private readonly IHttpContextAccessor _httpContextAccessor;
		private HttpClient _httpClient = new HttpClient();
		public AccessController(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<IActionResult> CallToolsAPI()
		{
			var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
			if (!string.IsNullOrWhiteSpace(accessToken))

			{
				_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
			}
			/*_httpClient.BaseAddress = new Uri("toosapi");
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept.Add(
				new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));*/
			return Ok(accessToken);

		}
    }
}