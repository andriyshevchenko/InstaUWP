using Cactoos;

namespace GalaSoft.MvvmLight.Extensions
{
    public struct IsNamespaced : IScalar<bool>
    {
        private string _source;

        public IsNamespaced(string source)
        {
            _source = source;
        }

        public bool Value()
        {
            return _source.Contains(".");
        }
    }
}
