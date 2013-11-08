using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Core.Application.Injection;


namespace Presentation.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		private UIWindow _window;
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// If you have defined a root view controller, set it here:
			// window.RootViewController = myViewController;

			// Register the main window
			Injector.Register (_window);

			//Apply our UI theme
			Themes.Default.Theme.Apply ();


			// make the window visible
			_window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

