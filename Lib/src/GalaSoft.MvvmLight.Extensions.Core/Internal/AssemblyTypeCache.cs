﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Cactoos;
using Cactoos.Scalar;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to cache only type names of assembly.
    /// </summary>
    public struct TypeCacheWithoutNamespace : IScalar<IReadOnlyDictionary<string, Type>>
    {
        private IScalar<IReadOnlyDictionary<string, Type>> _source;

        /// <summary>
        /// Initializes a new instance of <see cref="TypeCacheWithoutNamespace"/>.
        /// </summary>
        /// <param name="source">Type cache with namespaces.</param>
        public TypeCacheWithoutNamespace(IScalar<IReadOnlyDictionary<string, Type>> source)
        {
            _source = source;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>New dictionary instance.</returns>
        public IReadOnlyDictionary<string, Type> Value()
        {
            return dictionary(
                       map(
                           _source.Value(),
                           item => 
                           pair(new SimpleName(item.Key).OwnName, item.Value)
                       )
                   );
        }
    }

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
            return new AssemblyTypes(_assembly)
                .Value()
                .ToDictionary(type => type.FullName, type => type);
        }
    }
}
