using System;
using System.Net;
using Core.Application.Models;

namespace Core.Application.Broadcasters
{
	public class NetworkEventBroadcaster
	{
		#region PROPERTIES

		private object _networkStartEndLock = new object();

		#endregion

		#region CTOR

		public NetworkEventBroadcaster ()
		{

		}

		#endregion

		#region EVENTS

		/// <summary>
		/// Exceptions occuring during network transactions are communicated
		/// via event so the application can handle them in one central area
		/// instead on each individual call in the application. This is useful 
		/// for handling situations where network connectivity becomes unavailable.
		/// </summary>
		static event EventHandler<NetworkErrorEventArgs> OnNetworkError;
		static event EventHandler<NetworkErrorEventArgs> OnError;
		static event EventHandler OnNetworkUsageStart;
		static event EventHandler OnNetworkUsageEnd;

		#endregion

		#region INTERFACE IMPLEMENTATION

		void RaiseNetworkErrorEvent (WebException ex, bool suppress)
		{
			if (OnNetworkError != null) {
				OnNetworkError (this, new NetworkErrorEventArgs(ex, suppress));
			}
		}

		void RaiseErrorEvent (Exception ex, bool suppress)
		{
			if (OnError != null) {
				OnError (this, new NetworkErrorEventArgs(ex, suppress));
			}
		}

		public void RaiseNetworkUsageStart ()
		{
			lock (_networkStartEndLock) {
				if (OnNetworkUsageStart != null) {
					OnNetworkUsageStart (null, null);
				}
			}
		}

		public void RaiseNetworkUsageEnd ()
		{
			lock (_networkStartEndLock) {
				if (OnNetworkUsageEnd != null) {
					OnNetworkUsageEnd (null, null);
				}
			}
		}

		#endregion
	}
}
