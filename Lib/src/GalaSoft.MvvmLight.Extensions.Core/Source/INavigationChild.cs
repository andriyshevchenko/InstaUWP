namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// The child of a <see cref="INavigationRoot"/>.
    /// </summary>
    public interface INavigationChild
    {
        /// <summary>
        /// The navigation root.
        /// </summary>
        INavigationRoot Root { get; }

        /// <summary>
        /// The name of the child.
        /// </summary>
        string ChildName { get; }
    }
}
