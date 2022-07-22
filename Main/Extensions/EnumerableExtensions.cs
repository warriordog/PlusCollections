using System;
using System.Collections.Generic;
using System.Linq;

namespace PlusCollections.Extensions
{
    public static class EnumerableExtensions
    {
        public static TOut MaxOrDefault<TIn, TOut>(this IEnumerable<TIn> enumerable, TOut def, Func<TIn, TOut> callback)
        {
            var max = def;
            
            var list = enumerable.ToList();
            if (list.Any())
            {
                max = list.Max(callback);
            }

            return max;
        }

        public static TOut? MaxOrDefault<TIn, TOut>(this IEnumerable<TIn> enumerable, Func<TIn, TOut> callback) => MaxOrDefault(enumerable, default, callback);

        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable, T def)
        {
            var max = def;
            
            var list = enumerable.ToList();
            if (list.Any())
            {
                max = list.Max();
            }

            return max;
        }

        public static T? MaxOrDefault<T>(this IEnumerable<T> enumerable) => MaxOrDefault(enumerable, default);
    }
}