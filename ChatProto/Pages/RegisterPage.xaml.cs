﻿using System.Text.RegularExpressions;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ChatProto.Models;
using Microsoft.Windows.ApplicationModel.Resources;
using ChatProto.Services;

namespace ChatProto.Pages;

public sealed partial class RegisterPage : Page
{
    public RegisterPage()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Обробляє подію натискання кнопки "Зареєструватись".
    /// </summary>
    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    {
        // Очистити попередні повідомлення про помилки
        UsernameErrorTextBlock.Visibility = Visibility.Collapsed;
        EmailErrorTextBlock.Visibility = Visibility.Collapsed;
        FullNameErrorTextBlock.Visibility = Visibility.Collapsed;
        InputPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
        ConfirmPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
        RegisterErrorTextBlock.Visibility = Visibility.Collapsed;

        bool isValid = true;

        var resourceLoader = new ResourceLoader();

        if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
        {
            UsernameErrorTextBlock.Text = resourceLoader.GetString("UsernameEmptyError");
            UsernameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        else if (UsernameTextBox.Text.Length < 3)
        {
            UsernameErrorTextBlock.Text = resourceLoader.GetString("UsernameLengthError");
            UsernameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
        {
            EmailErrorTextBlock.Text = resourceLoader.GetString("EmailEmptyError");
            EmailErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        else if (!IsValidEmail(EmailTextBox.Text))
        {
            EmailErrorTextBlock.Text = resourceLoader.GetString("EmailInvalidError");
            EmailErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
        {
            FullNameErrorTextBlock.Text = resourceLoader.GetString("FullNameEmptyError");
            FullNameErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(InputPasswordBox.Password))
        {
            InputPasswordErrorTextBlock.Text = resourceLoader.GetString("PasswordEmptyError");
            InputPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }
        else if (InputPasswordBox.Password.Length < 6)
        {
            InputPasswordErrorTextBlock.Text = resourceLoader.GetString("PasswordLengthError");
            InputPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (InputPasswordBox.Password != ConfirmPasswordBox.Password)
        {
            ConfirmPasswordErrorTextBlock.Text = resourceLoader.GetString("PasswordsMismatchError");
            ConfirmPasswordErrorTextBlock.Visibility = Visibility.Visible;
            isValid = false;
        }

        if (!isValid)
            return;

        User newUser = new User(
            UsernameTextBox.Text,
            InputPasswordBox.Password,
            EmailTextBox.Text,
            FullNameTextBox.Text
        );

        if (App.AuthService.Register(newUser))
        {
            App.AuthService.SaveUsers();
            App.MainWindowRef.IsUserLoggedIn = true;
           // App.MainWindowRef.ChatHistoriTextBlock.Visibility = Visibility.Collapsed;
           // App.MainWindowRef.ChatHistoriGrid.Visibility = Visibility.Visible;
            Frame.Navigate(typeof(ChatPage));
        }
        else
        {
            RegisterErrorTextBlock.Visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Перевіряє, чи є текст дійсною електронною адресою.
    /// </summary>
    /// <param name="text">Введена користувачем пошта.</param>
    /// <returns>true якщо пошта відповідає вимогам, та false якщо ні.</returns>
    private bool IsValidEmail(string text)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(text, pattern);
    }

    /// <summary>
    /// Обробляє подію натискання кнопки "Переходу на сторінку входу".
    /// </summary>
    private void LoginLink_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(LoginPage));
    }
}
