using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Extensions
{
	public static class ListExtensions
	{
		public static string ToArrayString<T>(this List<T> list) where T : new() {
			StringBuilder sb = new StringBuilder ();

			int i = 0;
			foreach (var item in list) {
				if (i != 0) {
					sb.Append (",");
				}

				sb.Append (item);
				i++;
			}

			return sb.ToString ();
		}
	}
}

