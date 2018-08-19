﻿namespace Smart.Windows.Interactivity
{
    using System;
    using System.Windows;
    using System.Windows.Interactivity;
    using System.Windows.Interop;

    [TypeConstraint(typeof(Window))]
    public sealed class MinimizedToHideBehavior : Behavior<Window>
    {
        private readonly HwndSourceHook hook;

        public MinimizedToHideBehavior()
        {
            hook = WndProc;
        }

        protected override void OnAttached()
        {
            AssociatedObject.SourceInitialized += SourceInitialized;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.SourceInitialized -= SourceInitialized;
            UnregisterHook();
        }

        private void SourceInitialized(object sender, EventArgs eventArgs)
        {
            RegisterHook();
        }

        private void RegisterHook()
        {
            (PresentationSource.FromVisual(AssociatedObject) as HwndSource)?.AddHook(hook);
        }

        private void UnregisterHook()
        {
            (PresentationSource.FromVisual(AssociatedObject) as HwndSource)?.RemoveHook(hook);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Compatibility.")]
        private IntPtr WndProc(IntPtr hwnd, Int32 msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // WM_SYSCOMMAND, SC_MINIMIZE
            if ((msg == 0x0112) && (wParam.ToInt32() == 0xf020))
            {
                AssociatedObject.Hide();
                handled = true;
            }

            return IntPtr.Zero;
        }
    }
}
