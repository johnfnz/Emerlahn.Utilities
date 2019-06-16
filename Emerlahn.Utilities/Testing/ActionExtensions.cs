using System;
using System.Collections.Generic;
using System.Linq;

namespace Emerlahn.Utilities.Testing
{
    public static class AssertionExtensions
    {
        public static void AssertAll(this IEnumerable<Action> asserts)
        {
            var exceptions = new List<Exception>();
            foreach (var assert in asserts)
            {
                try
                {
                    assert();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                if (exceptions.Count == 1) throw exceptions.First();
                string message = $"Multiple assertions failed:\r\n\r\n{string.Join("\r\n\r\n", exceptions.Select(e => e.Message))}\r\n";
                throw new AssertionException(message);
            }
        }

        public static void AssertAll(this IEnumerable<NamedAction> namedAssertions)
        {
            var exceptions = new Dictionary<string, Exception>();
            foreach (var named in namedAssertions)
            {
                try
                {
                    named.Action();
                    Console.WriteLine($"Passed {named.Name}");
                }
                catch (Exception ex)
                {
                    exceptions.Add(named.Name, ex);
                }
            }

            if (exceptions.Any())
            {
                if (exceptions.Count == 1) throw exceptions.First().Value;
                string message = $"Multiple assertions failed:\r\n\r\n{string.Join("\r\n\r\n", exceptions.Select(kv => $"{kv.Key}: {kv.Value.Message}"))}\r\n";
                throw new AssertionException(message);
            }
        }
    }
}