
namespace Presentation.iOS
{
	public class InterfaceRegistrar
	{
		public InterfaceRegistrar ()
		{
		}

		/// <summary>
		/// Call on startup of the app, it configures ServiceContainer
		/// </summary>
		public static void Load ()
		{
			#region REPOSITORY INJECTION

			//ServiceContainer.Register<IRepository<UserSetting>> (() => new SQLiteRepository<UserSetting>());
			//ServiceContainer.Register<ILogIntegrationRepository> (() => new LogIntegrationRepository(Settings.WebApiRootUri));
			//ServiceContainer.Register<IRepository<SETPoint>> (() => new SQLiteRepository<SETPoint>()); 

			#endregion

			#region SERVICE INJECTION

			//Injector.Register<ILoggingService> (() => new LoggingService ());
			//Injector.Register<INetworkEventBroadcaster> (() => new NetworkEventBroadcaster ());

			#endregion


			#if !NETFX_CORE

			//Only do these on iOS or Android

			#endif
		}
	}
}
