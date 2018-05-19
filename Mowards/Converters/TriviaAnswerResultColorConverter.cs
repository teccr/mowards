using System;
using System.Globalization;
using Mowards.Models;
using Xamarin.Forms;

namespace Mowards.Converters
{
    public class TriviaAnswerResultColorConverter : IValueConverter
    {
        private Color NoAction = Color.DarkGoldenrod;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return NoAction;

            var triviaProxy = value as TriviaAnswer;

            if (triviaProxy == null)
                return NoAction;

            if (triviaProxy.UserAnswer == null)
                return NoAction;

            if(triviaProxy.Award.Nominee 
               == triviaProxy.UserAnswer.Nominee)
            {
                return NoAction;
            }
            else
            {
                return Color.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
