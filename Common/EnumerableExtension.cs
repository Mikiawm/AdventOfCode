using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class IEnumerable
    {
        public static IEnumerable<int> XRange(int from, int to)
        {
            var len = Math.Abs(from - to) + 1;
            return from < to ? Enumerable.Range(from, len) : Enumerable.Range(to, len).Reverse();
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self.Select((item, index) => (item, index));
    }
}