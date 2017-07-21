using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Represents a collection of pairs
    /// </summary>
    public class PairCollection : List<Pair>
    {
        public Type[] Views => System.Linq.Enumerable.Select(this, collection => collection.View).ToArray();
        public Type[] ViewModels => System.Linq.Enumerable.Select(this, collection => collection.ViewModel).ToArray();

        public PairCollection(IEnumerable<Pair> item):base(item)
        {

        }

        public PairCollection()
        {

        }
    }
}
