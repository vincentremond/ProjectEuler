#nullable enable
using System;
using System.Collections.Generic;

namespace TestCSharp
{
    public class P003
    {
        public static void EntryPoint()
        {
            var primes = new long[]
            {
                2,
                3,
                5,
                7,
                11,
                13
            };

            var value = 132L;
            foreach (var l in XXX(primes, value))
            {
                Console.WriteLine(l);
            }
        }

        private static IEnumerable<(long prime, long count)> XXX(long[] primes, long value)
        {
            var vv = value;
            foreach (var prime in primes)
            {
                var (newValue, primeCount) = PrimeCount(vv, prime);
                vv = newValue;
                if (primeCount > 0)
                {
                    yield return (prime, primeCount);
                }

                if (vv == 1)
                {
                    yield break;
                }
            }
        }

        public static (long count, long newValue) PrimeCount(long value, long prime, long currentCount = 0)
        {
            if (value % prime == 0)
            {
                return PrimeCount(value / prime, prime, currentCount + 1);
            }

            return (value, currentCount);
        }
    }
}
