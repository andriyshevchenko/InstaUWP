using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    /// <summary>
    /// Represents a list of pairs
    /// </summary>
    public class PairList : List<IPair>, ICollection<IPair>, IEnumerable<IPair>
    {
        public Type[] Views => Enumerable.Select(this, collection => collection.View).ToArray();
        public Type[] ViewModels => Enumerable.Select(this, collection => collection.ViewModel).ToArray();

        public PairList(IEnumerable<IPair> item):base(item)
        {

        }

        public PairList(params IPair[] pairs):base(pairs)
        {

        }
        
        public PairList()
        {

        }
    }
}
