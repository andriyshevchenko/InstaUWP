using System;
using System.Collections.Generic;
using System.Linq;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMapItemCollection : List<Pair>
    {
        public Type[] Views => System.Linq.Enumerable.Select(this, collection => collection.View).ToArray();
        public Type[] ViewModels => System.Linq.Enumerable.Select(this, collection => collection.ViewModel).ToArray();

        public ViewMapItemCollection(IEnumerable<Pair> item):base(item)
        {

        }

        public ViewMapItemCollection()
        {

        }
    }
}
