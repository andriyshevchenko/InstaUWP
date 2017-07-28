using Cactoos;

namespace GalaSoft.MvvmLight.Extensions 
{
    public struct IsBlank : IScalar<bool>
    {
        private string _source;

        public IsBlank(string source)
        {
            _source = source;
        }

        public bool Value()
        {
            return _source == string.Empty
                || string.IsNullOrWhiteSpace(_source);
        }
    }
}
