﻿namespace Smart.Windows.Interactivity
{
    using System.Windows;
    using System.Windows.Interactivity;
    using System.Windows.Media.Effects;

    [TypeConstraint(typeof(FrameworkElement))]
    public sealed class ApplyEffectAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty EffectProperty = DependencyProperty.Register(
            nameof(Effect),
            typeof(Effect),
            typeof(ApplyEffectAction),
            new PropertyMetadata(null));

        public Effect Effect
        {
            get => (Effect)GetValue(EffectProperty);
            set => SetValue(EffectProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            AssociatedObject.Effect = parameter as Effect ?? Effect;
        }
    }
}
