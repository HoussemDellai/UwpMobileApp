using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace MobileAppX.Converters
{
    public class TimeSpanToTimeSpanFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is TimeSpan)
            {
                var timeSpan = (TimeSpan) value;

                if (timeSpan.Hours == 0)
                {
                    return string.Format("{0}:{1}", timeSpan.Minutes, timeSpan.Seconds);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
