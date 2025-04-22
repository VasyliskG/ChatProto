using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ChatProto.Helpers;
using ChatProto.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ChatProto.Pages;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingPage : Page
{
    public SettingPage()
    {
        this.InitializeComponent();
    }

    private void ThemeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeRadioButtons.SelectedItem is RadioButton selectedRadio)
        {
            var newTheme = selectedRadio.Tag.ToString() == "Light" ? ElementTheme.Light : ElementTheme.Dark;

            if (App.MainWindow is MainWindow mainWindow)
            {
                mainWindow.ChangeЦindowЕheme(newTheme.ToString());
            }
        }
    }

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