using GalaSoft.MvvmLight.Extensions.Xaml;
using Instagram.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using static System.Collections.Generic.Create;
using static System.Functional.Func;

namespace Instagram
{
    public class ViewMap : IViewMap
    {
        static Dictionary<Type, Func<Page>> _views
            = dictionary((typeof(BlankViewModel), fun(() => (Page)new BlankPage())));

        public object GetViewFor(object viewModel)
        {
            Type type = viewModel.GetType();
            if (_views.ContainsKey(type))
            {
                return _views[type];
            }
            throw new InvalidOperationException($"View for view model {type} not added yet");
        }

        public bool HasView(object viewModel)
        {
            return _views.ContainsKey(viewModel.GetType());
        }
    }
}
