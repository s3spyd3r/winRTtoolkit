using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace winRTtoolkit.Animations
{
    public class CustomAnimations
    {
        public static void ShowUIElementAnimation(UIElement label, double duration)
        {
            try
            {
                label.Visibility = Visibility.Visible;
                var storyboard = new Storyboard();
                var fadeIn = new DoubleAnimation
                {
                    To = 1,
                    Duration = TimeSpan.FromSeconds(duration),
                    EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn }
                };

                Storyboard.SetTarget(fadeIn, label);
                Storyboard.SetTargetProperty(fadeIn, "(UIElement.Opacity)");
                storyboard.Children.Add(fadeIn);
                storyboard.Begin();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        public static void HideUIElementAnimation(UIElement label, double duration)
        {
            try
            {
                label.Visibility = Visibility.Visible;
                var storyboard = new Storyboard();
                var fadeIn = new DoubleAnimation
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(duration),
                    EasingFunction = new CircleEase { EasingMode = EasingMode.EaseIn }
                };

                Storyboard.SetTarget(fadeIn, label);
                Storyboard.SetTargetProperty(fadeIn, "(UIElement.Opacity)");
                storyboard.Children.Add(fadeIn);
                storyboard.Begin();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        public static void OnUIElementLoaded(object sender, double duration = 0.5, bool reverse = false)
        {
            try
            {
                double to = 1;
                var localImage = sender as UIElement;

                if (localImage != null)
                {
                    var storyBoard = new Storyboard();

                    if (reverse)
                    {
                        to = 0;
                    }

                    var doubleAnimation = new DoubleAnimation
                    {
                        To = to,
                        Duration = new Duration(TimeSpan.FromSeconds(duration))
                    };
                    Storyboard.SetTarget(doubleAnimation, (UIElement)sender);
                    Storyboard.SetTargetProperty(doubleAnimation, "(UIElement.Opacity)");

                    storyBoard.Children.Add(doubleAnimation);

                    if (((FrameworkElement)sender).Tag != null)
                    {
                        var hideAnimation = new DoubleAnimation
                        {
                            From = 1,
                            To = 0,
                            Duration = new Duration(TimeSpan.FromSeconds(duration))
                        };
                        Storyboard.SetTarget(hideAnimation, (UIElement)((FrameworkElement)sender).Tag);
                        Storyboard.SetTargetProperty(hideAnimation, "(UIElement.Opacity)");
                        storyBoard.Children.Add(hideAnimation);

                        var objectAnimationUsingKeyFrames = new ObjectAnimationUsingKeyFrames
                        {
                            Duration = new Duration(TimeSpan.FromSeconds(duration))
                        };
                        Storyboard.SetTarget(objectAnimationUsingKeyFrames, (UIElement)((FrameworkElement)sender).Tag);
                        Storyboard.SetTargetProperty(objectAnimationUsingKeyFrames, "(Visibility)");
                        var discreteObjectKeyFrame = new DiscreteObjectKeyFrame
                        {
                            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(duration)),
                            Value = Visibility.Collapsed
                        };

                        objectAnimationUsingKeyFrames.KeyFrames.Add(discreteObjectKeyFrame);
                        storyBoard.Children.Add(objectAnimationUsingKeyFrames);
                    }

                    storyBoard.Begin();
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }
    }
}