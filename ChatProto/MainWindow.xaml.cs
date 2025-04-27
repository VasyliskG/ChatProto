using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ChatProto.Helpers;
using ChatProto.Pages;
using ChatProto.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace ChatProto;

public sealed partial class MainWindow : Window
{
    public bool IsUserLoggedIn { get; set; } = true;

    public MainWindow()
    {
        this.InitializeComponent();

    }

    /// <summary>
    /// Ініціалізація заголовка вікна.
    /// </summary>
    public void InitializeTitleBar()
    {
        Window window = App.MainWindow;
        window.ExtendsContentIntoTitleBar = true;
        window.SetTitleBar(AppTitleBar);

        var appWindowPresenter = this.AppWindow?.Presenter as Microsoft.UI.Windowing.OverlappedPresenter;
        if (appWindowPresenter != null)
        {
            appWindowPresenter.PreferredMinimumHeight = 600;
            appWindowPresenter.PreferredMinimumWidth = 800;
        }
    }

    /// <summary>
    /// Обробка події зміни вибору елемента в навігаційній панелі.
    /// </summary>
    private void NavigationPane_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem selectedItem)
        {
            string pageTad = selectedItem.Tag.ToString()!;
            switch (pageTad)
            {
                case "Help":
                    var uri = new Uri("https://google.com");
                    Windows.System.Launcher.LaunchUriAsync(uri);
                    break;
                case "Menu":
                    if (Sidebar.Visibility == Visibility.Collapsed)
                    {
                        Sidebar.Visibility = Visibility.Visible;
                        var showStoryboard = (Storyboard)MainGrid.Resources["ShowSidebarAnimation"];
                        showStoryboard.Begin();

                        var expandSidebarAnimation = (Storyboard)MainGrid.Resources["ExpandSidebarAnimation"];
                        expandSidebarAnimation.Begin();
                    }
                    else
                    {
                        var hideStoryboard = (Storyboard)MainGrid.Resources["HideSidebarAnimation"];
                        var collapseSidebarAnimation = (Storyboard)MainGrid.Resources["CollapseSidebarAnimation"];

                        hideStoryboard.Begin();
                        collapseSidebarAnimation.Begin();

                        hideStoryboard.Completed += (s, ev) =>
                        {
                            Sidebar.Visibility = Visibility.Collapsed;
                        };
                    }

                    break;
                case "ChatPage":
                    ContentFrame.Navigate(typeof(ChatPage), this);
                    break;
            }

            sender.SelectedItem = null;
        }
    }

    /// <summary>
    /// Обробка події кнопки "Налаштувань".
    /// </summary>
    private void SettingButton_GettingFocus(UIElement sender, Microsoft.UI.Xaml.Input.GettingFocusEventArgs args)
    {
        if (ContentFrame.Content is SettingPage) return;

        ContentFrame.Navigate(typeof(SettingPage), this);
    }

    /// <summary>
    /// Обробка події кнопки "Акаунту".
    /// </summary>
    private void LoginButton_GettingFocus(UIElement sender, Microsoft.UI.Xaml.Input.GettingFocusEventArgs args)
    {
        if (ContentFrame.Content is RegisterPage) return;

        ContentFrame.Navigate(typeof(RegisterPage), this);
    }

    /// <summary>
    /// Змініа теми вікна.
    /// </summary>
    /// <param name="newTheme">Тема.</param>
    public void ChangeWindowTheme(string newTheme)
    {
        if (newTheme == "Light")
        {
            AppSettings.Current.CurrentTheme = ElementTheme.Light;
            if (App.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainGrid.RequestedTheme = ElementTheme.Light;
                UIHelper.RefreshAllButtons(mainWindow.MainGrid);
            }
        }
        else if (newTheme == "Dark")
        {
            AppSettings.Current.CurrentTheme = ElementTheme.Dark;
            if (App.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainGrid.RequestedTheme = ElementTheme.Dark;
                UIHelper.RefreshAllButtons(mainWindow.MainGrid);
            }
        }
        else
        {
            AppSettings.Current.CurrentTheme = ElementTheme.Light;
            if (App.MainWindow is MainWindow mainWindow)
            {
                mainWindow.MainGrid.RequestedTheme = ElementTheme.Light;
                UIHelper.RefreshAllButtons(mainWindow.MainGrid);
            }
        }
    }

    /// <summary>
    /// Обробка події кнопки "Чат".
    /// </summary>
    private void ContentTitle_Click(object sender, RoutedEventArgs e)
    {
        if (ContentFrame.Content is ChatPage) return;

        ContentFrame.Navigate(typeof(ChatPage));
    }

    
}
