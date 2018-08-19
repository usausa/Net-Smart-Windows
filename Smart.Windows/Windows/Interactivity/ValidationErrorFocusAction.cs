﻿namespace Smart.Windows.Interactivity
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    using Smart.Windows.Messaging;

    [TypeConstraint(typeof(DependencyObject))]
    public sealed class ValidationErrorFocusAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var element = AssociatedObject.FindChildrens<UIElement>().FirstOrDefault(Validation.GetHasError);
            if (element != null)
            {
                element.Focus();

                if (parameter is CancelMessage message)
                {
                    message.Cancel = true;
                }
            }
        }
    }
}
