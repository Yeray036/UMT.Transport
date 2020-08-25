using System;
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

namespace UMT.Transport.UserControls
{
    /// <summary>
    /// Interaction logic for UcFunctions.xaml
    /// </summary>
    public partial class UcFunctions : UserControl
    {
        public static UcFunctions ucFunctions = new UcFunctions();
        public static string SelectedFunction { get; set; }
        public UcFunctions()
        {
            InitializeComponent();
        }

        private void Back_Btn(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcDepots.ucDepots);
            }
            else
            {
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcDepots.ucDepots);
            }
        }

        private void OpenBezorgerPlanning(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                SelectedFunction = "Bezorger";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
            }
            else
            {
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
                SelectedFunction = "Bezorger";
            }
        }

        private void OpenDepotPlanning(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                SelectedFunction = "DepotWerk";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
            }
            else
            {
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
                SelectedFunction = "DepotWerk";
            }
        }

        private void OpenSorteerPlanning(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                SelectedFunction = "SorteerWerk";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
            }
            else
            {
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
                SelectedFunction = "SorteerWerk";
            }
        }

        private void OpenPlanningForAll(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                SelectedFunction = "AllEmployees";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
            }
            else
            {
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcPlanningOptions.ucPlanningOptions);
                SelectedFunction = "AllEmployees";
            }
        }
    }
}
