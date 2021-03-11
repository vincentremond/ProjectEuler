using System;
using System.Collections.Generic;

namespace TestCSharp
{
    public abstract class State<TResult, TState>
    {
    }

    public class Terminate<TResult, TState> : State<TResult, TState>
    {
    }

    public class Inconclusive<TResult, TState> : State<TResult, TState>
    {
        public TState State { get; }

        public Inconclusive(TState state)
        {
            State = state;
        }
    }

    public class Valid<TResult, TState> : State<TResult, TState>
    {
        public TResult Value { get; }
        public TState State { get; }

        public Valid(TResult value, TState state)
        {
            Value = value;
            State = state;
        }
    }

    public record ResultItem(long Prime, long Count);

    public static class P003b
    {
        public static IEnumerable<ResultItem> EntryPoint()
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

            return primes.LoremIpsum(
                value,
                (prime, v) =>
                {
                    if (prime * prime > v)
                    {
                        return new Terminate<ResultItem, long>();
                    }

                    var (newValue, count) = PrimeCount(value, prime);
                    return count switch
                    {
                        > 0 => new Valid<ResultItem, long>(new ResultItem(prime, count), newValue),
                        _ => new Inconclusive<ResultItem, long>(newValue),
                    };
                }
            );
        }

        public static IEnumerable<TResult> LoremIpsum<TEnum, TResult, TState>(this IEnumerable<TEnum> enumerable, TState seed, Func<TEnum, TState, State<TResult, TState>> getState)
        {
            var currentValue = seed;
            foreach (var item in enumerable)
            {
                var state = getState(item, currentValue);

                switch (state)
                {
                    case Inconclusive<TResult, TState> inconclusive:
                        currentValue = inconclusive.State;
                        break;
                    case Valid<TResult, TState> valid:
                        currentValue = valid.State;
                        yield return valid.Value;
                        break;
                    case Terminate<TResult, TState>:
                        yield break;
                }
            }
        }

        private static (long newValue, long count ) PrimeCount(long value, long prime, long currentCount = 0)
        {
            if (value % prime == 0)
            {
                return PrimeCount(value / prime, prime, currentCount + 1);
            }

            return (value, currentCount);
        }
    }
}
