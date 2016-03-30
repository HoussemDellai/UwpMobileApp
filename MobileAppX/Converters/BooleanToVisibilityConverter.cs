using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MobileAppX.Converters
{
    class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
           
            var isFavorite = (bool)value;

            if (isFavorite)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
