using System.Collections.Generic;

namespace GalaSoft.MvvmLight.Extensions
{
    public interface IParent
    {
        IReadOnlyDictionary<string, object> Children { get; }
    }
}
