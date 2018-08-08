using Microsoft.AspNetCore.Mvc;

namespace LoginWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/LogIn")]
    public class LogInController : Controller
    {
		[HttpGet]
		public ActionResult Login()
		{
			return Ok("Login ok");
		}

		
	}
}