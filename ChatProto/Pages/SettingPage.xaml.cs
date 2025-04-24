using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ChatProto.Pages;

public sealed partial class SettingPage : Page
{
    public SettingPage()
    {
        this.InitializeComponent();
    }

    /// <summary>
    ///  Обробляє подію зміни вибору радіо кнопок теми.
    /// </summary>
    private void ThemeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeRadioButtons.SelectedItem is RadioButton selectedRadio)
        {
            var newTheme = selectedRadio.Tag.ToString() == "Light" ? ElementTheme.Light : ElementTheme.Dark;

            if (App.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ChangeWindowTheme(newTheme.ToString());
            }
        }
    }

    /// <summary>
    /// Обробляє подію зміни вибору мовних радіо кнопок.
    /// </summary>
    private void LanguageRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LanguageRadioButtons.SelectedItem is RadioButton selectedRadio)
        {
            string languageTag = selectedRadio.Tag.ToString()!;
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = languageTag;

            switch(languageTag)
            {
                case "en-US":
                    SuccessInfoBar.Message = "Language changed. Restart the app to apply changes.";
                    SuccessInfoBar.IsOpen = true;
                    break;
                case "uk-UA":
                    SuccessInfoBar.Message = "Мова змінилася. Перезапустіть програму, щоб застосувати зміни.";
                    SuccessInfoBar.IsOpen = true;
                    break;
                default:
                    SuccessInfoBar.Message = "Language changed. Restart the app to apply changes.";
                    SuccessInfoBar.IsOpen = true;
                    break;
            }
        }
    }
}