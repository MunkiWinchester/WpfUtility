using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace WpfUtility
{
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool) value ? True : False;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T) value, True);
        }
    }

    public sealed class NegatedBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        public NegatedBooleanToVisibilityConverter() :
            base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }

    public sealed class NegatedBooleanConverter : BooleanConverter<bool>
    {
        public NegatedBooleanConverter() : base(false, true)
        {
        }
    }

    public sealed class DrawingColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color)
            {
                var color = (Color) value;
                return new SolidColorBrush(
                    System.Windows.Media.Color.FromArgb(255, color.R, color.G, color.B));
            }
            return new SolidColorBrush(new System.Windows.Media.Color {A = 255, R = 255, B = 255, G = 255});
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            if (brush != null)
                return Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
            return Color.Black;
        }
    }
}