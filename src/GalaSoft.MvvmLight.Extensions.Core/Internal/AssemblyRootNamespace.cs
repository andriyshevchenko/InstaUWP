using Cactoos;
using Cactoos.Scalar;
using System.Linq;
using System.Reflection;

namespace GalaSoft.MvvmLight.Extensions
{
    public struct AssemblyRootNamespace : IScalar<string>
    {   
        private IScalar<Assembly> _source;

        public AssemblyRootNamespace(Assembly source): this(new ValueScalar<Assembly>(source))
        {

        }

        public AssemblyRootNamespace(IScalar<Assembly> source)
        {
            _source = source;
        }

        public string Value()
        {
            return new SimpleName(_source.Value().ExportedTypes.First().FullName).Namespace;
        }
    }
}
