using Cactoos;
using Cactoos.Scalar;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Allows to extract namespace and own name from a full name.
    /// </summary>
    public struct SimpleName : ICSharpName
    {
        private string source;
        private IScalar<int> _lastOccurence;

        public SimpleName(string nameWithNamespace)
        {
            source = nameWithNamespace;
            _lastOccurence = new LazyScalar<int>(() =>
            {
                int lastOccurence = 0;
                for (int i = 0; i < nameWithNamespace.Length; i++)
                {
                    if (nameWithNamespace[i] == '.')
                    {
                        lastOccurence = i;
                    }
                }
                return lastOccurence;
            });
        }

        public string OwnName
        {
            get
            {
                int value = _lastOccurence.Value();
                string result = source;
                if (value != 0)
                {
                    result = source.Substring(value + 1, source.Length - value - 1);
                }
                return result;
            }
        }

        public string Namespace
        {
            get
            {
                int value = _lastOccurence.Value();
                string result = string.Empty;
                if (value != 0)
                {
                    result = source.Substring(0, value);
                }
                return result;
            }
        }
    }
}
