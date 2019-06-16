using System;
using System.Collections.Generic;
using System.Linq;

namespace Emerlahn.Utilities.Testing
{
    public static class Assertions
    {
        public static void ShouldBeEmpty(this IEnumerable<string> values, string because = null)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));

            if (values.Any())
            {
                string itemsText = string.Join("\", \"", values);
                var reason = because == null ? string.Empty : $" because {because}";
                throw new Exception($"Expected array to be empty{reason}, but found \"{itemsText}\".");
            }
        }

        public static void ShouldContainSameValuesAs(this ICollection<string> left, ICollection<string> right, string becauseOnlyOnLeft = null, string becauseOnlyOnRight = null)
        {
            var comparer = new SetComparer<string>();
            var comparison = comparer.Compare(left, right, StringComparer.Ordinal);

            var asserts = new List<Action> {
                () => comparison.OnlyOnLeft.ShouldBeEmpty(becauseOnlyOnLeft),
                () => comparison.OnlyOnRight.ShouldBeEmpty(becauseOnlyOnRight)
            };

            asserts.AssertAll();
        }

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
