using System.Collections.Generic;

namespace PlusCollections.Extensions
{
    public static class ListExtensions
    {
        public static bool ContainsFirst<T>(this IList<T> list, T target)
        {
            return list.IndexOf(target) == 0;
        }

        public static bool ContainsLast<T>(this IList<T> list, T target)
        {
            var idx = list.IndexOf(target);
            return idx >= 0 && idx == list.Count - 1;
        }
        
        public static bool ContainsNotFirst<T>(this IList<T> list, T target)
        {
            return list.IndexOf(target) > 0;
        }

        public static bool ContainsNotLast<T>(this IList<T> list, T target)
        {
            var idx = list.IndexOf(target);
            return idx >= 0 && idx < list.Count - 1;
        }
    }
}