﻿namespace Smart.Windows.Interactivity
{
    using System.Windows.Controls.Primitives;
    using System.Windows.Interactivity;

    [TypeConstraint(typeof(TextBoxBase))]
    public class SelectAllTextAction : TriggerAction<TextBoxBase>
    {
        protected override void Invoke(object parameter)
        {
            AssociatedObject.SelectAll();
        }
    }
}
