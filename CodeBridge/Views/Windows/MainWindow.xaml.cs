using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using Microsoft.Win32;
using SideNav.Views.Pages;

namespace SideNav.Views.Windows;

public partial class MainWindow : Window
{
    private static bool isLoggedIn = false;
    public static string user = string.Empty;
    public MainWindow()
    {
        InitializeComponent();
        if (isLoggedIn)
            _LoginScreen();
    }
    
    public static MainWindow mainWindow;
    
    //Login screen method
    private void _LoginScreen()
        => this.Content = mainWindow;

    private void SignIn(object sender, RoutedEventArgs e)
    {
        user = StringFromRichTextBox(Username);
        if (StringFromRichTextBox(Username).ToLower() == "admin" || Password.Password == "123")
            this.Content = new FullMainPage();
        else
            MessageBox.Show("Incorrect username or password.");
    }
    
    string StringFromRichTextBox(RichTextBox rtb)
    {
        TextRange textRange = new TextRange(
            rtb.Document.ContentStart,
            rtb.Document.ContentEnd
        );
        return textRange.Text;
    }

}