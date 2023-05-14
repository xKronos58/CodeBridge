using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SideNav.Views.Windows;
using static System.Windows.Media.Colors;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace SideNav.Views.Pages;

public partial class FullMainPage : Page
{
    public FullMainPage()
    {
        InitializeComponent();
        LoadMembers();
        LoadRepos();
        UserName_d.Content = _user;
    }
    
    #region Variables
    /// <summary>
    /// Format = {UserName, UUID *(SERVER IMPLEMENTATION)*}
    /// </summary>
    private readonly Dictionary<string, string> _members = new();
    /// <summary>
    /// Format = {RepoName, UUID *(SERVER IMPLEMENTATION)*}
    /// </summary>
    public static readonly Dictionary<string, string> Repos = new();
    /// <summary>
    /// Format = {repoName, {repoDesc, Lang, Owner}}
    /// </summary>
    private readonly Dictionary<string, string[]> _repoInfo = new();
    /// <summary>
    /// Format = {repoName, {Ftp path, UUID *(SI)*, fileType}}
    /// </summary>
    private readonly List<Dictionary<string, string[]>> _repoFiles = new();
    
    private string _user = MainWindow.user;
    
    #endregion

    #region MainPageElementLoading

    private void LoadRepos()
    {
        if (Repos.Count <= 0)
            DefaultRepos();

        foreach (var repo in Repos.Values)
        {
            var rand = new Random();
            var gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1)
            };
            gradient.GradientStops.Add(new GradientStop(rand.Next(3) switch
            {
                1 => Color.FromRgb(113, 146, 209),
                2 => Color.FromRgb(134, 216, 196),
                _ => Color.FromRgb(216, 134, 134),
            }, 0.0));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(195, 212, 244), 1.0));
            
            // Create the grid
            var grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(315) }); // First column with fixed width
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // Second column with remaining space

            // Create the rectangles
            Rectangle leftRect = new Rectangle
            {
                Name = repo,
                Width = 295,
                Height = 154, 
                Fill = gradient,
                RadiusX = 30,
                RadiusY = 30,
                Margin = new Thickness(10, 40, 0, 0),
                Effect = new DropShadowEffect
                {
                    Color = Black,
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Opacity = 0.5
                }
            };

            Rectangle rightRect = new Rectangle
            {
                Width = double.NaN,
                Height = 154, 
                Fill = new SolidColorBrush(Color.FromRgb(20, 18, 39)),
                RadiusX = 30,
                RadiusY = 30,
                Name = repo,
                Effect = new DropShadowEffect
                {
                    Color = Black,
                    BlurRadius = 10,
                    ShadowDepth = 5,
                    Opacity = 0.5
                },
                Margin = new Thickness(0, 40, 30, 0)
            };

            Label codeSymb = new Label
            {
                Content = "</>",
                FontSize = 36,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = Brushes.Black,
                Margin = new Thickness(0, 20, 0, 0)
            };

            var repoName = new Label
            {
                Content = repo,
                FontSize = 20,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 40, 0, 0)
            };

            var repoDescS = string.Empty;
            foreach (var repoDes in _repoInfo.Where(repoDes => repoDes.Key == repo))
                repoDescS = repoDes.Value[0];

            var repoDesc = new Label
            {
                Content = repoDescS.Length > 50 ? repoDescS.Substring(0, 50) + "..." : repoDescS,
                FontSize = 14,
                FontWeight = FontWeights.Light,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 0, 0, 0),
                Foreground = new SolidColorBrush(Colors.White)
            };

            rightRect.MouseLeftButtonDown += LoadRepo;
            leftRect.MouseLeftButtonDown += LoadRepo;
            
            // Add the rectangles to the grid
            Grid.SetColumn(leftRect, 0);
            Grid.SetColumn(codeSymb, 0);
            Grid.SetColumn(repoName, 0);
            Grid.SetColumn(rightRect, 1);
            Grid.SetColumn(repoDesc, 1);

            grid.Children.Add(leftRect);
            grid.Children.Add(codeSymb);
            grid.Children.Add(repoName);
            grid.Children.Add(rightRect);
            grid.Children.Add(repoDesc);

            // Add the grid to the stack panel
            RepoPanel.Children.Add(grid);
        }
    }

    private void LoadMembers()
    {
        if (_members.Count <= 0)
            DefaultMembers();
        
        var memberNames = new List<Label>();
        if (memberNames == null) throw new ArgumentNullException(nameof(memberNames));

        foreach (var member in _members.Values)
        {
            var label = new Label
            {
                Content = member,
                Margin = new Thickness(15, 29, 0, 0),
                Width = 182,
                Height = 35,
                Foreground = Brushes.White,
                Background = new SolidColorBrush(Color.FromRgb(20, 18, 39)),
                FontSize = 14
            };

            // Create the style object
            var labelStyle = new Style(typeof(Label));
            var template = new ControlTemplate(typeof(Label));
            var borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Control.BackgroundProperty));
            borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Control.BorderBrushProperty));
            borderFactory.SetValue(Border.BorderThicknessProperty,
                new TemplateBindingExtension(Control.BorderThicknessProperty));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(15));
            var contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenterFactory.SetValue(ContentPresenter.ContentProperty,
                new TemplateBindingExtension(ContentControl.ContentProperty));
            contentPresenterFactory.SetValue(ContentPresenter.MarginProperty,
                new Thickness(8, 5, 0, 0)); // Add left padding
            borderFactory.AppendChild(contentPresenterFactory);
            template.VisualTree = borderFactory;
            labelStyle.Setters.Add(new Setter(Control.TemplateProperty, template));
            labelStyle.Setters.Add(new Setter(Label.EffectProperty, new DropShadowEffect
            {
                Color = Color.FromRgb(20, 18, 39),
                ShadowDepth = 2,
                Opacity = 0.8,
                BlurRadius = 20,
                Direction = 270
            }));
            
            label.MouseLeftButtonDown += LoadMember;
            
            // Apply the style to the label control
            label.Style = labelStyle;
            memberNames.Add(label);
            
            //Adds user profile pictures.
            var rand = new Random();
            var pfp = new Image
            {
                Width = 31,
                Height = 31,
                Margin = new Thickness(10, 33, 0, 0),
                Source = new BitmapImage(new Uri(rand.Next(2) switch
                {
                    2 => @"file:///W:/SideNav/CodeBridge/img/defaultPfp.png",   //Note this is the file struct for my pc, change to your own.
                    1 => @"file:///W:/SideNav/CodeBridge/img/bluePfp.png",
                    0 => @"file:///W:/SideNav/CodeBridge/img/yellowPfp.png",
                    _ => @""
                }, UriKind.RelativeOrAbsolute))
            };
            Pfps.Children.Add(pfp);
            
            // Adds the user status.
            var status = new Rectangle
            {
                Width = 12,
                Height = 12,
                RadiusX = 12,
                RadiusY = 12,
                Fill = rand.Next(3) switch
                {
                    1 => new SolidColorBrush(Lime),
                    2 => new SolidColorBrush(Yellow),
                    3 => new SolidColorBrush(Red),
                    _ => new SolidColorBrush(Gray)
                },
            };

            MemberGrid.Children.Add(label);
        }
    }
    #endregion

    #region repoLoading
    
    public void LoadRepo(object sender, MouseButtonEventArgs e)
    {
        var rpage = new RepoPage();
        load_frame.Content = null;
        load_frame.Content = rpage;
        load_frame.BringIntoView();
        var rect = (Rectangle) sender;
        // MessageBox.Show(rect.Name);
        
        if(_repoFiles.Count <= 0)
            RepoList();
        
        //Load the repo from the server

        rpage.PopulateFiles(_repoFiles, rect.Name);
    }

    #endregion

    #region UserProfeileLoading

    private void LoadMember(object sender, EventArgs e)
        => load_frame.Content = new ViewUserProfile(sender as Label);

    #endregion

    #region defaults

    private void RepoList()
    {
        foreach (var repo in Repos.Values)
        {
            var repoFileLoad = new Dictionary<string, string[]>();
            switch (repo)
            {
                case "Code_Bridge":
                    repoFileLoad.Add("Code_Bridge", new string[] {""});
                    repoFileLoad.Add(".idea/.idea.codeBridge/.Idea", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/.idea/.idea.codeBridge/.Idea", "id:[0a000001aa1a]", "folder"});
                    repoFileLoad.Add("CodeBridge", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/CodeBridge", "id:[0a000001aa1b]", "folder"});
                    repoFileLoad.Add("README.txt", new string[] {$"W:/_CodeBridgeRepo/admin/Code_Bridge/README.txt", "id:[0a000001aa1c]", "file"});
                    repoFileLoad.Add("CodeBridge.sln", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/CodeBridge.sln", "id:[0000001aa1d]", "file"});
                    repoFileLoad.Add("CodeBridge.sln.DotSettings.user", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/CodeBridge.sln.DotSettings.user", "id:[0000001aa1e]", "file"});
                    break;
                case "Ms_Office":
                    repoFileLoad.Add("Ms_Office", new string[] {""});
                    repoFileLoad.Add(".idea/.idea.msOffice/.Idea", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/.idea/.idea.msOffice/.Idea", "id:[0a000001aa1a]", "folder"});
                    repoFileLoad.Add("_Ms_Office", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Ms_Office", "id:[0a000001aa1b]", "folder"});
                    repoFileLoad.Add("README.txt", new string[] {$"W:/_CodeBridgeRepo/admin/Ms_Office/README.txt", "id:[0a000001aa1f]", "file"});
                    repoFileLoad.Add("Ms_Office.sln", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Ms_Office.sln", "id:[0a000001aa1g]", "file"});
                    repoFileLoad.Add("Ms_Office.sln.DotSettings.user", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Ms_Office.sln.DotSettings.user", "id:[0a000001aa1h]", "file"});
                    break;
                case "IOS_source":
                    repoFileLoad.Add("IOS_source", new string[] {""});
                    repoFileLoad.Add(".idea/.idea.iosSource/.Idea", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/.idea/.idea.iosSource/.Idea", "id:[0a000001aa1a]", "folder"});
                    repoFileLoad.Add("_IOS_source", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/IOS_source", "id:[0a000001aa1b]", "folder"});
                    repoFileLoad.Add("README.txt", new string[] {$"W:/_CodeBridgeRepo/admin/IOS_source/README.txt", "id:[0a000001aa1i]", "file"});
                    repoFileLoad.Add("IOS_source.sln", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/IOS_source.sln", "id:[0a000001aa1j]", "file"});
                    repoFileLoad.Add("IOS_source.sln.DotSettings.user", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/IOS_source.sln.DotSettings.user", "id:[0a000001aa1k]", "file"});
                    break;
                case "Android_source":
                    repoFileLoad.Add("Android_source", new string[] {""});
                    repoFileLoad.Add(".idea/.idea.androidSource/.Idea", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/.idea/.idea.androidSource/.Idea", "id:[0a000001aa1a]", "folder"});
                    repoFileLoad.Add("_Android_source", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Android_source", "id:[0a000001aa1b]", "folder"});
                    repoFileLoad.Add("README.txt", new string[] {$"W:/_CodeBridgeRepo/admin/Android_source/README.txt", "id:[0a000001aa1l]", "file"});
                    repoFileLoad.Add("Android_source.sln", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Android_source.sln", "id:[0a000001aa1m]", "file"});
                    repoFileLoad.Add("Android_source.sln.DotSettings.user", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Android_source.sln.DotSettings.user", "id:[0a000001aa1n]", "file"});
                    break;
                case "Windows_source":
                    repoFileLoad.Add("Windows_source", new string[] {""});
                    repoFileLoad.Add(".idea/.idea.windowsSource/.Idea", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/.idea/.idea.windowsSource/.Idea", "id:[0a000001aa1a]", "folder"});
                    repoFileLoad.Add("_Windows_source", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Windows_source", "id:[0a000001aa1b]", "folder"});
                    repoFileLoad.Add("README.txt", new string[] {$"W:/_CodeBridgeRepo/admin/Windows_source/README.txt", "id:[0a000001aa1o]", "file"});
                    repoFileLoad.Add("Windows_source.sln", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Windows_source.sln", "id:[0a000001aa1p]", "file"});
                    repoFileLoad.Add("Windows_source.sln.DotSettings.user", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Windows_source.sln.DotSettings.user", "id:[0a000001aa1q]", "file"});
                    break;
                case "Linux_source":
                    repoFileLoad.Add("Linux_source", new string[] {""});
                    repoFileLoad.Add(".idea/.idea.linuxSource/.Idea", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/.idea/.idea.linuxSource/.Idea", "id:[0a000001aa1a]", "folder"});
                    repoFileLoad.Add("_Linux_source", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Linux_source", "id:[0a000001aa1b]", "folder"});
                    repoFileLoad.Add("README.txt", new string[] {$"W:/_CodeBridgeRepo/admin/Linux_source/README.txt", "id:[0a000001aa1r]", "file"});
                    repoFileLoad.Add("Linux_source.sln", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Linux_source.sln", "id:[0a000001aa1s]", "file"});
                    repoFileLoad.Add("Linux_source.sln.DotSettings.user", new string[] {$"W:/_CodeBridgeRepo/{_user}/{repo}/Linux_source.sln.DotSettings.user", "id:[0a000001aa1t]", "file"});
                    break;
                default:
                    repoFileLoad.Add("NAME:", new string[] {"ERROR"});
                    break;
            }
            
            _repoFiles.Add(repoFileLoad);
        }
    }

    private void DefaultRepoDesc()
    {
        _repoInfo.Add("Code_Bridge", new [] { @"Code bridge is a repository / team management software", "Finley Crowther", "C# (WPF)" });
        _repoInfo.Add("MS_Office", new [] { @"Microsoft Office is a suite of applications", "Bill Gates", "C++" });
        _repoInfo.Add("IOS_source", new [] { @"Operating system for all Iphones", "Tim Cook", "C++ / C / Swift" });
        _repoInfo.Add("Android_source", new [] { @"Base OS for all android phones", "Sundar Pichai", "JAVA / Kotlin" });
        _repoInfo.Add("Windows_source", new [] { @"Windows operating system", "Bill Gates", "C# / C / ASM / C++" });
        _repoInfo.Add("Linux_source", new [] { @"The base linux kernal", "Linus Torvalds", "C" });
    }
    
    private void DefaultMembers()
    {
        _members.Add("a000001a", "Mike");
        _members.Add("a000001b", "John");
        _members.Add("a000001c", "Luke");
        _members.Add("a000001d", "Josh");
        _members.Add("a000001e", "Mark");
        _members.Add("a000001f", "Paul");
        _members.Add("a000001g", "Peter");
        _members.Add("a000001h", "James");
        _members.Add("a000001i", "Jude");
        _members.Add("a000001j", "Matthew");
        _members.Add("a000001k", "Thomas");
        _members.Add("a000001l", "Andrew");
        _members.Add("a000001m", "Simon");
    }
    
    public static void DefaultRepos()
    {
        Repos.Add("a000001a", "Code_Bridge");
        Repos.Add("a000001b", "Ms_Office");
        Repos.Add("a000001c", "IOS_source");
        Repos.Add("a000001d", "Android_source");
        Repos.Add("a000001e", "Windows_source");
        Repos.Add("a000001f", "Linux_source");
    }
    #endregion

    #region Button Functionallity
    private void GoBackToMain(object sender, RoutedEventArgs e)
        => load_frame.Content = null;

    private void PfpClick(object sender, MouseButtonEventArgs e)
        => load_frame.Content = new EditUserProfile();
    
    private void ProfileClick(object sender, MouseButtonEventArgs e)
        => load_frame.Content = new EditUserProfile();

    private void ShowRepo(object sender, RoutedEventArgs e)
        => load_frame.Content = null;

    public void TestResetPage()
        => load_frame.Content = new TextEditor();

    private void OpenMessagePage(object sender, RoutedEventArgs e)
        => load_frame.Content = new MessagePage("");

    private void OpenCalendar(object sender, RoutedEventArgs e)
        => load_frame.Content = new CalendarPage();

    private void OpenManage(object sender, RoutedEventArgs e)
        => load_frame.Content = new ManagePage();
        
    private void ImageLoadFail(object? sender, ExceptionRoutedEventArgs e)
        => MessageBox.Show("ProfilePicture failed to load.");
    #endregion
}