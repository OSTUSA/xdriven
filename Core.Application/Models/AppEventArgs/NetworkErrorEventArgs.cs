using System;

namespace Core.Application.Models
{
	public class NetworkErrorEventArgs : EventArgs {
		public NetworkErrorEventArgs(Exception ex, bool suppress) {
			Exception = ex;
			Suppress = suppress;
		}

		public Exception Exception { get; set; }
		public bool Suppress { get; set; }
	}
}

