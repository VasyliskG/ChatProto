using System;
using ChatProto.UserControl;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;

namespace ChatProto.Pages
{
    public sealed partial class ChatPage : Page
    {
        public ChatPage()
        {
            this.InitializeComponent();
            ShowWelcomeMessage();
        }
        public void ShowWelcomeMessage()
        {
            var resourceLoader = new ResourceLoader();
            string welcomeMessage = resourceLoader.GetString("WelcomeMessage");
            AddMessageToListBox(welcomeMessage, HorizontalAlignment.Left);
        }

        private void MessageInput_KeyDown(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
             if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SendMessageInChatButton_Click(sender, e);
            }
        }
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

        public void ReceiveMessage(string message)
        {
            MessageListBox.Items.Add(message);
        }
    }
}
 