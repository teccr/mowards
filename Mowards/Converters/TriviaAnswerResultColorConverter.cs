using System;
using System.Globalization;
using Mowards.Models;
using Xamarin.Forms;

namespace Mowards.Converters
{
    public class TriviaAnswerResultColorConverter : IValueConverter
    {
        private double NoAction = Color.Transparent.R;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return NoAction;

            var userAnswer = value as Award;
            var correctAnswer = parameter as Award;

            if (userAnswer == null || correctAnswer == null)
                return NoAction;

            if(userAnswer.Nominee == correctAnswer.Nominee)
            {
                return Color.Green.R;
            }
            else
            {
                return Color.Red.R;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
