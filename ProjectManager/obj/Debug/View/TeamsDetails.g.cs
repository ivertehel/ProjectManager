﻿#pragma checksum "..\..\..\View\TeamsDetails.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA705CCAB45AD4DD8044E91B6737F0E9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PMDataLayer;
using PMView;
using PMView.View.WrapperVM;
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


namespace PMView {
    
    
    /// <summary>
    /// TeamsDetails
    /// </summary>
    public partial class TeamsDetails : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 154 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RetrieveButton;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox EmployeesListBox;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox SkillsListBox;
        
        #line default
        #line hidden
        
        
        #line 222 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PositionsGrid;
        
        #line default
        #line hidden
        
        
        #line 247 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PositionListBox;
        
        #line default
        #line hidden
        
        
        #line 258 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PositionToAddListBox;
        
        #line default
        #line hidden
        
        
        #line 281 "..\..\..\View\TeamsDetails.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid OrdersDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/ProjectManager;component/view/teamsdetails.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\TeamsDetails.xaml"
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
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 154 "..\..\..\View\TeamsDetails.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RetrieveButton = ((System.Windows.Controls.Button)(target));
            
            #line 163 "..\..\..\View\TeamsDetails.xaml"
            this.RetrieveButton.Click += new System.Windows.RoutedEventHandler(this.RetrieveButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.EmployeesListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 178 "..\..\..\View\TeamsDetails.xaml"
            this.EmployeesListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.EmployeesListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SkillsListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.PositionsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.PositionListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.PositionToAddListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.OrdersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 283 "..\..\..\View\TeamsDetails.xaml"
            this.OrdersDataGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.DataGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

