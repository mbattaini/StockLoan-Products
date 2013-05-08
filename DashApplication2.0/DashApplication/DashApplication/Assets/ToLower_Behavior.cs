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
    public class ToLower_Behavior
    {
        // Dependency property and setter/getter
        public static DependencyProperty ToLower_Property = DependencyProperty.RegisterAttached(
            "ToLower_", typeof(bool), typeof(ToLower_Behavior), new PropertyMetadata(ToLower_PropertyChanged));
        public static void SetToLower_(DependencyObject obj, bool toLower_) { obj.SetValue(ToLower_Property, toLower_); }
        public static bool GetToLower_(DependencyObject obj) { return (bool)obj.GetValue(ToLower_Property); }

        // When value set to true on TextBlock or ContentControl add loaded event to control
        public static void ToLower_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (ToLower_Behavior.GetToLower_(d))
            {
                if (d.GetType().ToString() == "System.Windows.Controls.TextBlock")
                {
                    TextBlock _tb = (TextBlock)d;
                    _tb.Text = _tb.Text.ToLower();
                    _tb.Loaded += new RoutedEventHandler(_tb_Loaded);
                }
                else if (d.GetType().ToString() == "System.Windows.Controls.ContentPresenter")
                {
                    ContentPresenter _cc = (ContentPresenter)d;
                    if (_cc.Content.GetType().ToString() == "System.String")
                    {
                        _cc.Content = _cc.Content.ToString().ToLower();
                    }
                    _cc.Loaded += new RoutedEventHandler(_cc_Loaded);
                }
            }
        }

        // Loaded event for TextBlock that touppers text
        private static void _tb_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock _tb = (TextBlock)sender;
            _tb.Text = _tb.Text.ToLower();
        }

        // Loaded event for ContentPresenter that touppers text
        private static void _cc_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPresenter _cc = (ContentPresenter)sender;
            if (_cc.Content.GetType().ToString() == "System.String")
            {
                _cc.Content = _cc.Content.ToString().ToLower();
            }
        }
    }
}
