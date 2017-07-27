using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using InputValidation;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    public class HostViewModel : ViewModelBase, IParent, ICanNavigate
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
            private int _position;

            public IReadOnlyList<object> ViewModel => _vm;
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

            internal void GoBack(int steps = 1) => _position-=steps;

            internal void GoForward(int steps = 1) => _position+=steps;

            public bool OnTop => Position == ViewModel.Count - 1;

            internal object Current => ViewModel[Position];

            internal bool CanGoBack(int steps)
            {
                return Position - steps >= 0;
            }

            internal bool CanGoForward(int steps)
            {
                return Position + steps < ViewModel.Count;
            }
        }

        ConcurrentDictionary<string, object> _children = new ConcurrentDictionary<string, object>();
        Dictionary<string, Item> _items = new Dictionary<string, Item>();

        public IReadOnlyDictionary<string, Item> Items => _items;
        public IReadOnlyDictionary<string, object> Children => (IReadOnlyDictionary<string, object>)_children;

        bool _flag = false;
        void NotifyUiListeners(string childName, object viewModel)
        {
            _children[childName] = viewModel;
            Set(nameof(Children), ref _flag, !_flag);
        }

        public void NavigateInternal(string childName, object viewModel, Direction direction, int steps = 1)
        {
            childName.CheckNotNull(nameof(childName));
            steps.CheckIfNatural(nameof(steps));

            object VM = viewModel;
            if (_items.ContainsKey(childName))
            {
                _items.TryGetValue(childName, out Item item);

                void error() => throw new ArgumentOutOfRangeException(nameof(steps));
                if (direction == Direction.Forward)
                {
                    if (steps == 1)
                    {
                        if (item.OnTop)
                        {
                            item.NewViewModel(viewModel);
                        }
                        else
                        {
                            item.GoForward();
                        }
                    }
                    else
                    {
                        item.CanGoForward(steps)
                            .CheckIfFalse(error);
                        item.GoForward(steps);
                        VM = item.Current;
                    }
                }
                else
                {
                    item.CanGoBack(steps)
                        .CheckIfFalse(error);
                    item.GoBack(steps);
                    VM = item.Current;
                }
                _items[childName] = new Item(ref item);
            }
            else
            {
                _items.Add(childName, new Item(list(viewModel)));
            }

            NotifyUiListeners(childName, VM);
        }

        public bool CanGoBack(string childName, int steps = 1)
        {
            return _items.ContainsKey(childName) && _items[childName].CanGoBack(steps);
        }

        public bool CanGoForward(string childName, int steps = 1)
        {
            return _items.ContainsKey(childName) && _items[childName].CanGoForward(steps);
        }

        public void GoForward(string childName, int steps = 1)
        {
            NavigateInternal(childName, null, Direction.Forward, steps: steps);
        }

        public void GoBack(string childName, int steps = 1)
        {
            NavigateInternal(childName, null, Direction.Back, steps: steps);
        }

        public void NavigateTo(string childName, object viewModel)
        {
            NavigateInternal(childName, viewModel, Direction.Forward);
        }
    }
}
