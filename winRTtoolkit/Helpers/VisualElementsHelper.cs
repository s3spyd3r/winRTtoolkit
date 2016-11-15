using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace winRTtoolkit.Helpers
{
    public class VisualElementsHelper
    {
        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            var obj = depObj as ScrollViewer;
            if (obj != null) return obj;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    var children = child as T;
                    if (children != null)
                    {
                        yield return children;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static T GetParentByType<T>(DependencyObject element) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);

            while (parent != null)
            {
                var result = parent as T;

                if (result != null)
                {
                    return result;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }
    }
}
