using System.Windows.Controls;
using System.Windows.Media;

namespace SideNav.Views.Pages;

public partial class MessagePage : Page
{
    public MessagePage(string sender)
    {
        InitializeComponent();
        UserName.Background = new SolidColorBrush(Color.FromArgb(40, 255, 255, 255));
    }
}