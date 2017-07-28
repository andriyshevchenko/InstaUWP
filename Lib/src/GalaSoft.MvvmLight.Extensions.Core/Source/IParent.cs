using System.Collections.Generic;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Defines an interface of a root view model which owns
    /// other view models.
    /// </summary>
    public interface IParent
    {
        /// <summary>
        /// The children.
        /// </summary>
        IReadOnlyDictionary<string, object> Children { get; }
    }
}
