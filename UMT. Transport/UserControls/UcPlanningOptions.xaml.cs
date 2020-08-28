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
using UMT.Transport.Pages;

namespace UMT.Transport.UserControls
{
    /// <summary>
    /// Interaction logic for UcPlanningOptions.xaml
    /// </summary>
    public partial class UcPlanningOptions : UserControl
    {
        public static UcPlanningOptions ucPlanningOptions = new UcPlanningOptions();

        public UcPlanningOptions()
        {
            InitializeComponent();
            DepotName.Text = UcFunctions.ucFunctions.DepotName.Text;

        }

        private void Back_Btn(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
                MainWindow.mainWindow.MainFrame.Navigate(null);
            }
            else
            {
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
                MainWindow.mainWindow.MainFrame.Navigate(null);
            }
        }

        private void OpenNewWeekPlanning(object sender, RoutedEventArgs e)
        {
            NewWeekPlanning newWeekPage = new NewWeekPlanning();
            if (MainWindow.mainWindow.MainFrame.CanGoBack)
            {
                MainWindow.mainWindow.MainFrame.RemoveBackEntry();
                MainWindow.mainWindow.MainFrame.Navigate(newWeekPage);
            }
            else
            {
                MainWindow.mainWindow.MainFrame.Navigate(newWeekPage);
            }
        }

        private void OpenMonthlyOverviewPage(object sender, RoutedEventArgs e)
        {
            MonthlyOverview monthlyOverview = new MonthlyOverview();
            if (MainWindow.mainWindow.MainFrame.CanGoBack)
            {
                MainWindow.mainWindow.MainFrame.RemoveBackEntry();
                MainWindow.mainWindow.MainFrame.Navigate(monthlyOverview);
            }
            else
            {
                MainWindow.mainWindow.MainFrame.Navigate(monthlyOverview);
            }
        }
    }
}
