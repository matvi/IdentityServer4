using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Model
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

		public bool IsActive { get; set; }
		public string SubjectId { get; set; } //user Id following the IdentityServer convection
	}
}
