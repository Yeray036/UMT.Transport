using CefSharp.Wpf;
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
    /// </summary>
    public partial class MainWindow : Window
    {
        UcDepots ucDepots = new UcDepots();
        public static MainWindow mainWindow { get; set; }

        public static ChromiumWebBrowser svWebBrowser;
        public static WebBrowser webBrowser;
        public static Uri svUri;

        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            mainWindow.MainMenuControls.Children.Add(ucDepots);
            svUri = new Uri("https://www.svdepot.nl/");
            svWebBrowser = new ChromiumWebBrowser("https://www.svdepot.nl/");
            webBrowser = new WebBrowser();
            webBrowser.Navigate(svUri);
        }

        private void OpenSvDepotSite(object sender, RoutedEventArgs e)
        {
            if (mainWindow.MainFrame.CanGoBack)
            {
                mainWindow.MainFrame.RemoveBackEntry();
                if (Environment.Is64BitProcess)
                {
                    mainWindow.MainFrame.Navigate(svWebBrowser);
                }
                else
                {
                    mainWindow.MainFrame.Navigate(webBrowser);
                }
            }
            else
            {
                if (Environment.Is64BitProcess)
                {
                    mainWindow.MainFrame.Navigate(svWebBrowser);
                }
                else
                {
                    mainWindow.MainFrame.Navigate(webBrowser);
                }
            }
        }
    }
}
