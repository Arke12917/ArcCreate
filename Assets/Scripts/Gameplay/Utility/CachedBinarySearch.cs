using System;
using System.Collections.Generic;
using ArcCreate.Utility.Extension;
using UnityEngine;

namespace ArcCreate.Gameplay
{
    /// <summary>
    /// Class for managing binary search with index caching.
    /// Optimized for accessing a list repeatedly while the returned index rarely changes.
    /// Unlike <see cref="CachedBisect{T, R}"/> which returns the index of the first element larger than the search value,
    /// this returns the larest element smaller than the search value.
    /// </summary>
    /// <typeparam name="T">The type of the list.</typeparam>
    /// <typeparam name="R">The type of the property to search by.</typeparam>
    public class CachedBinarySearch<T, R>
        where R : IComparable<R>
    {
        private R nextRebisect;
        private R nextIncrement;
        private R prevDecrement;
        private R prevRebisect;
        private int cachedIndex;
        private bool nextRebisectAvailable;
        private bool nextIncrementAvailable;
        private bool prevDecrementAvailable;
        private bool prevRebisectAvailable;
        private bool hasResetted = true;
        private readonly List<T> list;
        private int count;
        private readonly Func<T, R> property;
        private readonly IComparer<T> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedBinarySearch{T, R}"/> class.
        /// </summary>
        /// <param name="list">The items to search with. A new sorted copy of the list will be made and stored,
        /// and the original enumerable will remain unchanged.</param>
        /// <param name="property">Function that extracts the property <see cref="{R}"/> from items.</param>
        /// <param name="comparison">Comparison function for sorting items.</param>
        /// <param name="comparer">Optionally, provide a ccomparer for list items.</param>
        public CachedBinarySearch(IEnumerable<T> list, Func<T, R> property, IComparer<T> comparer = null)
        {
            this.list = new List<T>(list);
            count = this.list.Count;
            this.property = property;
            this.comparer = comparer;
            Sort();
        }

        public List<T> List => list;

        public void Sort()
        {
            if (comparer == null)
            {
                list.Sort((a, b) => property(a).CompareTo(property(b)));
            }
            else
            {
                list.Sort(comparer);
            }

            count = list.Count;
            Reset();
        }

        /// <summary>
        /// Binary search on the list. See <see cref="CollectionExtension.BinarySearchNearest{T, R}(IList{T}, R, Func{T, R})"/>.
        /// </summary>
        /// <param name="value">The value to search.</param>
        /// <returns>The index.</returns>
        public int Search(R value)
        {
            int previousCachedIndex = cachedIndex;

            if (hasResetted
             || (prevRebisectAvailable && value.CompareTo(prevRebisect) <= 0)
             || (nextRebisectAvailable && value.CompareTo(nextRebisect) >= 0))
            {
                hasResetted = false;
                cachedIndex = list.BinarySearchNearest(value, property);
                cachedIndex = Mathf.Clamp(cachedIndex, 0, count - 1);
                RecalculateCheckpoints();
                previousCachedIndex = cachedIndex;
            }

            if (nextIncrementAvailable && value.CompareTo(nextIncrement) >= 0)
            {
                cachedIndex++;
                cachedIndex = Mathf.Min(cachedIndex, count - 1);
            }

            if (prevDecrementAvailable && value.CompareTo(prevDecrement) < 0)
            {
                cachedIndex--;
                cachedIndex = Mathf.Max(cachedIndex, 0);
            }

            if (previousCachedIndex != cachedIndex)
            {
                RecalculateCheckpoints();
            }

            return cachedIndex;
        }

        /// <summary>
        /// Reset the internal state.
        /// </summary>
        public void Reset()
        {
            nextRebisectAvailable = false;
            nextIncrementAvailable = false;
            prevDecrementAvailable = false;
            prevRebisectAvailable = false;
            hasResetted = true;
        }

        private void RecalculateCheckpoints()
        {
            if (cachedIndex >= 0)
            {
                prevDecrement = property(list[cachedIndex]);
                prevDecrementAvailable = true;
            }
            else
            {
                prevDecrementAvailable = false;
            }

            if (cachedIndex >= 1)
            {
                prevRebisect = property(list[cachedIndex - 1]);
                prevRebisectAvailable = true;
            }
            else
            {
                prevRebisectAvailable = false;
            }

            if (cachedIndex < count - 1)
            {
                nextIncrement = property(list[cachedIndex + 1]);
                nextIncrementAvailable = true;
            }
            else
            {
                nextIncrementAvailable = false;
            }

            if (cachedIndex < count - 2)
            {
                nextRebisect = property(list[cachedIndex + 2]);
                nextRebisectAvailable = true;
            }
            else
            {
                nextRebisectAvailable = false;
            }
        }
    }
}