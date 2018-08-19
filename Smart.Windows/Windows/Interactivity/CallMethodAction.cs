﻿namespace Smart.Windows.Interactivity
{
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;

    [TypeConstraint(typeof(DependencyObject))]
    public sealed class CallMethodAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register(
            nameof(TargetObject),
            typeof(object),
            typeof(CallMethodAction),
            new PropertyMetadata(HandleTargetObjectChanged));

        public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(
            nameof(MethodName),
            typeof(string),
            typeof(CallMethodAction),
            new PropertyMetadata(HandleMethodNameChanged));

        public static readonly DependencyProperty MethodParameterProperty = DependencyProperty.Register(
            nameof(MethodParameter),
            typeof(object),
            typeof(CallMethodAction),
            new PropertyMetadata(default(object)));

        public static readonly DependencyProperty ConverterProperty = DependencyProperty.Register(
            nameof(Converter),
            typeof(IValueConverter),
            typeof(CallMethodAction),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ConverterParameterProperty = DependencyProperty.Register(
            nameof(ConverterParameter),
            typeof(object),
            typeof(CallMethodAction),
            new PropertyMetadata(null));

        public object TargetObject
        {
            get => GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        public string MethodName
        {
            get => (string)GetValue(MethodNameProperty);
            set => SetValue(MethodNameProperty, value);
        }

        public object MethodParameter
        {
            get => GetValue(MethodParameterProperty);
            set => SetValue(MethodParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(ConverterProperty);
            set => SetValue(ConverterProperty, value);
        }

        public object ConverterParameter
        {
            get => GetValue(ConverterParameterProperty);
            set => SetValue(ConverterParameterProperty, value);
        }

        private MethodDescriptor cachedMethod;

        protected override void Invoke(object parameter)
        {
            var target = TargetObject ?? AssociatedObject;
            var methodName = MethodName;
            if ((target == null) || (methodName == null))
            {
                return;
            }

            if ((cachedMethod == null) ||
                (cachedMethod.Method.DeclaringType != target.GetType() ||
                 (cachedMethod.Method.Name != methodName)))
            {
                var methodInfo = target.GetType().GetRuntimeMethods().FirstOrDefault(m =>
                    m.Name == methodName &&
                    ((m.GetParameters().Length == 0) ||
                     ((m.GetParameters().Length == 1) &&
                      ((MethodParameter == null) ||
                       MethodParameter.GetType().GetTypeInfo().IsAssignableFrom(m.GetParameters()[0].ParameterType.GetTypeInfo())))));
                if (methodInfo == null)
                {
                    return;
                }

                cachedMethod = new MethodDescriptor(methodInfo, methodInfo.GetParameters().Length > 0);
            }

            if (cachedMethod.HasParameter)
            {
                var methodParameter = MethodParameter;
                var argument = (methodParameter != null) || this.IsSet(MethodNameProperty)
                    ? methodParameter
                    : Converter?.Convert(parameter, typeof(object), ConverterParameter, null) ?? parameter;
                cachedMethod.Method.Invoke(target, new[] { argument });
            }
            else
            {
                cachedMethod.Method.Invoke(target, null);
            }
        }

        private static void HandleTargetObjectChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((CallMethodAction)sender).cachedMethod = null;
        }

        private static void HandleMethodNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((CallMethodAction)sender).cachedMethod = null;
        }
    }
}
