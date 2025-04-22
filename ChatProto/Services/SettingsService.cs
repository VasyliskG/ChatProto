using Microsoft.UI.Xaml;
using System;
using Windows.Storage;

namespace ChatProto.Services;

public class SettingsService
{
    private const string ThemeKey = "Theme";
    private readonly ApplicationDataContainer _localSettings;

    public event EventHandler<ElementTheme>? ThemeChanged;

    public SettingsService()
    {
        _localSettings = ApplicationData.Current.LocalSettings;

        if (!_localSettings.Values.ContainsKey(ThemeKey))
        {
            _localSettings.Values[ThemeKey] = ElementTheme.Default.ToString();
        }
    }

    public ElementTheme CurrentTheme
    {
        get
        {
            string themeString = _localSettings.Values[ThemeKey]?.ToString() ?? ElementTheme.Default.ToString();
            return Enum.TryParse<ElementTheme>(themeString, out var theme) ? theme : ElementTheme.Default;
        }
        set
        {
            if (value != CurrentTheme)
            {
                _localSettings.Values[ThemeKey] = value.ToString();
                ThemeChanged?.Invoke(this, value);
            }
        }
    }
}
