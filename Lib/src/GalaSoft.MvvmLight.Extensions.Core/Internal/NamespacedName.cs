using Cactoos;
using System.Text;

namespace GalaSoft.MvvmLight.Extensions
{
    public struct NamespacedName : IText
    {
        private string _source;
        private string _namespace;

        public NamespacedName(string source, ISimpleNamespace @namespace) 
            : this(source, @namespace.Name)
        {

        }

        public NamespacedName(string source, string @namespace)
        {
            _source = source;
            _namespace = @namespace;
        }

        public string String()
        {
            string @namespace = new SimpleName(_source).Namespace;
            string result = _source;
            if (new IsBlank(@namespace).Value())
            {
                result = new StringBuilder()
                       .Append(_namespace)
                       .Append('.')
                       .Append(_source)
                       .ToString();
            }
            return result;
        }
    }
}
