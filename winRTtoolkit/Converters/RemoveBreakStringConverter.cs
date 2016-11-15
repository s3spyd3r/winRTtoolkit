using System;
using System.Diagnostics;
using Windows.UI.Xaml.Data;

namespace winRTtoolkit.Converters
{
    public class RemoveBreakStringConverter : IValueConverter
    {
        private string _originalValue;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                _originalValue = (string)value;

                if (_originalValue.Contains(Environment.NewLine))
                    return _originalValue.Replace(Environment.NewLine, " ");
                else
                    return _originalValue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return _originalValue;
        }
    }
}
