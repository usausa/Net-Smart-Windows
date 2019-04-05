﻿namespace Smart.Windows.Data
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    [ValueConversion(typeof(bool), typeof(Brush))]
    public sealed class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueValue { get; set; } = Brushes.Transparent;

        public Brush FalseValue { get; set; } = Brushes.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return FalseValue;
            }

            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
            {
                return true;
            }

            if (Equals(value, FalseValue))
            {
                return false;
            }

            return Binding.DoNothing;
        }
    }
}