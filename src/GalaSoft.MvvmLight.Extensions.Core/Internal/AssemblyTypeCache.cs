﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Cactoos;
using Cactoos.Scalar;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to cache types of assembly.
    /// </summary>
    public class AssemblyTypeCache : IScalar<IReadOnlyDictionary<string, Type>>
    {
        private IScalar<Assembly> _assembly;

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyTypeCache"/>.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public AssemblyTypeCache(IScalar<Assembly> assembly)
        {
            _assembly = assembly;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AssemblyTypeCache"/>.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public AssemblyTypeCache(Assembly assembly) : this(new ValueScalar<Assembly>(assembly))
        {

        }

        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        /// <returns>New dictionary instance.</returns>
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
