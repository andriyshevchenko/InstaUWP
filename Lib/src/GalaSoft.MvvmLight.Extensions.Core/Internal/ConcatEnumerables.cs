using Cactoos;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Concatenated enumerables.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public struct ConcatEnumerables<T> : IEnumerable<T>
    {
        private IScalar<IEnumerable<T>>[] _items;

        /// <summary>
        /// Initializes a new instance of <see cref="ConcatEnumerables{T}"/>.
        /// </summary>
        /// <param name="items">The enumerables.</param>
        public ConcatEnumerables(params IScalar<IEnumerable<T>>[] items)
        {
            _items = items;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator{T}"/> for this collection.
        /// </summary>
        /// <returns>New instance of <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var result = _items[0].Value().AsEnumerable();
            for (int i = 1; i < _items.Length; i++)
            {
                result = result.Concat(_items[i].Value());
            }
            return result.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
