using System;
using System.Linq;
using System.Collections.Generic;

namespace evo.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static T ObtainAndRemove<T>(this ICollection<T> collection, int index)
        {
            T item = collection.ElementAt(index);
            collection.Remove(item);

            return item;
        }
    }
}