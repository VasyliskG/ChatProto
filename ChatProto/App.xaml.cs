﻿using ChatProto.Services;
using Microsoft.UI.Xaml;

namespace ChatProto;
public partial class App : Application
{
    public static AuthService AuthService { get; private set; } = null!;
    public static MainWindow MainWindowRef { get; private set; }
    public static MainWindow MainWindow { get; private set; } = null!;

    public App()
    {
        this.InitializeComponent();
        AuthService = new AuthService();
    }

    /// <summary>
    /// Виконується при запуску програми.
    /// </summary>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        if (string.IsNullOrEmpty(Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride))
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US";
        }

        MainWindow = new MainWindow();
        MainWindowRef = MainWindow;
        MainWindow.InitializeTitleBar();
        MainWindow.Activate();
    }
}
