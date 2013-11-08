using System;
using System.Net;
using Core.Application.Models;

namespace Core.Application.Broadcasters
{
	public interface INetworkEventBroadcaster
	{
		void RegisterNetworkErrorHandler (EventHandler<NetworkErrorEventArgs> handler);
		void RegisterErrorHandler(EventHandler<NetworkErrorEventArgs> handler);
		void RegisterNetworkUsageStart (EventHandler handler);
		void RegisterNetworkUsageEnd(EventHandler handler);

		void RaiseNetworkErrorEvent (WebException ex, bool suppress);
		void RaiseErrorEvent (Exception ex, bool suppress);
		void RaiseNetworkUsageStart ();
		void RaiseNetworkUsageEnd ();
	}
}

