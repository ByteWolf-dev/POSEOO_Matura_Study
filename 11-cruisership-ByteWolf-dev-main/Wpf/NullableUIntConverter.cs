namespace Wpf;

using System.Globalization;
using System.Windows.Data;

public class NullableUIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        uint number;
        if (uint.TryParse((string)value, out number))
        {
            return number;
        }
        else
        {
            return null;
        }
    }
}