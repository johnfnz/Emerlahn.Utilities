using System;
using System.Collections.Generic;
using System.Linq;

namespace Emerlahn.Utilities
{
    public static class BooleanCombinationsGenerator
    {
        public static List<bool[]> Generate(int count)
        {
            int combinations = (int)Math.Pow(2, count);
            return Enumerable.Range(0, combinations)
                .Select(flags => IntToBools(count, flags).ToArray())
                .ToList();
        }

        private static bool[] IntToBools(int count, int flags)
        {
            return Enumerable.Range(0, count).Reverse()
                .Select(i => (int)Math.Pow(2, i))
                .Select(pow => (pow & flags) != pow)
                .ToArray();
        }
    }
}
