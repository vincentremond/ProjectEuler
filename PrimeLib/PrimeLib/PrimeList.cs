using System.Collections.Generic;
using System.Linq;

namespace PrimeLib
{
    public interface IPrimeList
    {
        IEnumerable<long> All();
        bool IsPrime(long value);
    }

    public class PrimeList : IPrimeList
    {
        private readonly object _enlargeInProgressLocker = new();
        private long _next;

        private readonly long[] _enlargers;
        private readonly long _enlargeSize;
        private readonly List<long> _primes;

        public PrimeList(long size)
        {
            IEnumerable<long> range(long count)
            {
                for (var i = 0; i < count; i++)
                {
                    yield return i;
                }
            }

            _next = size;
            _enlargeSize = size;
            _enlargers = (
                from a in range(size / 10L)
                from b in new[]
                {
                    1,
                    3,
                    7,
                    9,
                }
                select (a * 10) + b
            ).ToArray();
            _primes = InitPrimes(size);
        }

        private List<long> InitPrimes(long size)
        {
            var result = new List<long>();
            for (var toTest = 2; toTest <= size; toTest++)
            {
                if (!result.Any(prime => toTest % prime == 0))
                {
                    result.Add(toTest);
                }
            }

            return result;
        }

        public IEnumerable<long> All()
        {
            var index = 0;
            while (true)
            {
                while (index >= _primes.Count)
                {
                    EnlargeList();
                }

                yield return _primes[index++];
            }
        }

        private void EnlargeList()
        {
            lock (_enlargeInProgressLocker)
            {
                var toAdd = _enlargers
                    .Select(e => _next + e)
                    .Where(IsPrimeFast)
                    .AsParallel()
                    .AsOrdered();
                _primes.AddRange(toAdd);
                _next += _enlargeSize;
            }
        }

        private bool IsPrimeFast(long value)
        {
            for (var index = 1; index < _primes.Count; index++)
            {
                var prime = _primes[index];
                if (value % prime == 0)
                {
                    return false;
                }

                if (prime * prime > value)
                {
                    return true;
                }
            }

            return true;
        }

        public bool IsPrime(long value)
        {
            foreach (var prime in All())
            {
                if (value % prime == 0)
                {
                    return false;
                }

                if (prime * prime > value)
                {
                    return true;
                }
            }

            return true;
        }
    }
}
