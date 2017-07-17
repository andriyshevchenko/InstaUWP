namespace GalaSoft.MvvmLight.Extensions.Test
{
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
