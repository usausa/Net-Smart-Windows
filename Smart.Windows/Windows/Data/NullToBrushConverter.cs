﻿namespace Smart.Windows.Data
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    ///
    /// </summary>
    [ValueConversion(typeof(object), typeof(Brush))]
    public sealed class NullToBrushConverter : IValueConverter
    {
        /// <summary>
        ///
        /// </summary>
        public Brush NullBrush { get; set; } = Brushes.Transparent;

        /// <summary>
        ///
        /// </summary>
        public Brush NonNullBrush { get; set; } = Brushes.Transparent;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? NullBrush : NonNullBrush;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}