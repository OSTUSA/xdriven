using MonoTouch.UIKit;
using CoreApplicationRegistrar = Core.Application.InterfaceRegistrar;
using PresentationRegistrar = Presentation.iOS.InterfaceRegistrar;

namespace Presentation.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// Setup our services for core library
			CoreApplicationRegistrar.Load ();
			PresentationRegistrar.Load ();

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
