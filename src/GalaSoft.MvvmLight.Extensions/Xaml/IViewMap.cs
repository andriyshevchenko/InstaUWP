namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public interface IViewMap
    {
        bool HasView(object viewModel);
        object GetViewFor(object viewModel);
    }
}
