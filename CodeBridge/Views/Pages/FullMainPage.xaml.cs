using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace SideNav.Views.Pages;

public partial class FullMainPage : Page
{
    public Dictionary<string, string> Members = new Dictionary<string, string>();

    public FullMainPage()
    {
        InitializeComponent();
        LoadMembers();
    }

    public void LoadMembers()
    {
        if (Members.Count == 0)
            DefaultMembers();
        
        List<Label> memberNames = new List<Label>();
        List<Image> pfps = new List<Image>();

        foreach (var member in Members.Values)
        {
            Label label = new Label();
            label.Content = member;
            label.Margin = new Thickness(52, 30, 0, 0);
            label.Width = 182;
            label.Height = 35;
            label.Foreground = Brushes.White;
            label.Background = new SolidColorBrush(Color.FromRgb(20, 18, 39));
            label.FontSize = 14;

            // Create the style object
            Style labelStyle = new Style(typeof(Label));
            ControlTemplate template = new ControlTemplate(typeof(Label));
            FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Label.BackgroundProperty));
            borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Label.BorderBrushProperty));
            borderFactory.SetValue(Border.BorderThicknessProperty,
                new TemplateBindingExtension(Label.BorderThicknessProperty));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(15));
            FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenterFactory.SetValue(ContentPresenter.ContentProperty,
                new TemplateBindingExtension(Label.ContentProperty));
            contentPresenterFactory.SetValue(ContentPresenter.MarginProperty,
                new Thickness(8, 5, 0, 0)); // Add left padding
            borderFactory.AppendChild(contentPresenterFactory);
            template.VisualTree = borderFactory;
            labelStyle.Setters.Add(new Setter(Label.TemplateProperty, template));
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

            var rand = new Random();

            Image pfp = new Image();
            pfp.Width = 24;
            pfp.Height = 24;
            pfp.Margin = new Thickness(52, 30, 0, 0);
            pfp.Source = new BitmapImage(new Uri(rand.Next(2) switch
            {
                1 => @"file:///W:/SideNav/CodeBridge/img/bluePfp.png",
                2 => @"file:///W:/SideNav/CodeBridge/img/yellowPfp.png",
                _ => @""
            }));
            MemberGrid.Children.Add(pfp);
        }

        // Add all labels to the MemberGrid.Children collection at once
        foreach (var label in memberNames)
        {
            MemberGrid.Children.Add(label);
        }
    }


    private void ImageLoadFail(object? sender, ExceptionRoutedEventArgs e)
    {
        MessageBox.Show("ProfilePicture failed to load.");
    }

    #region defaults

    public void DefaultMembers()
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

    #endregion
}