namespace GalaSoft.MvvmLight.Extensions.Test
{
    /// <summary>
    /// Creates a <see cref="HostViewModel"/> which navigates forward N times.
    /// Use for test purposes.
    /// </summary>
    public class FakeHost : HostViewModel
    {
        public FakeHost(string childName, object viewModel, int makeSteps = 2)
        {
            for (int i = 0; i < makeSteps; i++)
            {
                NavigateInternal("main", new object(), Direction.Forward);
            }
        }
    }
}
