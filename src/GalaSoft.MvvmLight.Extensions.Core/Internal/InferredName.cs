using Cactoos;
using System;
using System.Collections.Generic;
using System.Reflection;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to infer the namespace of a type from given assemblies.
    /// </summary>
    public struct InferredName : IScalar<string>
    {
        private string _source;
        private ValueTuple<IScalar<string>, IScalar<IReadOnlyDictionary<string, Type>>>[] _items;

        public InferredName(string source, params ValueTuple<IScalar<string>, IScalar<IReadOnlyDictionary<string, Type>>>[] nameAndTypeCachePairs)
        {
            _items = nameAndTypeCachePairs;
            _source = source;
        }

        public InferredName(string source, params ValueTuple<IScalar<string>, IScalar<Assembly>>[] nameAndTypeCachePairs)
            : this(
                  source,
                  Zip(
                      first(nameAndTypeCachePairs),
                      map(
                          nameAndTypeCachePairs,
                          assembly =>
                          (IScalar<IReadOnlyDictionary<string, Type>>)new AssemblyTypeCache(assembly.Item2)
                      )
                  )
              )
        {

        }

        public InferredName(string source, params ValueTuple<IScalar<string>, Assembly>[] nameAndTypeCachePairs)
             : this(
                  source,
                  Zip(
                      first(nameAndTypeCachePairs),
                      map(
                          nameAndTypeCachePairs,
                          assembly =>
                          (IScalar<IReadOnlyDictionary<string, Type>>)new AssemblyTypeCache(assembly.Item2)
                      )
                  )
              )
        {

        }

        public string Value()
        {
            string result = _source;
            if (!new IsNamespaced(result).Value())
            {
                string targetNamespace;
                foreach (var (rootNamespace, assemblyTypeCache) in _items)
                {
                    var namespacedName = new NamespacedName(_source, rootNamespace.Value());
                    if (assemblyTypeCache.Value().ContainsKey(namespacedName.String()))
                    {
                        targetNamespace = rootNamespace.Value();
                        return namespacedName.String();
                    }
                }

                throw new InvalidOperationException($"Unable to infer the namespace for {result}");
            }
            return result;
        }
    }
}
