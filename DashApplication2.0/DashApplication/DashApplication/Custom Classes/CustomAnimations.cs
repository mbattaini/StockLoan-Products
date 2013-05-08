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

namespace DashApplication
{
    public class CustomAnimations
    {
        const double xSHAKEFROM = -5.0;
        const double xSHAKETO = 5.0;
        const int xSHAKEDURATION = 70;

        const double xFADEINFROM = 0.0;
        const double xFADEINTO = 1.0;
        const int xFADEINDURATION = 2;

        const double xFADEOUTFROM = 1.0;
        const double xFADEOUTTO = 0.0;
        const int xFADEOUTDURATION = 2;

        const double xSHIMMYFROM = -5.0;
        const double xSHIMMYTO = 5.0;
        const int xSHIMMYDURATION = 70;

        public static void FadeIn(object target)
        {
            Fade(target, xFADEINFROM, xFADEINTO, xFADEINDURATION);
        }

        public static void FadeOut(object target)
        {
            Fade(target, xFADEOUTFROM, xFADEOUTTO, xFADEOUTDURATION);
        }
        
        public static void Fade(object target, double from, double to, int durationSeconds)
        {            
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(0, 0, durationSeconds);
           
            DoubleAnimation animation = new DoubleAnimation();

            animation.From = from;
            animation.To = to;
            animation.Duration = new Duration(duration);            
            Storyboard.SetTarget(animation, (System.Windows.DependencyObject)target);
            Storyboard.SetTargetProperty(animation, new PropertyPath(Control.OpacityProperty));
            
            storyboard.Children.Add(animation);
            
            storyboard.Begin();
        }

        public static void Shake(object target)
        {
            Shake(target, xSHAKEFROM, xSHAKETO, xSHAKEDURATION);
        }

        public static void Shake(object target, double from, double to, int durationSeconds)
        {
            // Create a storyboard to contain the animations.
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, durationSeconds);

            // Create a DoubleAnimation to fade the not selected option control
            DoubleAnimation animation = new DoubleAnimation();

            animation.From = from;
            animation.To = to;
            animation.FillBehavior = FillBehavior.Stop;
            animation.Duration = new Duration(duration);
            animation.AutoReverse = true;

            PlaneProjection pp = new PlaneProjection();
            ((UIElement)(target)).Projection = pp;

            Storyboard.SetTarget(animation, pp);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PlaneProjection.RotationZProperty));

            storyboard.Children.Add(animation);

            storyboard.Begin();
        }

        public static void Shimmy(object target)
        {
            Shimmy(target, xSHIMMYFROM, xSHIMMYTO, xSHIMMYDURATION);
        }
        
        public static void Shimmy(object target, double from, double to, int durationSeconds)
        {
            Storyboard storyboard = new Storyboard();
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, durationSeconds);

            DoubleAnimation animation = new DoubleAnimation();

            animation.From = from;
            animation.To = to;
            animation.FillBehavior = FillBehavior.Stop;
            animation.Duration = new Duration(duration);
            animation.AutoReverse = true;

            

            Projection pp = new PlaneProjection();
            ((UIElement)(target)).Projection = pp;

            Storyboard.SetTarget(animation, pp);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PlaneProjection.GlobalOffsetXProperty));

            storyboard.Children.Add(animation);
            
            storyboard.Begin();
        }

    }
}