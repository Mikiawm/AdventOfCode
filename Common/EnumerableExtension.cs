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
    }
}