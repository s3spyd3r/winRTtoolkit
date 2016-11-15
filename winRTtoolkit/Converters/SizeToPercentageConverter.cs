using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace winRTtoolkit.Converters
{
    public class SizeToPercentageConverter : IValueConverter
    {
        private double _originalValue;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var doubleValue = value as double? ?? 0;
            var factor = double.Parse(parameter as string, CultureInfo.InvariantCulture);
            _originalValue = doubleValue;

            return doubleValue * factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return _originalValue;
        }
    }
}
