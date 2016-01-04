﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PMView.View
{
    /// <summary>
    /// Interaction logic for EmptyWindow.xaml
    /// </summary>
    public partial class EmptyWindow : Window
    {
        public EmptyWindow(string title)
        {
            InitializeComponent();
            this.Title = title;
        }
    }
}