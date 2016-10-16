using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Emerlahn.Utilities
{
    public class SetComparer<TValue>
    {
        public class Comparison
        {
            public IReadOnlyCollection<TValue> Matches { get; }
            public IReadOnlyCollection<TValue> OnlyOnLeft { get; }
            public IReadOnlyCollection<TValue> OnlyOnRight { get; }

            public bool AreEquivalent { get; }

            internal Comparison(IList<TValue> matches, IList<TValue> onlyOnLeft, IList<TValue> onlyOnRight)
            {
                Matches = new ReadOnlyCollection<TValue>(matches);
                OnlyOnLeft = new ReadOnlyCollection<TValue>(onlyOnLeft);
                OnlyOnRight = new ReadOnlyCollection<TValue>(onlyOnRight);
                AreEquivalent = !OnlyOnLeft.Any() && !OnlyOnRight.Any();
            }
        }

        public Comparison Compare(IEnumerable<TValue> left, IEnumerable<TValue> right, IEqualityComparer<TValue> comparer = null)
        {
            if (comparer == null) comparer = EqualityComparer<TValue>.Default;
            var matches = left.Intersect(right, comparer).ToList();
            var onlyInLeft = left.Except(right, comparer).ToList();
            var onlyInRight = right.Except(left, comparer).ToList();
            return new Comparison(matches, onlyInLeft, onlyInRight);
        }

        public Comparison Compare<TKey>(IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right, IEqualityComparer<TKey> comparer = null)
        {
            var keysComparison = new SetComparer<TKey>().Compare(left.Keys, right.Keys);
            return GetDictionaryValues(keysComparison, left, right);
        }

        private Comparison GetDictionaryValues<TKey>(SetComparer<TKey>.Comparison keysComparison, IDictionary<TKey, TValue> left, IDictionary<TKey, TValue> right)
        {
            return new Comparison(keysComparison.Matches.Select(k => left[k]).ToList(),
                keysComparison.OnlyOnLeft.Select(k => left[k]).ToList(),
                keysComparison.OnlyOnRight.Select(k => right[k]).ToList());
        }
    }
}
