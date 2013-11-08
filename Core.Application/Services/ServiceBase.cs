using Core.Application.Injection;


namespace Core.Application.Services
{
	public abstract class ServiceBase
	{
		public ServiceBase ()
		{
		}

		private ILoggingService _loggingService;
		/// <summary>
		/// Lazy loaded log service
		/// </summary>
		/// <value>The log service.</value>
		protected ILoggingService LoggingService
		{
			get{
				if (_loggingService == null)
					_loggingService = Injector.Resolve<ILoggingService> ();

				return _loggingService;
			}
		}
	}
}

