using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HangmanGame.UI.Converters
{
    /// <summary>
    ///     Converter that returns a color dependent on a bool? that is passed to it. Used in UI for
    ///     the visual indication for a win or a loss
    /// </summary>
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (bool?) value;

            switch (state)
            {
                case true:
                    return Brushes.LightGreen;
                case false:
                    return Brushes.LightCoral;
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}