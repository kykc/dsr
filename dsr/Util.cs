using System;
using System.Collections.Generic;

namespace dsr
{
	internal static class Util
	{
		
		public static string Separator {
			get {
				return new String('_', Console.LargestWindowWidth);
			}
		}
		
		public static bool IsUnix {
		    get {
		    	return (new List<int>{4, 6, 128}).Contains((int)Environment.OSVersion.Platform);
		    }
		}
		
		public static bool filter<S, P>(S subject, IEnumerable<P> predicates, Func<P, S, bool> apply)
		{
			return predicates.All(x => apply(x, subject));
		}
	}
}
