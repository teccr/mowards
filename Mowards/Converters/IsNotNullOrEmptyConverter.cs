using System;
using System.Globalization;
using Xamarin.Forms;

namespace Mowards.Converters
{
    public class IsNotNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool differentFromNull = value != null ? true : false;
            differentFromNull = differentFromNull ? !string.IsNullOrWhiteSpace(value.ToString().Trim()) : false;
            return differentFromNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
