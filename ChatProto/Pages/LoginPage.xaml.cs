using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;

namespace ChatProto.Pages
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Обробляє подію натискання кнопки "Увійти".
        /// </summary>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Очистити попередні повідомлення про помилки
            UsernameErrorTextBlock.Visibility = Visibility.Collapsed;
            InputPasswordErrorTextBlock.Visibility = Visibility.Collapsed;
            LoginErrorTextBlock.Visibility = Visibility.Collapsed;

            bool isValid = true;

            var resourceLoader = new ResourceLoader();

            // Перевірка наявності імені користувача
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameErrorTextBlock.Text = resourceLoader.GetString("UsernameEmptyError");
                UsernameErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }

            // Перевірка наявності пароля
            if (string.IsNullOrWhiteSpace(InputPasswordBox.Password))
            {
                InputPasswordErrorTextBlock.Text = resourceLoader.GetString("PasswordEmptyError");
                InputPasswordErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (!isValid)
                return;

            if (App.AuthService.Login(UsernameTextBox.Text, InputPasswordBox.Password))
                Frame.Navigate(typeof(ChatPage));
            else
            {
                Frame.Navigate(typeof(AccountPage));
                App.MainWindowRef.IsUserLoggedIn = true;
               // App.MainWindowRef.ChatHistoriTextBlock.Visibility = Visibility.Collapsed;
               // App.MainWindowRef.ChatHistoriGrid.Visibility = Visibility.Visible;
                //LoginErrorTextBlock.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Обробляє подію натискання кнопки "Переходу на сторінку реєстрації".
        /// </summary>
        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
