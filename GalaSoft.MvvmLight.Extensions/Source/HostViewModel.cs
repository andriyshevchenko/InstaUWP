using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using InputValidation;
using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    public interface IParent
    {
        IDictionary<string, object> Children { get; }
    }

    public class HostViewModel : IParent, ICanNavigate
    {
        public HostViewModel()
        {

        }

        struct Item
        {
            public readonly List<object> ViewModel; 
            public readonly int Position;

            public Item(List<object> list, int position)
            {
                ViewModel = list.CheckNotNull(nameof(list));
                Position = position.CheckIfNonNegative(nameof(position));
            }
             
            bool CanGoBack => Position > 0;
            bool CanGoForward => Position < ViewModel.Count - 1;
        }

        public IDictionary<string, object> Children => throw new NotImplementedException();

        ObservableConcurrentDictionary<string, Item> _children
            = new ObservableConcurrentDictionary<string, Item>();

        public bool CanGoBack(string childName, int steps = 1)
        {
            throw new NotImplementedException();
        }

        public bool CanGoForward(string childName, int steps = 1)
        {
            throw new NotImplementedException();
        }

        public void GoForward(string childName, int steps = 1)
        {
            throw new NotImplementedException();
        }

        public void GoBack(string childName, int steps = 1)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string childName, object viewModel, int steps = 1)
        {
            throw new NotImplementedException();
        }
    }
}
