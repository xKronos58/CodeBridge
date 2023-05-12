using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SideNav.Views.Pages;

public partial class ViewUserProfile : Page
{
    private string _sender = string.Empty;
    private string currentProfile = string.Empty;
    public ViewUserProfile(Label? sender)
    {
        InitializeComponent();

        _sender = sender.Content.ToString();
        var user = sender?.Content.ToString();
        if (user != null) LoadProfile(user);
        LoadRepos();
    }
    Random rand = new Random();
    
    private void LoadProfile(string user)
    {
        if(currentProfile == user) return;
        currentProfile = user;
        
        DisplayName.Content = user;
        Email.Content = user + "@codeBridge.com";
        int stat = rand.Next(4);

        StatusDot.Fill = new SolidColorBrush(stat switch
        {
            1 => Colors.Lime,
            2 => Colors.Yellow,
            3 => Colors.Red,
            _ => Colors.Gray
        });
        
        StatusText.Content = stat switch
        {
            1 => "Online",
            2 => "Away",
            3 => "Busy",
            _ => "Offline"
        };
    }
    
    private void LoadRepos()
    {
        if (FullMainPage.Repos.Count <= 0)
            FullMainPage.DefaultRepos();

        int i = 0;
        foreach (var repo in FullMainPage.Repos.Values)
        {
            if(i > 4)
                break;
            i++;
            Random rand = new Random();
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0);
            gradient.EndPoint = new Point(1, 1);
            gradient.GradientStops.Add(new GradientStop(rand.Next(3) switch
            {
                1 => Color.FromRgb(113, 146, 209),
                2 => Color.FromRgb(134, 216, 196),
                _ => Color.FromRgb(216, 134, 134),
            }, 0.0));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(195, 212, 244), 1.0));
            
            // Create the grid
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(186) }); // First column with fixed width
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // Second column with remaining space

            // Create the rectangles
            Rectangle leftRect = new Rectangle
            {
                Name = repo,
                Width = 173,
                Height = 93, 
                Fill = gradient,
                RadiusX = 20,
                RadiusY = 20,
                Margin = new Thickness(10, 10, 0, 0),
                Effect = new DropShadowEffect
                {
                    Color = Colors.Black,
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Opacity = 0.5
                }
            };

            Rectangle rightRect = new Rectangle
            {
                Width = double.NaN,
                Height = 93, 
                Fill = new SolidColorBrush(Color.FromRgb(20, 18, 39)),
                RadiusX = 20,
                RadiusY = 20,
                Name = repo,
                Effect = new DropShadowEffect
                {
                    Color = Colors.Black,
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Opacity = 0.5
                },
                Margin = new Thickness(0, 10, 0, 0)
            };

            Label codeSymb = new Label
            {
                Content = "</>",
                FontSize = 26,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Black,
                Margin = new Thickness(0, 40, 0, 0)
            };

            Label repoName = new Label
            {
                Content = repo,
                FontSize = 15,
                FontWeight = FontWeights.Medium,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 30, 0, 0)
            };

            FullMainPage fmp = new FullMainPage();
            
            rightRect.MouseLeftButtonDown += fmp.LoadRepo;
            leftRect.MouseLeftButtonDown += fmp.LoadRepo;
            
            // Add the rectangles to the grid
            Grid.SetColumn(leftRect, 0);
            Grid.SetColumn(codeSymb, 0);
            Grid.SetColumn(repoName, 0);
            Grid.SetColumn(rightRect, 1);

            grid.Children.Add(leftRect);
            grid.Children.Add(codeSymb);
            grid.Children.Add(repoName);
            grid.Children.Add(rightRect);

            // Add the grid to the stack panel
            RepoPanel.Children.Add(grid);
        }
    }

    private void NewEmail(object sender, MouseButtonEventArgs e)
    {
        // TODO: Needs fixing
        try
        {
            Outlook.Application objOutlook = new Outlook.Application();
            Outlook.MailItem mic = objOutlook.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

            mic.To = "abc@gmail.com";
            mic.Subject = "This is the subject...";
            mic.Display(false);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }

    private void OpenMessage(object sender, MouseButtonEventArgs e)
        => load_frame.Content = new MessagePage(_sender);
}