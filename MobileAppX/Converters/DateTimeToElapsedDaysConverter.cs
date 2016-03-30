using System;
using Windows.UI.Xaml.Data;

namespace MobileAppX.Converters
{
    class DateTimeToElapsedDaysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime == false)
            {
                return value;
            }

            var dateTime = (DateTime)value;

            var elapsedDays = (DateTime.Now - dateTime).Days;

            if (elapsedDays == 0)
            {
                return "today";
            }
            if (elapsedDays > 365)
            {
                return elapsedDays / 365 + " years";
            }
            if (elapsedDays > 30)
            {
                return elapsedDays / 30 + " months";
            }

            return elapsedDays + " days";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
