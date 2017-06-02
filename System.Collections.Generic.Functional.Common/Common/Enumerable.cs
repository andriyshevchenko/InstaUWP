using System.Collections.Generic;


namespace System.Linq.Functional
{
    public static class Enumerable
    {
        public static IEnumerable<T> ZipIterator<T>(Func<IEnumerable<T>, T> selector, params IEnumerable<T>[] items)
        {
            var e = items.Select(i => i.GetEnumerator()).ToArray();
            while (e.Select(enumerator => enumerator.MoveNext())
                    .Aggregate((b1, b2) => b1 && b2))
            {
                yield return selector(e.Select(enumerator => enumerator.Current));
            }
            foreach (var item in e)
            {
                item.Dispose();
            }
        }

        public static IEnumerable<TResult> ZipIterator<K, T, S, TResult>(IEnumerable<K> first, IEnumerable<T> second, IEnumerable<S> third, Func<K, T, S, TResult> selector)
        {
            var e1 = first.GetEnumerator();
            var e2 = second.GetEnumerator();
            var e3 = third.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
            {
                yield return selector(e1.Current, e2.Current, e3.Current);
            }
            e1.Dispose();
            e2.Dispose();
            e3.Dispose();
        }

        public static IEnumerable<T> ZipIterator<T>(IEnumerable<IEnumerable<T>> items)
        {
            foreach (var item in items)
            {
                foreach (var subItem in item)
                {
                    yield return subItem;
                }
            }
        }

        public static IEnumerable<T> PartIterator<T>(T[] items, int start, int end)
        {
            if (start < 0 || end > items.Length)
            {
                yield break;
            }
            for (int i = start; i < end; i++)
            {
                yield return items[i];
            }
        }

        public static IEnumerable<T> PartIterator<T>(IReadOnlyList<T> items, int start, int end)
        {
            if (start < 0 || end > items.Count)
            {
                yield break;
            }
            for (int i = start; i < end; i++)
            {
                yield return items[i];
            }
        }
    }
}
