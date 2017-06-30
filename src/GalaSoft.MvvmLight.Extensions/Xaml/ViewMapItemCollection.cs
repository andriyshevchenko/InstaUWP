using System.Collections.Generic;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMapItemCollection : List<Pair>
    {
        public ViewMapItemCollection(IEnumerable<Pair> item):base(item)
        {

        }

        public ViewMapItemCollection()
        {

        }
    }
}
