using Cactoos;
using Cactoos.Scalar;
using System;
using System.Linq;
using System.Reflection;

namespace GalaSoft.MvvmLight.Extensions
{
    public struct AssemblyTypes : IScalar<Type[]>
    {
        private IScalar<Assembly> _assembly;

        public AssemblyTypes(IScalar<Assembly> assembly)
        {
            _assembly = assembly;
        }

        public AssemblyTypes(Assembly assembly) : this(new ValueScalar<Assembly>(assembly))
        {

        }

        public Type[] Value()
        {
            Assembly assembly = _assembly.Value();
            return assembly.DefinedTypes
                    .Cast<Type>()
                    .Concat(assembly.ExportedTypes)
                    .Distinct()
                    .ToArray();
        }
    }
}
