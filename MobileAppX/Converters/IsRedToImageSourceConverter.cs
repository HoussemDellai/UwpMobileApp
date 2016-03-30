using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MobileAppX.Converters
{
    public class IsRedToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var isRed = (bool) value;

            if (isRed)
            {
                return "../Images.read.png";
            }

            return "../Images.unread.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
