﻿namespace Smart.Windows.Interactivity
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    /// <summary>
    ///
    /// </summary>
    [TypeConstraint(typeof(DependencyObject))]
    public sealed class SetFocusAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            nameof(Target),
            typeof(FrameworkElement),
            typeof(SetFocusAction),
            new PropertyMetadata(default(FrameworkElement)));

        /// <summary>
        ///
        /// </summary>
        public FrameworkElement Target
        {
            get => (FrameworkElement)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            var element = Target ?? (AssociatedObject as FrameworkElement);
            if (element == null)
            {
                return;
            }

            if (!element.Focus())
            {
                var fs = FocusManager.GetFocusScope(element);
                FocusManager.SetFocusedElement(fs, element);
            }
        }
    }
}
