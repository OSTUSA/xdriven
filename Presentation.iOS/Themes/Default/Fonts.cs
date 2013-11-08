using MonoTouch.UIKit;


namespace Presentation.iOS.Themes.Default
{
	public class Fonts
	{
		public Fonts ()
		{
		}

		const string FontName = "HelveticaNeue";
		const string BoldFontName = "HelveticaNeue-Bold";
		const string ItallicFontName =  "HelveticaNeue";

		/// <summary>
		/// Returns the default font with a certain size
		/// </summary>
		public static UIFont FontOfSize (float size)
		{
			return UIFont.FromName (FontName, size);
		}

		/// <summary>
		/// Returns the default font with a certain size
		/// </summary>
		public static UIFont BoldFontOfSize (float size)
		{
			return UIFont.FromName (BoldFontName, size);
		}

		/// <summary>
		/// Returns the default font itallicized with a certain size
		/// </summary>
		public static UIFont ItallicFontOfSize (float size)
		{
			return UIFont.FromName (ItallicFontName, size);
		}
	}
}

