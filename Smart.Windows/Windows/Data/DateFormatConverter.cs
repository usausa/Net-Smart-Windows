﻿namespace Smart.Windows.Data
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    ///
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateFormatConverter : IValueConverter
    {
        /// <summary>
        ///
        /// </summary>
        public string Format { get; set; } = "HH:mm:ss.fff";

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
            if (value == null)
            {
                return string.Empty;
            }

            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString(Format, culture);
            }

            if (value is DateTime dateTime)
            {
                return dateTime.ToString(Format, culture);
            }

            return string.Empty;
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
            var str = value as string;
            if (String.IsNullOrEmpty(str))
            {
                return null;
            }

            return DateTime.ParseExact(str, Format, culture);
        }
    }
}
