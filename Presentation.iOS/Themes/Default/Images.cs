using System;
using MonoTouch.UIKit;


namespace Presentation.iOS.Themes.Default
{
	public class Images
	{
		public Images ()
		{
		}

		static Lazy<UIImage> _demoImage = new Lazy<UIImage> (() => UIImage.FromFile ("Images/garfield.jpg"));
		public UIImage DemoImage {
			get { return _demoImage.Value; }
		}
	}
}

