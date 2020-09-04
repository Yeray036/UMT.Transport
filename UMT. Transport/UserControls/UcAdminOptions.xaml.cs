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
using UMT.Transport.Pages.AdminPanel;

namespace UMT.Transport.UserControls
{
    /// <summary>
    /// Interaction logic for UcAdminOptions.xaml
    /// </summary>
    public partial class UcAdminOptions : UserControl
    {

        public static UcAdminOptions ucAdminOptions = new UcAdminOptions();

        public UcAdminOptions()
        {
            InitializeComponent();
            this.DepotName.Text = $"Depot: {UcDepots.SelectedDepot}";
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

        private void OpenAddNewEmployee_Btn(object sender, RoutedEventArgs e)
        {
            AddNewEmployee newEmployeePage = new AddNewEmployee();
            if (MainWindow.mainWindow.MainFrame.CanGoBack)
            {
                MainWindow.mainWindow.MainFrame.RemoveBackEntry();
                MainWindow.mainWindow.MainFrame.Navigate(newEmployeePage);
            }
            else
            {
                MainWindow.mainWindow.MainFrame.Navigate(newEmployeePage);
            }
        }
    }
}
