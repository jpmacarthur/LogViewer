using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LogViewer.Viewer
{
    public partial class Viewer
    {
        [ValueConversion(typeof(DateTime), typeof(String))]
        public class DateConverter : IValueConverter
        {
            private const string _format = "dd-MM-yy";

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                DateTime date = (DateTime)value;

                return date.ToString(_format);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DateTime.ParseExact((string)value, _format, culture);
            }

        }
        public class TimeConverer : IValueConverter
        {
            private const string _format = "HH-mm-ss";

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                DateTime date = (DateTime)value;

                return date.ToString(_format);
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DateTime.ParseExact((string)value, _format, culture);
            }

        }
    }
}
