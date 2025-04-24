using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace ChatProto.Helpers;

/// <summary>
/// UIHelper використовується для оновлення станів кнопок та фону панелей при змінах теми.
/// </summary>
public static class UIHelper
{
    /// <summary>
    /// Оновлює стан кнопки, щоб відобразити зміни теми.
    /// </summary>
    /// <param name="control">Кнопка, стан якої потрібно оновити.</param>
    public static void RefreshButtonState(Control control)
    {
        if (control != null)
        {
            VisualStateManager.GoToState(control, "Focused", true);
            VisualStateManager.GoToState(control, "Normal", true);
        }
    }

    /// <summary>
    /// Оновлює всі кнопки в заданому батьківському елементі, щоб відобразити зміни теми.
    /// </summary>
    /// <param name="parent">Батьківський елемент, у якому потрібно оновити стан кнопок.</param>
    public static void RefreshAllButtons(DependencyObject parent)
    {
        if (parent == null)
        {
            return;
        }

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

    /// <summary>
    /// Оновлює фон панелі, щоб відобразити зміни теми.
    /// </summary>
    /// <param name="panel">Поточна панель.</param>
    /// <param name="resourceKey">Ключ ресурса в App.</param>
    public static void RefreshBackground(Panel panel, string resourceKey)
    {
        if (panel != null && Application.Current.Resources.TryGetValue(resourceKey, out var resource) && resource is Brush brush)
        {
            panel.Background = brush;
        }
    }
}
