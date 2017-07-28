using Cactoos;
using Cactoos.Scalar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to infer the namespace of a type from given assemblies.
    /// </summary>
    public struct InferredName : IScalar<string>
    {
        private string _source;
        private IScalar<IEnumerable<SimpleNamespace>> _namespaces;
        private IScalar<IReadOnlyDictionary<string, Type>> _typeCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespaces"></param>
        /// <param name="typeCache"></param>
        /// <param name="source"></param>
        public InferredName(IScalar<IEnumerable<SimpleNamespace>> namespaces, IScalar<IReadOnlyDictionary<string, Type>> typeCache, string source)
        {
            _namespaces = namespaces;
            _typeCache = new CachedScalar<IReadOnlyDictionary<string, Type>>(typeCache);
            _source = source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <param name="typeCache"></param>
        /// <param name="source"></param>
        public InferredName(IScalar<Type[]> types, IScalar<IReadOnlyDictionary<string, Type>> typeCache, string source)
            : this(new NamespacesOfAssembly(types), typeCache, source)
        {

        }

        public InferredName(IScalar<Assembly> assembly, IScalar<IReadOnlyDictionary<string, Type>> typeCache, string source)
            : this(new AssemblyTypes(assembly), typeCache, source)
        {

        }

        public InferredName(Assembly assembly, IScalar<IReadOnlyDictionary<string, Type>> typeCache, string source)
          : this(new AssemblyTypes(assembly), typeCache, source)
        {

        }


        public string Value()
        {
            string result = _source;
            string targetNamespace = null;
            if (!new IsNamespaced(result).Value())
            {
                var namespaces = _namespaces.Value().ToArray();

                foreach (var item in namespaces)
                {
                    var namespacedName = new NamespacedName(result, item).String();
                    IReadOnlyDictionary<string, Type> readOnlyDictionary = _typeCache.Value();
                    if (readOnlyDictionary.ContainsKey(namespacedName))
                    {
                        return namespacedName;
                    }
                }

                if (new IsBlank(targetNamespace).Value())
                {
                    throw new InvalidOperationException($"Unable to infer the namespace for {result}");
                }
            }
            return result;
        }
    }
}
