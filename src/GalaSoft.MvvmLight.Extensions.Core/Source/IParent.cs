namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// Defines an interface of a root view model which owns
    /// other view models.
    /// </summary>
    public interface IParent
    {
        /// <summary>
        /// The children. To use with data binding.
        /// </summary>
        object Children { get; }

        /// <summary>
        /// Accesses a specific view model for child by its name.
        /// </summary>
        /// <param name="key">The child name.</param>
        /// <returns>View model for specific child.</returns>
        object this[string key] { get; }
    }
}
