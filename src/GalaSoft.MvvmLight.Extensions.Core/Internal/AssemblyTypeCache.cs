using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to cache types of assembly
    /// </summary>
    public class AssemblyTypeCache
    {
        private Lazy<IReadOnlyDictionary<string, Type>> _lazy; 

        public AssemblyTypeCache(Assembly assembly)
        { 
            _lazy
                = new Lazy<IReadOnlyDictionary<string, Type>>(
                    () =>
                        assembly.DefinedTypes
                        .Cast<Type>()
                        .Concat(assembly.ExportedTypes)
                        .Distinct()
                        .ToDictionary(type => type.FullName, type => type)
                  );
        }

        public IReadOnlyDictionary<string, Type> Values()
        {
            return _lazy.Value;
        }
    }
}
