using ChatProto.UserControl;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.ApplicationModel.Resources;
using System.Collections.Generic;
using System.Linq;

namespace ChatProto.Pages;

public sealed partial class ChatPage : Page
{

    public ChatPage()
    {
        this.InitializeComponent();

    }

    private void MessageInput_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == Windows.System.VirtualKey.Enter)
        {
            SendMessageInChatButton_Click(sender, e);
        }
    }

    public void SendMessageInChatButton_Click(object sender, RoutedEventArgs e)
    {
        string userMessage = MessageInput.Text;
        if (string.IsNullOrWhiteSpace(userMessage)) return;

        AddMessageToListBox(userMessage, HorizontalAlignment.Right);

        string botMessage = "This is a bot response";
        AddMessageToListBox(botMessage, HorizontalAlignment.Left);
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
}
