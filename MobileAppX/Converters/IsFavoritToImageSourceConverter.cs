using System;
using System.Globalization;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace MobileAppX.Converters
{
    public class IsFavoritToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var isFavorit = (bool)value;

            if (isFavorit)
            {
                return "../Images.like_purple.png";
            }

            return "../Images.like.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}
