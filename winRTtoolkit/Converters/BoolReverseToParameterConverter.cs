using System;
using Windows.UI.Xaml.Data;

namespace winRTtoolkit.Converters
{
    public class BoolReverseToParameterConverter : IValueConverter
    {
        private object _originalValue;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            _originalValue = value;
            if (value == null)
            {
                return null;
            }
            return (bool)value ? null : parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return _originalValue;
        }
    }
}