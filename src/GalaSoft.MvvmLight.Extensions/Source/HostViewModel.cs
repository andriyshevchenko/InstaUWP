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

    public class HostViewModel : ViewModelBase
    {
        public HostViewModel()
        {

        }

        public enum Direction
        {
            Forward,
            Back
        }

        public struct Item
        {
            private readonly List<object> _vm;
            public IReadOnlyList<object> ViewModel => _vm;
            private int _position;
            public int Position => _position;

            public Item(ref Item other) : this(other._vm, other.Position)
            {

            }

            public Item(List<object> list) : this(list, 0)
            {

            }

            public Item(List<object> list, int position)
            {
                _vm = list.CheckNotNull(nameof(list));
                _position = position.CheckIfNonNegative(nameof(position));
            }

            public void NewViewModel(object viewModel)
            {
                _vm.Add(viewModel);
                _position = _vm.Count - 1;
            }

            public bool OnTop => Position == ViewModel.Count - 1;

            public bool CanGoBack(int steps)
            {
                return Position - steps > 0;
            }

            public bool CanGoForward(int steps)
            {
                return Position + steps < ViewModel.Count;
            }
        }

        bool _flag = false;

        ObservableConcurrentDictionary<string, object> Children = new ObservableConcurrentDictionary<string, object>();
        Dictionary<string, Item> _items = new Dictionary<string, Item>();
        public IReadOnlyDictionary<string, Item> Items => _items;

        void NotifyUiListeners(string childName, object viewModel)
        {
            Children[childName] = viewModel;
            Set(nameof(Children), ref _flag, !_flag);
        }

        public void NavigateInternal(string childName, object viewModel, Direction direction, int steps = 1)
        {
            viewModel.CheckNotNull(nameof(viewModel));
            childName.CheckNotNull(nameof(childName));
            steps.CheckIfNatural(nameof(steps));

            object VM = viewModel;
            if (_items.ContainsKey(childName))
            {
                _items.TryGetValue(childName, out Item item);
                
                void error() => throw new ArgumentOutOfRangeException(nameof(steps));
                if (direction == Direction.Forward)
                {
                    if (steps == 1 && item.OnTop)
                    {
                        item.NewViewModel(viewModel);
                        _items[childName] = new Item(ref item);
                    }
                    else
                    {
                        item.CanGoForward(steps).CheckIfFalse(error);
                        VM = item.ViewModel[item.Position + steps];
                    }
                }
                else
                {
                    item.CanGoBack(steps).CheckIfFalse(error);
                    VM = item.ViewModel[item.Position - steps];
                }
            }
            else
            {
                _items.Add(childName, new Item(list(viewModel)));
            }

            NotifyUiListeners(childName, VM);
        }
    }
}
