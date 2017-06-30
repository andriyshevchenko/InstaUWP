using System.Collections.Generic;

namespace GalaSoft.MvvmLight.Extensions.Xaml
{
    public class ViewMapItemCollection : List<ViewMapItem>
    {
        public ViewMapItemCollection(IEnumerable<ViewMapItem> item):base(item)
        {

        }

        public ViewMapItemCollection()
        {

        }
    }
}
