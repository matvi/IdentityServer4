using LoginWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Data
{
    public interface IAuthRepository
    {
		User ValidateUserAsync(string username, string password);

		List<Role> GetUserRoles(int? userId);
    }
}
