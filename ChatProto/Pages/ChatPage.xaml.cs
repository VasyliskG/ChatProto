using System;
using ChatProto.UserControl;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;

namespace ChatProto.Pages
{
    /// <summary>
    /// Логіка взаємодії ChatPage.xaml.
    /// </summary>
    public sealed partial class ChatPage : Page
    {
        public ChatPage()
        {
            this.InitializeComponent();
            ShowWelcomeMessage();
        }

        /// <summary>
        /// Відображає вітальне повідомлення у списку повідомлень.
        /// </summary>
        public void ShowWelcomeMessage()
        {
            var resourceLoader = new ResourceLoader();
            string welcomeMessage = resourceLoader.GetString("WelcomeMessage");
            AddMessageToListBox(welcomeMessage, HorizontalAlignment.Left);
        }

        /// <summary>
        /// Обробляє подію натискання клавіші Enter у полі введення повідомлення.
        /// </summary>
        private void MessageInput_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
             if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SendMessageInChatButton_Click(sender, e);
            }
        }

        /// <summary>
        /// Обробляє подію натискання кнопки "Відправити" у чаті.
        /// </summary>
        /// <param name="sender">Джерело події, зазвичай елемент, який викликав подію.</param>
        /// <param name="e">Дані події, що містять інформацію про подію.</param>
        public void SendMessageInChatButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = MessageInput.Text;
            if (userMessage == "clear") return;
            if (!string.IsNullOrWhiteSpace(userMessage))
            {
                AddMessageToListBox(userMessage, HorizontalAlignment.Right);

                string botMessage = "This is a bot response";
                AddMessageToListBox(botMessage, HorizontalAlignment.Left);
            }
        }

        /// <summary>
        /// Додає повідомлення до списку повідомлень у чаті.
        /// </summary>
        /// <param name="message">Текст повідомлення.</param>
        /// <param name="alignment">Розташування в ListBox.</param>
        public void AddMessageToListBox(string message, HorizontalAlignment alignment)
        {
            var chatMessageControl = new MessageUserControl(message);
            var listBoxItem = new ListBoxItem
            {
                Content = chatMessageControl,
                HorizontalContentAlignment = alignment
            };
            MessageListBox.Items.Add(listBoxItem);
            MessageListBox.ScrollIntoView(listBoxItem);
        }

        /// <summary>
        /// Отримує повідомлення від бота і додає його до списку повідомлень.
        /// </summary>
        /// <param name="message">Текст повідомлення.</param>
        public void ReceiveMessage(string message)
        {
            MessageListBox.Items.Add(message);
        }
    }
}
