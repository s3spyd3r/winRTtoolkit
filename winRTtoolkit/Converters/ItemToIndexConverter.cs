using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using winRTtoolkit.Helpers;

namespace winRTtoolkit.Converters
{
    public class ItemToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var parent = VisualElementsHelper.GetParentByType<ListView>(value as DependencyObject);
            var item = VisualElementsHelper.GetParentByType<ListViewItem>(value as DependencyObject);
            if (parent != null && item != null)
            {
                var index = parent.IndexFromContainer(item);
                return index + 1;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
