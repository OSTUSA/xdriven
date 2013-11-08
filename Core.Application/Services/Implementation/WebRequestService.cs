using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Core.Application.Broadcasters;
using Core.Domain.Models;
using Core.Application.Models;
using Core.Application.Injection;

namespace Core.Application.Services
{
	public class WebRequestService : ServiceBase, IWebRequestService
	{
		#region PROPERTIES

		private readonly INetworkEventBroadcaster _networkEventBroadcaster;
		private readonly string _requestBaseUrl;

		#endregion

		#region CTOR

		public WebRequestService (string requestBaseUri)
		{
			_networkEventBroadcaster = Injector.Resolve<INetworkEventBroadcaster> ();
			_requestBaseUrl = requestBaseUri;
		}

		#endregion

		#region INTERFACE IMPLEMENTATION

		public void SetBasicAuthHeader(WebRequest req, String userName, String userPassword)
		{
			string authInfo = userName + ":" + userPassword;
			authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
			req.Headers["Authorization"] = "Basic " + authInfo;
		}

		public T GetDataSingle<T>(HttpWebRequest request) where T : new()
		{
			return GetDataSingle<T>(request, false, true);
		}

		public T GetDataSingle<T>(HttpWebRequest request, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false) where T : new()
		{
			var data = default(T);

			try{
				_networkEventBroadcaster.RaiseNetworkUsageStart();

				using (HttpWebResponse response = (HttpWebResponse) request.GetResponse()) {
					if (response.StatusCode != HttpStatusCode.OK) {
						// Handle error response
						return default(T);
					} else {
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream())) {
							var content = streamReader.ReadToEnd ();
							if (string.IsNullOrWhiteSpace (content)) {
								//TODO: Handle empty response
							} else {
								// Convert response to model
								data = DeserializeSingle<T> (content);
							}
						}
					}
				}
			}
			catch (WebException ex) {
				if (!suppressNetworkErrors) {
					_networkEventBroadcaster.RaiseNetworkErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			catch (Exception ex) {
				if (!suppressExceptions) {
					_networkEventBroadcaster.RaiseErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			finally {
				_networkEventBroadcaster.RaiseNetworkUsageEnd ();
			}

			return data;
		}

		public List<T> GetData<T>(HttpWebRequest request)
		{
			return GetData<T>(request, false, true);
		}

		public List<T> GetData<T>(HttpWebRequest request, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false)
		{
			var data = new List<T> ();

			try {
				_networkEventBroadcaster.RaiseNetworkUsageStart ();

				using (HttpWebResponse response = (HttpWebResponse) request.GetResponse()) {
					if (response.StatusCode != HttpStatusCode.OK) {
						// Handle error response
						return null;
					} else {
						using (StreamReader streamReader = new StreamReader(response.GetResponseStream())) {
							var content = streamReader.ReadToEnd ();
							if (string.IsNullOrWhiteSpace (content)) {
								//TODO: Handle empty response
							} else {
								// Convert response to models
								data = DeserializeList<T> (content);
							}
						}
					}
				}
			}
			catch (WebException ex) {
				if (!suppressNetworkErrors) {
					_networkEventBroadcaster.RaiseNetworkErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			catch (Exception ex) {
				if (!suppressExceptions) {
					_networkEventBroadcaster.RaiseErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			finally {
				_networkEventBroadcaster.RaiseNetworkUsageEnd ();
			}

			return data;
		}


		public WebRequestServiceResponse PostData(HttpWebRequest request, object objectToPost)
		{
			return PostData (request, objectToPost, false, true);
		}

		public WebRequestServiceResponse PostData(HttpWebRequest request, 
		                                          object objectToPost, 
		                                          bool suppressNetworkErrors, 
		                                          bool suppressExceptions, 
		                                          bool notifyUser = false)
		{
			var response = new WebRequestServiceResponse ();

			try {
				_networkEventBroadcaster.RaiseNetworkUsageStart ();

				var errorInfo = SerializeObject(objectToPost);

				using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
					streamWriter.Write (errorInfo);
					streamWriter.Flush ();
					streamWriter.Close ();

					using (HttpWebResponse webResponse = (HttpWebResponse) request.GetResponse()) {
						response = new WebRequestServiceResponse(null, webResponse.StatusCode, webResponse.StatusDescription);
						// Nothing else to do here
					}
				}
			}
			catch (WebException ex) {
				if (!suppressNetworkErrors) {
					_networkEventBroadcaster.RaiseNetworkErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			catch (Exception ex) {
				if (!suppressExceptions) {
					_networkEventBroadcaster.RaiseErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			finally {
				_networkEventBroadcaster.RaiseNetworkUsageEnd ();
			}

			return response;
		}

		public T PostData<T> (HttpWebRequest request, object objectToPost) {
			return PostData<T>(request, objectToPost, false, true);
		}

		public T PostData<T> (HttpWebRequest request, object objectToPost, bool suppressNetworkErrors, bool suppressExceptions, bool notifyUser = false)
		{
			T data = default(T);

			try {
				_networkEventBroadcaster.RaiseNetworkUsageStart ();

				using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
					streamWriter.Write (SerializeObject(objectToPost));
					streamWriter.Flush ();
					streamWriter.Close ();

					using (HttpWebResponse webResponse = (HttpWebResponse) request.GetResponse()) {
						if (webResponse.StatusCode == HttpStatusCode.OK) {
							using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
								var content = streamReader.ReadToEnd ();
								if (!string.IsNullOrWhiteSpace (content)) {
									data = DeserializeSingle<T> (content);
								}
							}
						}
					}
				}
			}
			catch (WebException ex) {
				if (!suppressNetworkErrors) {
					_networkEventBroadcaster.RaiseNetworkErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			catch (Exception ex) {
				if (!suppressExceptions) {
					_networkEventBroadcaster.RaiseErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			finally {
				_networkEventBroadcaster.RaiseNetworkUsageEnd ();
			}

			return data;
		}

		public WebRequestServiceResponse PostData (HttpWebRequest request, string json) {
			return PostData (request, json, false, true);
		}

		public WebRequestServiceResponse PostData (HttpWebRequest request, 
		                                           string json, 
		                                           bool suppressNetworkErrors, 
		                                           bool suppressExceptions, 
		                                           bool notifyUser = false)
		{
			var response = new WebRequestServiceResponse ();

			try {
				_networkEventBroadcaster.RaiseNetworkUsageStart ();

				using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
					streamWriter.Write (json);
					streamWriter.Flush ();
					streamWriter.Close ();

					using (HttpWebResponse webResponse = (HttpWebResponse) request.GetResponse()) {
						response = new WebRequestServiceResponse(null, webResponse.StatusCode, webResponse.StatusDescription);

						if (webResponse.StatusCode == HttpStatusCode.OK) {
							using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream())) {
								var content = streamReader.ReadToEnd ();
								if (!string.IsNullOrWhiteSpace (content)) {
									response.Data = content;
								}
							}
						}
					}
				}
			}
			catch (WebException ex) {
				if (!suppressNetworkErrors) {
					_networkEventBroadcaster.RaiseNetworkErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			catch (Exception ex) {
				if (!suppressExceptions) {
					_networkEventBroadcaster.RaiseErrorEvent (ex, notifyUser);
				}

				if (!suppressExceptions) {
					throw;
				}
			}
			finally {
				_networkEventBroadcaster.RaiseNetworkUsageEnd ();
			}

			return response;
		}

		#endregion

		#region PRIVATE METHODS

		private HttpWebRequest GetRequest (string controller, string action, User user = null)
		{
			return GetRequest (controller, action, null, user);
		}

		private HttpWebRequest GetRequest (string controller, string action, string qsParams, User user = null)
		{
			var uri = (qsParams == null)
				? string.Format ("{0}/{1}/{2}", _requestBaseUrl, controller, action)
					: string.Format ("{0}/{1}/{2}?{3}", _requestBaseUrl, controller, action, qsParams);

			var request = (HttpWebRequest)HttpWebRequest.Create (uri);
			request.ContentType = "application/json";
			request.Method = "GET";

			if (user != null)
				SetBasicAuthHeader (request, user.UserName, user.Password);

			return request;
		}

		private HttpWebRequest GetPostRequest (string controller, string action, User user = null)
		{
			var uri = string.Format ("{0}/{1}/{2}", _requestBaseUrl, controller, action);

			var request = (HttpWebRequest)HttpWebRequest.Create (uri);
			request.ContentType = "application/json; charset=utf-8";
			request.Method = "POST";

			if (user != null)
				SetBasicAuthHeader (request, user.UserName, user.Password);

			return request;
		}

		private T DeserializeSingle<T>(string json)
		{
			return JsonConvert.DeserializeObject<T> (json);
		}

		private List<T> DeserializeList<T>(string json)
		{
			return JsonConvert.DeserializeObject<List<T>> (json);
		}

		private string SerializeObject (object items)
		{
			return JsonConvert.SerializeObject (items);
		}

		#endregion


	}
}

