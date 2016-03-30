using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MobileAppX.Converters
{
    class BooleanToNonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
           
            var isFavorite = (bool)value;

            if (isFavorite)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
