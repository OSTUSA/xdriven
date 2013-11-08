using System;
using MonoTouch.UIKit;


namespace Presentation.iOS.Themes.Default
{
	public class Colors
	{
		public Colors ()
		{
		}

		static Lazy<UIColor> _demoColor = new Lazy<UIColor> (() => UIColor.FromRGB(170, 170, 170));
		public static UIColor DemoColor {
			get { return _demoColor.Value; }
		}
	}
}

