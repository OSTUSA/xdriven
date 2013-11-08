using System;
using System.Net;

namespace Core.Application.Models
{
	public class WebRequestServiceResponse
	{
		public WebRequestServiceResponse ()
		{
		}

		public WebRequestServiceResponse (object data, HttpStatusCode statusCode, string statusDescription)
		{
			Data = data;
			StatusCode = statusCode;
			StatusDescription = statusDescription;
		}

		public object Data { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string StatusDescription { get; set; }
	}
}

