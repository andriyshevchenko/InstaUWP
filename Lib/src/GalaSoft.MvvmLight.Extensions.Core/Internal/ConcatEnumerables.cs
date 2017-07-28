using Cactoos;
using System.Collections.Generic;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions
{
    public struct ConcatEnumerables<T> : IScalar<IEnumerable<T>>
    {
        private IScalar<IEnumerable<T>>[] _items;

        public ConcatEnumerables(params IScalar<IEnumerable<T>>[] items)
        {
            _items = items;
        }

        public IEnumerable<T> Value()
        {
            var result = _items[0].Value().AsEnumerable();
            for (int i = 1; i < _items.Length; i++)
            {
                result = result.Concat(_items[i].Value());
            }
            return result;
        }
    }
}
