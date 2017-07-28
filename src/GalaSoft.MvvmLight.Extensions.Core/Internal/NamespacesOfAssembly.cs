using Cactoos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Collections.Generic.Create;

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

    public struct NamespacesOfAssembly : IScalar<IEnumerable<SimpleNamespace>>
    {
        private IScalar<Type[]> _types;

        public NamespacesOfAssembly(IScalar<Type[]> types)
        {
            _types = types;
        }

        public NamespacesOfAssembly(IScalar<Assembly> assembly)
            : this(new AssemblyTypes(assembly))
        {

        }

        public NamespacesOfAssembly(Assembly assembly)
          : this(new AssemblyTypes(assembly))
        {

        }

        public IEnumerable  <SimpleNamespace> Value()
        {
            return
                set(
                    map(
                        _types.Value(),
                        type =>
                            new SimpleNamespace(
                                new SimpleName(type.FullName)
                            )
                    )
                );
        }
    }
}
