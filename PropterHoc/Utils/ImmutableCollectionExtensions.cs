using System;

namespace PropterHoc
{
    internal static class ImmutableCollectionExtensions
    {
        internal static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable is null
                ? new List<T>().AsReadOnly()
                : new List<T>(enumerable).AsReadOnly();
        }

        internal static IReadOnlyDictionary<TKey, TValue> ToReadOnlyDictionary<TKey, TValue>(
            this IDictionary<TKey, TValue>? dict)
            where TKey : notnull
        {
            return dict is null
                ? new Dictionary<TKey, TValue>().AsReadOnly()
                : dict.AsReadOnly();
        }
    }
}