using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Represents a list of pairs
    /// </summary>
    public class PairList : List<Pair>
    {
        public Type[] Views => System.Linq.Enumerable.Select(this, collection => collection.View).ToArray();
        public Type[] ViewModels => System.Linq.Enumerable.Select(this, collection => collection.ViewModel).ToArray();

        public PairList(IEnumerable<Pair> item):base(item)
        {

        }

        public PairList(params Pair[] pairs):base(pairs)
        {

        }
        
        public PairList()
        {

        }
    }
}
