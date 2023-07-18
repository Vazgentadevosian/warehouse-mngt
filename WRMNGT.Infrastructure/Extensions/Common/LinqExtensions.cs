﻿using System.Linq.Expressions;
using Newtonsoft.Json;

namespace WRMNGT.Infrastructure.Extensions.Common
{
    public static class LinqExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return !items.Any();
        }

        public static bool NotEmpty<T>(this IEnumerable<T> items)
        {
            return items != null && !items.IsEmpty();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || items.IsEmpty();
        }

        public static IEnumerable<List<T>> SplitList<T>(this List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element))) yield return element;
            }
        }

        public static Expression ToStringCall(this Expression exp)
        {
            return Expression.Call(exp, nameof(Object.ToString), null);
        }

        public static Expression DeserializeObject(this Expression exp, Type type)
        {
            return Expression.Call(typeof(JsonConvert), 
                                   nameof(JsonConvert.DeserializeObject),
                                   new[] { type },
                                   exp);
        }
    }
}
