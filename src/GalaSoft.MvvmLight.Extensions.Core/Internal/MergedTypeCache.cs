using Cactoos;
using System;
using System.Collections.Generic;
using System.Linq;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    public class MergedTypeCache : IScalar<IReadOnlyDictionary<string, Type>>
    {
        private IScalar<IReadOnlyDictionary<string, Type>>[] _items;

        public MergedTypeCache(params IScalar<IReadOnlyDictionary<string, Type>>[] items)
        {
            _items = items;
        }

        public IReadOnlyDictionary<string, Type> Value()
        {
            var result = _items[0].Value().AsEnumerable();
            for (int i = 1; i < _items.Length; i++)
            {
                result = result.Concat(_items[i].Value());
            }
            return dictionary(result);
        }
    }
}
