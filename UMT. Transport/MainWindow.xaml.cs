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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UMT.Transport.Pages;
using UMT.Transport.UserControls;

namespace UMT.Transport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UcDepots ucDepots = new UcDepots();
        public static MainWindow mainWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            mainWindow.MainMenuControls.Children.Add(ucDepots);
        }
    }
}
