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
using static System.Windows.Media.Colors;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace SideNav.Views.Pages;

public partial class FullMainPage : Page
{
    public Dictionary<string, string> Members = new Dictionary<string, string>();
    public Dictionary<string, string> Repos = new Dictionary<string, string>();

    public FullMainPage()
    {
        InitializeComponent();
        LoadMembers();
        LoadRepos();
    }

    #region MainPageElementLoading

    private void LoadRepos()
    {
        if (Repos.Count <= 0)
            DefaultRepos();

        foreach (var repo in Repos.Values)
        {
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
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(315) }); // First column with fixed width
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) }); // Second column with remaining space

            // Create the rectangles
            Rectangle leftRect = new Rectangle
            {
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

            Label repoName = new Label
            {
                Content = repo,
                FontSize = 20,
                FontWeight = FontWeights.SemiBold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 40, 0, 0)
            };

            rightRect.MouseLeftButtonDown += LoadRepo;
            leftRect.MouseLeftButtonDown += LoadRepo;
            
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

    private void LoadMembers()
    {
        if (Members.Count <= 0)
            DefaultMembers();
        
        List<Label> memberNames = new List<Label>();

        foreach (var member in Members.Values)
        {
            Label label = new Label
            {
                Content = member,
                Margin = new Thickness(15, 30, 0, 0),
                Width = 182,
                Height = 35,
                Foreground = Brushes.White,
                Background = new SolidColorBrush(Color.FromRgb(20, 18, 39)),
                FontSize = 14
            };

            // Create the style object
            Style labelStyle = new Style(typeof(Label));
            ControlTemplate template = new ControlTemplate(typeof(Label));
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Control.BackgroundProperty));
            borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Control.BorderBrushProperty));
            borderFactory.SetValue(Border.BorderThicknessProperty,
                new TemplateBindingExtension(Control.BorderThicknessProperty));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(15));
            FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenterFactory.SetValue(ContentPresenter.ContentProperty,
                new TemplateBindingExtension(ContentControl.ContentProperty));
            contentPresenterFactory.SetValue(ContentPresenter.MarginProperty,
                new Thickness(8, 5, 0, 0)); // Add left padding
            borderFactory.AppendChild(contentPresenterFactory);
            template.VisualTree = borderFactory;
            labelStyle.Setters.Add(new Setter(Control.TemplateProperty, template));
            labelStyle.Setters.Add(new Setter(Label.EffectProperty, new DropShadowEffect()
            {
                Color = Color.FromRgb(20, 18, 39),
                ShadowDepth = 2,
                Opacity = 0.8,
                BlurRadius = 20,
                Direction = 270
            }));

            // Apply the style to the label control
            label.Style = labelStyle;
            memberNames.Add(label);
            
            //Adds user profile pictures.
            var rand = new Random();
            Image pfp = new Image();
            pfp.Width = 31;
            pfp.Height = 31;
            pfp.Margin = new Thickness(10, 33, 0, 0);
            pfp.Source = new BitmapImage(new Uri(rand.Next(2) switch
            {
                2 => @"file:///W:/SideNav/CodeBridge/img/defaultPfp.png",
                1 => @"file:///W:/SideNav/CodeBridge/img/bluePfp.png",
                0 => @"file:///W:/SideNav/CodeBridge/img/yellowPfp.png",
                _ => @""
            }, UriKind.RelativeOrAbsolute));
            Pfps.Children.Add(pfp);
        }

        // Add all labels to the MemberGrid.Children collection at once
        foreach (var label in memberNames)
        {
            MemberGrid.Children.Add(label);
        }
    }
    #endregion

    #region repoLoading
    
    private void LoadRepo(object sender, MouseButtonEventArgs e)
    {
        load_frame.Content = new RepoPage();
        load_frame.BringIntoView();
    }

    #endregion

    
    private void ImageLoadFail(object? sender, ExceptionRoutedEventArgs e)
    {
        MessageBox.Show("ProfilePicture failed to load.");
    }

    #region defaults

    private void DefaultMembers()
    {
        Members.Add("a000001a", "Mike");
        Members.Add("a000001b", "John");
        Members.Add("a000001c", "Luke");
        Members.Add("a000001d", "Josh");
        Members.Add("a000001e", "Mark");
        Members.Add("a000001f", "Paul");
        Members.Add("a000001g", "Peter");
        Members.Add("a000001h", "James");
        Members.Add("a000001i", "Jude");
        Members.Add("a000001j", "Matthew");
        Members.Add("a000001k", "Thomas");
        Members.Add("a000001l", "Andrew");
        Members.Add("a000001m", "Simon");
    }
    
    private void DefaultRepos()
    {
        Repos.Add("a000001a", "Code Bridge");
        Repos.Add("a000001b", "Ms Office");
        Repos.Add("a000001c", "IOS source");
        Repos.Add("a000001d", "Android source");
        Repos.Add("a000001e", "Windows source");
        Repos.Add("a000001f", "Linux source");
    }
    #endregion

    private void GoBackToMain(object sender, RoutedEventArgs e)
    {
        load_frame.Content = null;
    }
}