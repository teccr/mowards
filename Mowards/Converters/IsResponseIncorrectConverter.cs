using System;
using System.Globalization;
using Mowards.Models;
using Xamarin.Forms;

namespace Mowards.Converters
{
    public class IsResponseIncorrectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            var triviaProxy = value as TriviaAnswer;

            if (triviaProxy == null)
                return false;

            if (triviaProxy.UserAnswer == null)
                return false;

            if (triviaProxy.Award.Nominee
               != triviaProxy.UserAnswer.Nominee)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
