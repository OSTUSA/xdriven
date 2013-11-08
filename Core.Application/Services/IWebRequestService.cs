using System;
using System.Net;
using System.Collections.Generic;
using Core.Application.Models;

namespace Core.Application.Services
{
	public interface IWebRequestService
	{
		void SetBasicAuthHeader (WebRequest req, String userName, String userPassword);

		T GetDataSingle<T> (HttpWebRequest request) where T : new();
		T GetDataSingle<T> (HttpWebRequest request, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false) where T : new();
		List<T> GetData<T> (HttpWebRequest request);
		List<T> GetData<T> (HttpWebRequest request, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false);

		WebRequestServiceResponse PostData (HttpWebRequest request, object objectToPost);
		WebRequestServiceResponse PostData (HttpWebRequest request, object objectToPost, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false);
		T PostData<T> (HttpWebRequest request, object objectToPost);
		T PostData<T> (HttpWebRequest request, object objectToPost, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false);
		WebRequestServiceResponse PostData (HttpWebRequest request, string json);
		WebRequestServiceResponse PostData (HttpWebRequest request, string json, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false);
	}
}