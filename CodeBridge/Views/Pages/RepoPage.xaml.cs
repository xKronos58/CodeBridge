using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SideNav.Views.Pages;

public partial class RepoPage : Page
{
    private string _currentRepo = string.Empty;
    private List<Dictionary<string, string[]>> _repoFiles = null;
    public RepoPage()
    {
        InitializeComponent();
    }

    public void PopulateFiles(List<Dictionary<string, string[]>> repoFiles, string sender)
    {
        // var rFile = repoFiles.Contains(repoFiles.Find(x => x.ContainsKey(sender))) ? repoFiles.Find(x => x.ContainsKey(sender)) : null;

        _currentRepo = sender;
        _repoFiles = repoFiles;
        var rFile = new Dictionary<string, string[]>();
        foreach (var list in  repoFiles)
            if (list.FirstOrDefault().Key == sender)
            {
                foreach (var (key, value) in list)
                {
                    rFile.Add(key, value);
                }
            }
        int skip1 = 0;
        
        foreach (var file in rFile)
        {
            if(skip1 == 0)
            {
                skip1++;
                continue;
            }
            Rectangle backGround = new Rectangle
            {
                Height = 50,
                Fill = new SolidColorBrush(Color.FromArgb(51,49,52, 49)),
                /*Name = file.Key,*/
                RadiusX = 19,
                RadiusY = 19,
                Margin = new Thickness(0, 10, 0 ,0)
            };

            Grid grid = new Grid
            {
                ColumnDefinitions = { new ColumnDefinition() { Width = new GridLength(605) }, /* new ColumnDefinition() {  } */ }
            };
            
            Image type = new Image
            {
                Source = new BitmapImage(new Uri(file.Value[2] switch
                {
                    "file" => "file:///W:/SideNav/CodeBridge/img/FileIcon.png",
                    "folder" => "file:///W:/SideNav/CodeBridge/img/folderIcon.png",
                    _ => "file:///W:/SideNav/CodeBridge/img/FileIcon.png"
                }, UriKind.Absolute)),
                Stretch = Stretch.None,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = (new Thickness(10, 8, 0, 0))
            };
            
            Label fileName = new Label
            {
                Content = file.Key,
                /*Name = file.Key,*/
                FontSize = 16,
                FontWeight = FontWeights.Medium,
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(60, 15, 0, 0)
            };

            fileName.MouseLeftButtonDown += LoadFile;
            backGround.MouseLeftButtonDown += LoadFile;

            Grid.SetColumn(backGround, 0);
            grid.Children.Add(backGround);
            
            Grid.SetColumn(type, 0);
            grid.Children.Add(type);
            
            Grid.SetColumn(fileName, 0);
            grid.Children.Add(fileName);
            
            FilePanel.Children.Add(grid);
        }
    }

    private void LoadFile(Object sender, EventArgs e)
    {
        TextFrame.Content = new TextEditor();

        
    }
}