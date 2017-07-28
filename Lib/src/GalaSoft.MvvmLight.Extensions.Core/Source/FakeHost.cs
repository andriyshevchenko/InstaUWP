namespace GalaSoft.MvvmLight.Extensions.Test
{
    /// <summary>
    /// Creates a <see cref="HostViewModel"/> which navigates forward N times.
    /// Use for test purposes.
    /// </summary>
    public class FakeHost : HostViewModel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FakeHost"/>.
        /// </summary>
        /// <param name="childName">The name of a child.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="makeSteps">The number of steps to make.</param>
        public FakeHost(string childName, object viewModel, int makeSteps = 2)
        {
            for (int i = 0; i < makeSteps; i++)
            {
                NavigateInternal("main", new object(), Direction.Forward);
            }
        }
    }
}
