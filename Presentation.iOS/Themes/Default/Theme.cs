using MonoTouch.UIKit;
using Core.Application.Injection;


namespace Presentation.iOS.Themes.Default
{
	public class Theme
	{
		#region PROPERTIES

		private Images _images;
		public Images Images {
			get {
				if (_images == null)
					_images = new Images ();

				 return _images;
			}
		}

		private Colors _colors;
		public Colors Colors {
			get {
				if (_colors == null)
					_colors = new Colors ();

				return _colors;
			}
		}

		private Fonts _fonts;
		public Fonts Fonts {
			get {
				if (_fonts == null)
					_fonts = new Fonts ();

				return _fonts;
			}
		}

		#endregion

		#region SINGLETON

		private static Theme _instance;
		/// <summary>
		/// Singleton instance
		/// </summary>
		/// <value>The instance.</value>
		public static Theme Instance 
		{
			get
			{
				if(_instance == null) {
					_instance = new Theme ();
				}

				return _instance;
			}
			private set {
				_instance = value;
			}
		}

		#endregion

		#region CTOR

		public Theme ()
		{

		}

		#endregion

		#region STATIC METHODS

		/// <summary>
		/// Apply UIAppearance to this application, this is iOS's version of "styling"
		/// </summary>
		public static void Apply ()
		{
			// Sets the loading indicator color (the spinner)
			//UIActivityIndicatorView.Appearance.Color = Colors.IndicatorColor;
			// Sets the status bar text to white
			//UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
//
//			#region Navigation bar and buttons
//
//			var navBarLabelStyle = UILabel.AppearanceWhenContainedIn (typeof(UINavigationBar));
//			var navBarStyle = UINavigationBar.Appearance;
//			var tabBarStyle = UITabBar.Appearance;
//
//			var titleTextAttributes = new UITextAttributes () {
//				Font = Theme.Fonts.FontOfSize (18f),
//				TextColor = UIColor.White
//			};
//
//			navBarStyle.SetTitleTextAttributes (titleTextAttributes);
//
//			// Only apply the following if it's pre-iOS7
//			//		This is just to match the default iOS7 style buttons
//			if (UIDevice.CurrentDevice.CheckSystemVersion (7, 0))
//			{
//				// iOS 7 Only stuff
//				navBarStyle.SetBackgroundImage (Theme.Images.NavBarBackgroundiOS7, UIBarMetrics.Default);
//
//				// Set nav bar button text
//				navBarStyle.TintColor = Colors.Navigation.Font;
//
//				// Set up the tabbar so it's dark
//				tabBarStyle.TintColor = Colors.Navigation.Font;
//				tabBarStyle.BarTintColor = UIColor.Black;
//				tabBarStyle.BackgroundColor = UIColor.Black;
//
//
//			} else {
//				// Pre-iOS 7 styles
//				navBarStyle.SetBackgroundImage (Theme.Images.NavBarBackgroundLandscapeiOS6, UIBarMetrics.LandscapePhone);
//				navBarStyle.SetBackgroundImage (Theme.Images.NavBarBackground, UIBarMetrics.Default);
//
//				navBarLabelStyle.TextColor = Colors.DarkBackgroundTextColor;
//
//				var buttonTextAttributes = new UITextAttributes () {
//					Font = Theme.Fonts.FontOfSize (16f),
//					TextColor = Theme.Colors.Navigation.Font
//				};
//
//				// Sets up the navbar button styles to match the iOS 7 look
//				var navBarButtonStyle = UIBarButtonItem.AppearanceWhenContainedIn (typeof(UINavigationBar));
//				navBarButtonStyle.TintColor = Colors.Navigation.Font;
//				navBarButtonStyle.SetTitleTextAttributes (buttonTextAttributes, UIControlState.Normal);
//				navBarButtonStyle.SetBackgroundImage (Images.NavBarButtonBackground, UIControlState.Normal, UIBarMetrics.Default);
//				navBarButtonStyle.SetBackButtonBackgroundImage (Images.NavBarButtonBackground, UIControlState.Normal, UIBarMetrics.Default);
//
//			}
//
//			#endregion
		}

		// <summary>
		// Transitions a controller to the rootViewController, for a fullscreen transition
		// </summary>
		public static void TransitionController (UIViewController controller, bool animated = true)
		{
			var window = Injector.Resolve<UIWindow> ();

			//Return if it's already the root controller
			if (window.RootViewController == controller)
				return;

			//Set the root controller
			window.RootViewController = controller;

			//Peform an animation, note that null is not allowed as a callback, so I use delegate { }
			if (animated)
				UIView.Transition (window, .3, UIViewAnimationOptions.TransitionCrossDissolve, delegate { }, delegate { });
		}

		#endregion
	}
}

