﻿namespace Smart.Windows.Data
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    public sealed class AllConverter : IMultiValueConverter
    {
        public bool Invert { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.All(value => value is bool b && b) ? !Invert : Invert;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
