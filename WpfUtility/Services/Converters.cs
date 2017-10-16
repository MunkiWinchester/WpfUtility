using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace WpfUtility.Services
{
    /// <summary>
    ///     Converts true or false to one of two given values of type T
    /// </summary>
    /// <typeparam name="T">Type which is converted</typeparam>
    public class BooleanConverter<T> : IValueConverter
    {
        /// <summary>
        ///     Constructor for the boolean converter
        /// </summary>
        /// <param name="trueValue">Value which is equal to true</param>
        /// <param name="falseValue">Value which is equal to false</param>
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        /// <summary>
        ///     Value which is equal to true
        /// </summary>
        public T True { get; set; }

        /// <summary>
        ///     Value which is equal to false
        /// </summary>
        public T False { get; set; }

        /// <summary>
        ///     Converts the given bool value to equally set value
        /// </summary>
        /// <param name="value">Bool which is "converted"</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>The equally set value to the bool</returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // checks if the value is bool and returns the "true" or "false" value
            return value is bool && (bool) value ? True : False;
        }

        /// <summary>
        ///     Converts the given value back to the equally set bool
        /// </summary>
        /// <param name="value">Bool which is "converted"</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>The equally set value to the bool</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T) value, True);
        }
    }

    /// <summary>
    ///     Converts true to Visibility.Collapsed or false to Visibility.Visible
    /// </summary>
    public sealed class NegatedBooleanToVisibilityConverter : BooleanConverter<Visibility>
    {
        /// <summary>
        ///     Converts true to Visibility.Collapsed or false to Visibility.Visible
        /// </summary>
        public NegatedBooleanToVisibilityConverter() :
            base(Visibility.Collapsed, Visibility.Visible)
        {
        }
    }

    /// <summary>
    ///     Converts true to false or false to true
    /// </summary>
    public sealed class NegatedBooleanConverter : BooleanConverter<bool>
    {
        /// <summary>
        ///     Converts true to false or false to true
        /// </summary>
        public NegatedBooleanConverter() : base(false, true)
        {
        }
    }

    /// <summary>
    ///     Converts a Drawing.Color to a solid color brush
    /// </summary>
    public sealed class DrawingColorConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a Drawing.Color to a solid color brush
        /// </summary>
        /// <param name="value">Value which is a Drawing.Color</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>SolidColorBrush</returns>
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

        /// <summary>
        ///     Converts a solid color brush to a Drawing.Color
        /// </summary>
        /// <param name="value">SolidColorBrush</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Drawing.Color</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            if (brush != null)
                return Color.FromArgb(brush.Color.A, brush.Color.R, brush.Color.G, brush.Color.B);
            return Color.Black;
        }
    }

    /// <summary>
    ///     Subtratcs an Int (parameter) from a given Int (value)
    ///     E.g. ActualWidth - Spacing
    /// </summary>
    public class SubtractIntConverter : IValueConverter
    {
        /// <summary>
        ///     Subtratcs an Int (parameter) from a given Int (value)
        ///     E.g. ActualWidth - Spacing
        /// </summary>
        /// <param name="value">Int from which is subtracted</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Int which is subtracted from value</param>
        /// <param name="culture">Not used</param>
        /// <returns>Value - paramter</returns>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null || !int.TryParse(value.ToString(), out var parsedValue))
                return 0;
            if (parameter == null || !int.TryParse(parameter.ToString(), out var parsedParameter))
                return parsedValue;
            var returnValue = parsedValue - parsedParameter;
            return returnValue > 0 ? returnValue : 0;
        }

        /// <summary>
        ///     Adds an Int (parameter) back to a given int (value)
        /// </summary>
        /// <param name="value">Int from which was subtracted</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Int which was subtracted from value</param>
        /// <param name="culture">Not used</param>
        /// <returns>Value + parameter</returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null || !int.TryParse(value.ToString(), out var parsedValue))
                return 0;
            if (parameter != null && int.TryParse(parameter.ToString(), out var parsedParameter))
                return parsedValue + parsedParameter;
            return parsedValue;
        }
    }
}