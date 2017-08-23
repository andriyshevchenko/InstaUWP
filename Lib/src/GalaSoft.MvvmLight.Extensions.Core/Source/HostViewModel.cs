using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using InputValidation;

using static System.Collections.Generic.Create;

namespace GalaSoft.MvvmLight.Extensions
{
    /// <summary>
    /// View model which is a navigation root and able to provide its children
    /// to use with data binding.
    /// It inherits GalaSoft.MvvmLight.<see cref="ViewModelBase"/>.
    /// </summary>
    public class HostViewModel : ViewModelBase, IParent, INavigationRoot
    {
        private const string MainChild = "main";
        private bool notificationFlag = false;
        private ConcurrentDictionary<string, object> _children = new ConcurrentDictionary<string, object>();
        private Dictionary<string, Entry> _items = new Dictionary<string, Entry>();

        /// <summary>
        /// Navigation diretion.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            /// Forward.
            /// </summary>
            Forward,

            /// <summary>
            /// Backward.
            /// </summary>
            Back
        }

        /// <summary>
        /// To use with unit testing. Defines an navigation entry for a child.
        /// </summary>
        public struct Entry
        {
            private readonly List<object> _vm;
            private int _position;

            /// <summary>
            /// Determines if current view is on top of navigation stack.
            /// </summary>
            public bool OnTop => Position == ViewModel.Count - 1;

            /// <summary>
            /// Navigation stack.
            /// </summary>
            public IReadOnlyList<object> ViewModel => _vm;

            /// <summary>
            /// Current position.
            /// </summary>
            public int Position => _position;

            /// <summary>
            /// Initializes a new instance of <see cref="Entry"/> from other.
            /// </summary>
            /// <param name="other">Reference to other entry.</param>
            public Entry(ref Entry other) : this(other._vm, other.Position)
            {

            }

            /// <summary>
            /// Initializes a new instance of <see cref="Entry"/> from other.
            /// </summary>
            /// <param name="list">Navigation stack.</param>
            public Entry(List<object> list) : this(list, 0)
            {

            }

            /// <summary>
            /// Initializes a new instance of <see cref="Entry"/> from other.
            /// </summary>
            /// <param name="list">Navigation stack.</param>
            /// <param name="position">The position.</param>
            public Entry(List<object> list, int position)
            {
                _vm = list.CheckNotNull(nameof(list));
                _position = position.CheckIfNonNegative(nameof(position));
            }

            /// <summary>
            /// Adds a new view model to stack.
            /// </summary>
            /// <param name="viewModel">The view model.</param>
            public void NewViewModel(object viewModel)
            {
                _vm.Add(viewModel);
                _position = _vm.Count - 1;
            }

            internal void GoBack(int steps = 1) => _position -= steps;

            internal void GoForward(int steps = 1) => _position += steps;

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

        /// <summary>
        /// Navigation stack of every child.
        /// </summary>
        public IReadOnlyDictionary<string, Entry> Items => _items;

        /// <summary>
        /// The children.
        /// </summary>
        public IReadOnlyDictionary<string, object> Children => (IReadOnlyDictionary<string, object>)_children;

        /// <summary>
        /// Navigates a specific child to a specific view, with a specific direction in navigation 
        /// stack. 
        /// </summary>
        /// <param name="childName">The name of a child.</param>
        /// <param name="viewModel">The view model.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="steps">Number of steps to make.</param>
        public void NavigateInternal(string childName, object viewModel, Direction direction, int steps = 1)
        {
            childName.CheckNotNull(nameof(childName));
            steps.CheckIfNatural(nameof(steps));

            object VM = viewModel;
            if (_items.ContainsKey(childName))
            {
                _items.TryGetValue(childName, out Entry item);

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
                _items[childName] = new Entry(ref item);
            }
            else
            {
                _items.Add(childName, new Entry(list(viewModel)));
            }

            //Notify UI listeners.
            _children[childName] = VM;
            Set(nameof(Children), ref notificationFlag, !notificationFlag);
        }

        /// <summary>
        /// Determines if specific child can navigate back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate back.</returns>
        public bool CanGoBack(string childName = MainChild, int steps = 1)
        {
            return _items.ContainsKey(childName) && _items[childName].CanGoBack(steps);
        }

        /// <summary>
        /// Determines if specific child can navigate forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        /// <returns>True if view model can navigate forward.</returns>
        public bool CanGoForward(string childName = MainChild, int steps = 1)
        {
            return _items.ContainsKey(childName) && _items[childName].CanGoForward(steps);
        }

        /// <summary>
        /// Navigates a specific child forward.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        public void GoForward(string childName = MainChild, int steps = 1)
        {
            NavigateInternal(childName, null, Direction.Forward, steps: steps);
        }

        /// <summary>
        /// Navigates a specific child back.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="steps">The number of steps to make.</param>
        public void GoBack(string childName = MainChild, int steps = 1)
        {
            NavigateInternal(childName, null, Direction.Back, steps: steps);
        }

        /// <summary>
        /// Navigates a specific view by passing its mapped view model.
        /// </summary>
        /// <param name="childName">The name of the child.</param>
        /// <param name="viewModel">The view model.</param>
        public void NavigateTo(string childName, object viewModel)
        {
            NavigateInternal(childName, viewModel, Direction.Forward);
        }
    }
}
