using System;

namespace ChatProto.UserControl;

public sealed partial class MessageUserControl : Microsoft.UI.Xaml.Controls.UserControl
{
    public MessageUserControl(string message)
    {
        this.InitializeComponent();
        MessageTextBlock.Text = message;
        TimeTextBlock.Text = DateTime.Now.ToString("HH:mm");
    }
}
