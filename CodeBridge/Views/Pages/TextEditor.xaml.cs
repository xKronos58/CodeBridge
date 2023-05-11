using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SideNav.Views.Pages;

public partial class TextEditor : Page
{
    public TextEditor()
    {
        InitializeComponent();
    }

    public void LoadText(List<Dictionary<string, string[]>> repoFiles, string sender, string? sender2)
    {
        var rFile = new string[] { "", "", "", "" };
        foreach (var list in repoFiles.Where(list => list.FirstOrDefault().Key == sender))
        foreach (var (key, value) in list)
            if (key == sender2)
            {
                rFile[0] = key;
                rFile[1] = value[0];
                rFile[2] = value[1];
                rFile[3] = value[2];
                var lines = File.ReadAllLines(rFile[1]);
                foreach (var line in lines)
                {
                    TextField.Document.Blocks.Clear();
                    TextField.Document.Blocks.Add(new Paragraph(new Run(line)));
                }
                break;
            }
        
        
    }
}