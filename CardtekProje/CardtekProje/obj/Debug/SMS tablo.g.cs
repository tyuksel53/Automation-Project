﻿#pragma checksum "..\..\SMS tablo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F79FA911AD556B698A6CB75A99B562E5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CardtekProje;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace CardtekProje {
    
    
    /// <summary>
    /// Window3
    /// </summary>
    public partial class Window3 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\SMS tablo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\SMS tablo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Goster;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SMS tablo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Taksitle;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\SMS tablo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Temizle;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\SMS tablo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\SMS tablo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Geri_Al;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CardtekProje;component/sms%20tablo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SMS tablo.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\SMS tablo.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.sms_click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 13 "..\..\SMS tablo.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Kampanya_click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 14 "..\..\SMS tablo.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Islem_click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 16 "..\..\SMS tablo.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.istatislik_click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\SMS tablo.xaml"
            this.dataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.degisti);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Goster = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\SMS tablo.xaml"
            this.Goster.Click += new System.Windows.RoutedEventHandler(this.Goster_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Taksitle = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\SMS tablo.xaml"
            this.Taksitle.Click += new System.Windows.RoutedEventHandler(this.Taksitle_click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Temizle = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\SMS tablo.xaml"
            this.Temizle.Click += new System.Windows.RoutedEventHandler(this.Temizle_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.textBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.Geri_Al = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\SMS tablo.xaml"
            this.Geri_Al.Click += new System.Windows.RoutedEventHandler(this.GeriAl_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

