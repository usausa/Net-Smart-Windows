﻿namespace Smart.Windows.Interactivity
{
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interactivity;

    [TypeConstraint(typeof(DependencyObject))]
    public sealed class SetFocusAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            nameof(TargetObject),
            typeof(FrameworkElement),
            typeof(SetFocusAction),
            new PropertyMetadata(default(FrameworkElement)));

        public FrameworkElement TargetObject
        {
            get => (FrameworkElement)GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            var element = TargetObject ?? (AssociatedObject as FrameworkElement);
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
