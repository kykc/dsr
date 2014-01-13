using System;
using System.Collections.Generic;
using System.Linq;

namespace dsr
{
	static class EnumerableExtentions
	{		
		public static Dictionary<TKey, TValue> ToDict<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
		{
			return list.ToDictionary(x => x.Key, x => x.Value);
		}
		
		public static bool All<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		{
			return enumerable.Select(predicate).Aggregate((x, y) => x && y);
		}
		
		public static bool Any<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		{
			return enumerable.Select(predicate).Aggregate((x, y) => x || y);
		}
		
		public static void ForEach<T>(this IEnumerable<T> iterator, Action<T> action)
		{
			foreach (T member in iterator)
			{
				action(member);
			}
		}
		
		public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> ii)
		{
			return ii.SelectMany(x => x);
		}
		
		public static void ForEach<T1, T2>(this Dictionary<T1, T2> dict, Action<KeyValuePair<T1, T2>> action)
		{
			foreach (var pair in dict)
			{
				action(pair);
			}
		}
    }
}
