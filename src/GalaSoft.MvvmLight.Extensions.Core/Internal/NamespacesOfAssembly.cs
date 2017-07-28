using Cactoos;
using System;
using System.Collections.Generic;
using System.Reflection;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
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
