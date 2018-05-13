using System;
using System.Globalization;
using Xamarin.Forms;

namespace Mowards.Converters
{
    public class IntToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
                return false;

            int currentValue = 0;
            if(int.TryParse(value.ToString(), out currentValue))
            {
                if (currentValue == 1) return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
