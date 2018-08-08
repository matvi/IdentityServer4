using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWeb.Model
{
    public class CustomError
    
	{
     public int Code { get; set; }
	public int HttpCode { get; set; }
	public string Message { get; set; }
	public string Description { get; set; }
	//https://en.wikipedia.org/wiki/List_of_HTTP_status_codes
}
}
