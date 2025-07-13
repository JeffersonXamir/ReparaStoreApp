using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ReparaStoreApp.WPF.Helpers
{
    public static class VisualTreeHelperExtensions
    {
        public static T GetParentOfType<T>(this DependencyObject child) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent == null) return null;
            if (parent is T typedParent) return typedParent;
            return GetParentOfType<T>(parent);
        }
    }
}
