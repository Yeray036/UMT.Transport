using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// SOFTWARE CREATED BY Yeray Guzmán Padrón.
    /// GITHUB: https://github.com/yeray036
    /// </summary>
    public partial class MainWindow : Window
    {
        UcDepots ucDepots = new UcDepots();
        public static MainWindow mainWindow { get; set; }

        public static WebBrowser webBrowser;
        public static Uri svUri;
        private string expireDate = "31-12-2020";
        DateTime currentDate;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            mainWindow.MainMenuControls.Children.Add(ucDepots);
            svUri = new Uri("https://www.svdepot.nl/");
            webBrowser = new WebBrowser();
            webBrowser.Navigate(svUri);
            currentDate = DateTime.Now;
            if (currentDate.ToShortDateString() == expireDate)
            {
                MessageBox.Show("Sessie is verlopen neem contact op met de leverancier.");
                this.Close();
            }
        }

        private void OpenSvDepotSite(object sender, RoutedEventArgs e)
        {
            if (mainWindow.MainFrame.CanGoBack)
            {
                mainWindow.MainFrame.RemoveBackEntry();
                
                    mainWindow.MainFrame.Navigate(webBrowser);
            }
        }
    }
}
