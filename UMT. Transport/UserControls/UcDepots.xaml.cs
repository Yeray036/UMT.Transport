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
    /// Interaction logic for UcDepots.xaml
    /// </summary>
    public partial class UcDepots : UserControl
    {
        public static UcDepots ucDepots { get; set; }
        public static string SelectedDepot { get; set; }

        public UcDepots()
        {
            InitializeComponent();
            ucDepots = this;
        }

        private void OpenUcFunctionsBilthoven(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                UcFunctions.ucFunctions.DepotName.Text = "Depot: Bilthoven";
                SelectedDepot = "Bilthoven";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
            }
            else
            {
                UcFunctions.ucFunctions.DepotName.Text = "Depot: Bilthoven";
                SelectedDepot = "Bilthoven";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
            }
        }

        private void OpenUcFunctionsAlmere(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                UcFunctions.ucFunctions.DepotName.Text = "Depot: Almere";
                SelectedDepot = "Almere";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
            }
            else
            {
                UcFunctions.ucFunctions.DepotName.Text = "Depot: Almere";
                SelectedDepot = "Almere";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
            }
        }

        private void OpenUcFunctionsLelystad(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainWindow.MainMenuControls.Children.Count >= 1)
            {
                MainWindow.mainWindow.MainMenuControls.Children.Clear();
                UcFunctions.ucFunctions.DepotName.Text = "Depot: Lelystad";
                SelectedDepot = "Lelystad";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
            }
            else
            {
                UcFunctions.ucFunctions.DepotName.Text = "Depot: Lelystad";
                SelectedDepot = "Lelystad";
                MainWindow.mainWindow.MainMenuControls.Children.Add(UcFunctions.ucFunctions);
            }
        }
    }
}
