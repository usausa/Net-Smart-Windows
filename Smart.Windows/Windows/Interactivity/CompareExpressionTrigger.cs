﻿namespace Smart.Windows.Interactivity
{
    using System.Windows;

    using Microsoft.Xaml.Behaviors;

    using Smart.Windows.Expressions;

    [TypeConstraint(typeof(DependencyObject))]
    public class CompareExpressionTrigger : TriggerBase<DependencyObject>
    {
        public static readonly DependencyProperty BindingProperty = DependencyProperty.Register(
            nameof(Binding),
            typeof(object),
            typeof(CompareExpressionTrigger),
            new PropertyMetadata(HandlePropertyChanged));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(object),
            typeof(CompareExpressionTrigger),
            new PropertyMetadata(HandlePropertyChanged));

        public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register(
            nameof(Expression),
            typeof(ICompareExpression),
            typeof(CompareExpressionTrigger),
            new PropertyMetadata(CompareExpressions.Equal));

        public object Binding
        {
            get => GetValue(BindingProperty);
            set => SetValue(BindingProperty, value);
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public ICompareExpression Expression
        {
            get => (ICompareExpression)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        private static void HandlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
            {
                return;
            }

            ((CompareExpressionTrigger)d).HandlePropertyChanged();
        }

        private void HandlePropertyChanged()
        {
            if (Expression.Eval(Binding, Value))
            {
                InvokeActions(null);
            }
        }
    }
}