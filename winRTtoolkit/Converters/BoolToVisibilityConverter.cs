using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace winRTtoolkit.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        private bool _originalValue;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isVisible = value is bool && (bool)value;
            _originalValue = isVisible;

            return isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return _originalValue;
        }
    }
}
