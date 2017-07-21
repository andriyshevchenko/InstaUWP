using System;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace GalaSoft.MvvmLight.Extensions.Test.Xaml
{
    public sealed partial class FakeView : UserControl
    {
        public static readonly Type Type = typeof(FakeView);
        public FakeView()
        {
            this.InitializeComponent();
        }
    }
}
