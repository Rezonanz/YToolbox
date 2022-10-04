using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace YellowJelloGames.YExtensions
{
    public static class CollectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmpty<T>(this ICollection<T> source)
        {
            return source.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidIndex<T>(this ICollection<T> source, int index)
        {
            return index >= 0 && index < source.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddUnique<T>(this ICollection<T> source, T item)
        {
            if (!source.Contains(item)) source.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ICollection<T> source, IList<T> items)
        {
            for (int i = 0, iMax = items.Count; i < iMax; ++i)
            {
                source.Add(items[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.Add(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRangeInt(this ICollection<int> source, int endExclusive, int startInclusive = 0)
        {
            for (int i = startInclusive; i < endExclusive; ++i)
            {
                source.Add(i);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRangeUnique<T>(this ICollection<T> source, IList<T> items)
        {
            for (int i = 0, iMax = items.Count; i < iMax; ++i)
            {
                source.AddUnique(items[i]);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRangeUnique<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                source.AddUnique(item);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this ICollection<T> source, IEnumerable<T> toRemove)
        {
            foreach (var item in toRemove)
            {
                source.Remove(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Pop<T>(this IList<T> list)
        {
            var last = list[0];
            list.RemoveAt(0);

            return last;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopLast<T>(this IList<T> list)
        {
            var last = list[^1];
            list.RemoveAt(list.Count - 1);

            return last;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains<T>(this IList<T> source, T item)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                if (source[i].Equals(item)) return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this ICollection<T> source)
        {
            return source.Count != 0;
        }
        
        public static bool Any<T>(this IList<T> source, Func<T, bool> predicate)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                if (predicate(source[i])) return true;
            }

            return false;
        }
        
        public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var i in source)
            {
                if (predicate(i)) return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<T>(this IList<T> source, IList<T> toCheck)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                if (toCheck.Contains(source[i])) return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All<T>(this IList<T> source, IList<T> toCheck)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                if (!toCheck.Contains(source[i])) return false;
            }

            return true;
        }


        public static bool All<TSource>(this IList<TSource> source, Func<TSource, bool> predicate)
        {
            for (int i = 0, iMax = source.Count; i < iMax; ++i)
            {
                if (!predicate(source[i])) return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All(this IList<bool> source)
        {
            return source.All(value => value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CopyTo<T>(this ICollection<T> source, ICollection<T> targetOut, bool clearTarget = true)
        {
            if (clearTarget) targetOut.Clear();

            if (source is null) return 0;
            if (targetOut is null) return source.Count;
            
            foreach (var item in source) targetOut.Add(item);

            return source.Count;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<T>(this ICollection<T> source, List<T> targetOut, bool clearTarget = true)
        {
            if (clearTarget) targetOut.Clear();
            
            if (source is null) return;

            targetOut.AddRange(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<T>(this IEnumerable<T> source, ICollection<T> targetOut, bool clearTarget = true)
        {
            if (clearTarget) targetOut.Clear();

            if (source is null || targetOut is null) return;

            foreach (var item in source) targetOut.Add(item);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyTo<T>(this IEnumerator<T> source, ICollection<T> targetOut, bool clearTarget = true)
        {
            if (clearTarget) targetOut?.Clear();

            if (source is null || targetOut is null) return;

            while (source.MoveNext()) targetOut.Add(source.Current);
        }

        public static TValue GetValueOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            if (dict.TryGetValue(key, out var value)) return value;

            value = new TValue();
            dict.Add(key, value);

            return value;
        }

        public static T GetValueOrDefault<T>(this IList<T> source, int index, T defaultValue)
        {
            return source.IsValidIndex(index) ? source[index] : defaultValue;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFirst<T>(this IEnumerable<T> source, out T result)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    result = enumerator.Current;
                    return true;
                }
            }

            result = default;
            return false;
        }
    }
}