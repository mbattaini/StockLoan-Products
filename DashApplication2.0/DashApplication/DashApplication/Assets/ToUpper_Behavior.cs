using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UsefulBehaviors
{
    public class ToUpper_Behavior
    {
        // Dependency property and setter/getter
        public static DependencyProperty ToUpper_Property = DependencyProperty.RegisterAttached(
            "ToUpper_", typeof(bool), typeof(ToUpper_Behavior), new PropertyMetadata(ToUpper_PropertyChanged));
        public static void SetToUpper_(DependencyObject obj, bool toUpper_) { obj.SetValue(ToUpper_Property, toUpper_); }
        public static bool GetToUpper_(DependencyObject obj) { return (bool)obj.GetValue(ToUpper_Property); }

        // When value set to true on TextBlock or ContentControl add loaded event to control
        public static void ToUpper_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (ToUpper_Behavior.GetToUpper_(d))
            {
                if (d.GetType().ToString() == "System.Windows.Controls.TextBlock")
                {
                    TextBlock _tb = (TextBlock)d;
                    _tb.Text = _tb.Text.ToUpper();
                    _tb.Loaded += new RoutedEventHandler(_tb_Loaded);
                }
                else if (d.GetType().ToString() == "System.Windows.Controls.ContentPresenter")
                {
                    ContentPresenter _cc = (ContentPresenter)d;
                    if (_cc.Content.GetType().ToString() == "System.String")
                    {
                        _cc.Content = _cc.Content.ToString().ToUpper();
                    }
                    _cc.Loaded += new RoutedEventHandler(_cc_Loaded);
                }
            }
        }

        // Loaded event for TextBlock that touppers text
        private static void _tb_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock _tb = (TextBlock)sender;
            _tb.Text = _tb.Text.ToUpper();
        }

        // Loaded event for ContentPresenter that touppers text
        private static void _cc_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPresenter _cc = (ContentPresenter)sender;
            if (_cc.Content.GetType().ToString() == "System.String")
            {
                _cc.Content = _cc.Content.ToString().ToUpper();
            }
        }
    }
}
