﻿#pragma checksum "..\..\..\..\..\Views\Pages\FullMainPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "380A6A1230D43B68E01BF4ED17FAC0D27A817FB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SideNav;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SideNav.Views.Pages {
    
    
    /// <summary>
    /// FullMainPage
    /// </summary>
    public partial class FullMainPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UserName_d;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Pfps;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel MemberGrid;
        
        #line default
        #line hidden
        
        
        #line 126 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel RepoPanel;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame load_frame;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CodeBridge;component/views/pages/fullmainpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 46 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GoBackToMain);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 47 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowRepo);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 57 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenCalendar);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 67 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenManage);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 77 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenMessagePage);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 95 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Image)(target)).ImageFailed += new System.EventHandler<System.Windows.ExceptionRoutedEventArgs>(this.ImageLoadFail);
            
            #line default
            #line hidden
            
            #line 95 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PfpClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.UserName_d = ((System.Windows.Controls.Label)(target));
            
            #line 96 "..\..\..\..\..\Views\Pages\FullMainPage.xaml"
            this.UserName_d.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ProfileClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Pfps = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.MemberGrid = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.RepoPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 12:
            this.load_frame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

