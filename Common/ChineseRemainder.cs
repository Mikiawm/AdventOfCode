using System.Linq;

namespace Common
{
    public static class ChineseRemainder
    {
        public static long Solve(long[] n, long[] a)
        {
            long prod = n.Aggregate(1l, (i, j) => i * j);
            long sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                var p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}