using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace ChatProto.Helpers;

public static class UIHelper
{
    public static void RefreshButtonState(Control control)
    {
        if (control != null)
        {
            VisualStateManager.GoToState(control, "Focused", true);
            VisualStateManager.GoToState(control, "Normal", true);
        }
    }

    public static void RefreshAllButtons(DependencyObject parent)
    {
        if (parent == null)
            return;

        int count = VisualTreeHelper.GetChildrenCount(parent);

        for (int i = 0; i < count; i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, i);

            if (child is Control control && control is Button)
            {
                RefreshButtonState(control);
            }

            RefreshAllButtons(child);
        }
    }

    public static void RefreshBackground(Panel panel, string resourceKey)
    {
        if (panel != null && Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is Brush brush)
        {
            panel.Background = brush;
        }
    }
}
