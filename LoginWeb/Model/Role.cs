using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Model
{
    public class Role
    {
		public int RoleID { get; set; }
		public string RoleName { get; set; }
		public bool Inactive { get; set; }
	}
}
