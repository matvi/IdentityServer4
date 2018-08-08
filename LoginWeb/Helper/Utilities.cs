using LoginWeb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Helper
{
    public static class Utilities
    {
		public static CustomError GetError(int code, int httpCode, string message, string description)
		{
			CustomError error = new CustomError
			{
				Message = message,
				HttpCode = httpCode,
				Description = description,
				Code = code
			};
			return error;
		}
	}
}
