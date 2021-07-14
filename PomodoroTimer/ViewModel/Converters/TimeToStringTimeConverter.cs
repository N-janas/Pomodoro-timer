using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace PomodoroTimer.ViewModel.Converters
{
    class TimeToStringTimeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(x => x == DependencyProperty.UnsetValue))
                return DependencyProperty.UnsetValue;

            string mins = values[0].ToString();
            string secs = values[1].ToString();

            if ((int)values[0] < 10)
                mins = $"0{mins}";
            if ((int)values[1] < 10)
                secs = $"0{secs}";

            if ((int)values[0] <= 0 && (int)values[1] <= 0)
            {
                mins = "00";
                secs = "00";
            }
            return $"{mins}:{secs}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
