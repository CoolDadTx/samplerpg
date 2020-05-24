using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleRpg.Engine
{
    public static class EnumerableExtensions
    {
        public static int SumOrZero<T> ( this IEnumerable<T> source, Func<T, int> accumulator ) => source.Any() ? source.Sum(accumulator) : 0;

        public static long SumOrZero<T> ( this IEnumerable<T> source, Func<T, long> accumulator ) => source.Any() ? source.Sum(accumulator) : 0;

        public static decimal SumOrZero<T> ( this IEnumerable<T> source, Func<T, decimal> accumulator ) => source.Any() ? source.Sum(accumulator) : 0;

        public static double SumOrZero<T> ( this IEnumerable<T> source, Func<T, double> accumulator ) => source.Any() ? source.Sum(accumulator) : 0;
    }
}
