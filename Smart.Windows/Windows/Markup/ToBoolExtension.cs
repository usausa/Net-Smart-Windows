namespace Smart.Windows.Markup
{
    using System;
    using System.Windows.Markup;

    using Smart.Windows.Data;

    [MarkupExtensionReturnType(typeof(TextToBoolConverter))]
    public sealed class TextToBoolExtension : MarkupExtension
    {
        public string True { get; set; }

        public string False { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new TextToBoolConverter { TrueValue = True, FalseValue = False };
        }
    }

    [MarkupExtensionReturnType(typeof(IntToBoolConverter))]
    public sealed class IntToBoolExtension : MarkupExtension
    {
        public int True { get; set; }

        public int False { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new IntToBoolConverter { TrueValue = True, FalseValue = False };
        }
    }
}
