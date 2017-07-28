namespace GalaSoft.MvvmLight.Extensions
{
    public struct SimpleNamespace : ISimpleNamespace
    {
        public SimpleNamespace(ICSharpName name) :this(name.Namespace)
        {

        }

        public SimpleNamespace(ISimpleNamespace other) : this(other.Name)
        {

        }

        public SimpleNamespace(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
