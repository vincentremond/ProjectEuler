using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeLib
{
    public class PrimeList2 : IPrimeList
    {
        private class PrimeItem
        {
            internal readonly long Value;
            internal readonly long MaxValue;
            private long _invalidator;

            public PrimeItem(long value)
            {
                _invalidator = value;
                MaxValue = value * value;
                Value = value;
            }

            public bool CanDivide(long candidate)
            {
                while (_invalidator < candidate)
                {
                    _invalidator += Value;
                }

                return _invalidator == candidate;
            }
        }

        private long nextCandidate = 2;
        private readonly List<PrimeItem> _primes = new() { new PrimeItem(2) };

        public IEnumerable<long> All()
        {
            var index = 0;
            while (true)
            {
                if (index >= _primes.Count)
                {
                    EnlargeList();
                }

                yield return _primes[index++].Value;
            }
        }

        private void EnlargeList()
        {
            bool IsPrimeLocal(long c)
            {
                for (var index = 0; index < _primes.Count; index++)
                {
                    if (_primes[index].MaxValue > c)
                    {
                        return true;
                    }

                    if (_primes[index].CanDivide(c))
                    {
                        return false;
                    }
                }

                throw new InvalidOperationException("where am i ?");
            }

            lock (_primes)
            {
                while (true)
                {
                    nextCandidate++;
                    if (IsPrimeLocal(nextCandidate))
                    {
                        _primes.Add(new PrimeItem(nextCandidate));
                        return;
                    }
                }
            }
        }

        public bool IsPrime(long value) =>
            All()
                .TakeWhile(prime => prime * prime <= value)
                .Any(x => value % x == 0);
    }
}
