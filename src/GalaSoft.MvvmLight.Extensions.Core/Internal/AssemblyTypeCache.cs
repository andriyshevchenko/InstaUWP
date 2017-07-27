using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Cactoos;
using Cactoos.Scalar;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to cache types of assembly
    /// </summary>
    public class AssemblyTypeCache : IScalar<IReadOnlyDictionary<string, Type>>
    {
        private IScalar<Assembly> _assembly; 

        public AssemblyTypeCache(IScalar<Assembly> assembly)
        {
            _assembly = assembly;
        }

        public AssemblyTypeCache(Assembly assembly) : this(new ValueScalar<Assembly>(assembly))
        {

        }

        public IReadOnlyDictionary<string, Type> Value()
        {
            Assembly assembly = _assembly.Value();
            return assembly.DefinedTypes
                       .Cast<Type>()
                       .Concat(assembly.ExportedTypes)
                       .Distinct()
                       .ToDictionary(type => type.FullName, type => type);
        }
    }
}
