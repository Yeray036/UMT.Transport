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
using UMT.Transport.Classes;
using UMT.Transport.UserControls;

namespace UMT.Transport.Pages.AdminPanel
{
    /// <summary>
    /// Interaction logic for AddNewEmployee.xaml
    /// </summary>
    public partial class AddNewEmployee : Page
    {
        public bool bezorger;
        public bool depotpersoneel;
        public bool sorteerpersoneel;
        public bool Bilthoven;
        public bool Almere;
        public bool Lelystad;

        public AddNewEmployee()
        {
            InitializeComponent();
            if (UcDepots.SelectedDepot != "Bilthoven")
            {
                SqliteHandler.Bedrijven = null;
                CompanyComboBox.Visibility = Visibility.Hidden;
            }
            else
            {
                CompanyComboBox.ItemsSource = SqliteHandler.LoadAllCompanyNames();
            }
            if (UcDepots.SelectedDepot == "Bilthoven")
            {
                BilthovenCheckbox.IsEnabled = false;
                BilthovenCheckbox.Content = "Al geactiveerd";
            }
            if (UcDepots.SelectedDepot == "Almere")
            {
                AlmereCheckbox.IsEnabled = false;
                AlmereCheckbox.Content = "Al geactiveerd";
            }
            if (UcDepots.SelectedDepot == "Lelystad")
            {
                LelystadCheckbox.IsEnabled = false;
                LelystadCheckbox.Content = "Al geactiveerd";
            }
            CurrentEmployeeListDataGrid.ItemsSource = SqliteHandler.SaveNewEmployee(null, null, null);
        }

        private void AddNewEmployee_Btn(object sender, RoutedEventArgs e)
        {
            if (VoornaamTextbox.Text != string.Empty && AchternaamTextbox.Text != string.Empty && PersNrTextbox.Text != string.Empty)
            {
                if (bezorger != false || depotpersoneel != false || sorteerpersoneel != false)
                {
                    PersoneelTabel newEmployee = new PersoneelTabel();
                    FunctieTabel newEmployeeFuncties = new FunctieTabel();
                    DepotTabel newEmployeeDepots = new DepotTabel();
                    if (Bilthoven == true | BilthovenCheckbox.IsEnabled == false)
                    {
                        newEmployeeDepots.BilthovenInput = "1";
                    }
                    if (Almere == true | AlmereCheckbox.IsEnabled == false)
                    {
                        newEmployeeDepots.AlmereInput = "2";
                    }
                    if (Lelystad == true | LelystadCheckbox.IsEnabled == false)
                    {
                        newEmployeeDepots.LelystadInput = "3";
                    }
                    if (UcDepots.SelectedDepot == "Bilthoven")
                    {
                        newEmployee.Bedrijfsnaam = CompanyComboBox.SelectedItem.ToString();
                    }
                    else
                    {
                        newEmployee.Bedrijfsnaam = null;
                    }
                    newEmployee.Voornaam = VoornaamTextbox.Text;
                    newEmployee.Achternaam = AchternaamTextbox.Text;
                    newEmployee.PersNr = int.Parse(PersNrTextbox.Text);
                    newEmployeeDepots.PersNr = int.Parse(PersNrTextbox.Text);
                    newEmployeeFuncties.PersNr = int.Parse(PersNrTextbox.Text);
                    if (bezorger == true)
                    {
                        newEmployeeFuncties.BezorgerInput = "1";
                    }
                    if (depotpersoneel == true)
                    {
                        newEmployeeFuncties.DepotwerkInput = "3";
                    }
                    if (sorteerpersoneel == true)
                    {
                        newEmployeeFuncties.SorteerwerkInput = "2";
                    }
                    SqliteHandler.SaveNewEmployee(newEmployee, newEmployeeFuncties, newEmployeeDepots);
                    CurrentEmployeeListDataGrid.ItemsSource = SqliteHandler.SavedPersonReturnList;
                    if (CompanyComboBox.Text != null)
                    {
                        CompanyComboBox.Text = "";
                    }
                    VoornaamTextbox.Text = null;
                    AchternaamTextbox.Text = null;
                    PersNrTextbox.Text = null;
                    BezorgerCheckbox.IsChecked = false;
                    DepotCheckbox.IsChecked = false;
                    SorteerCheckbox.IsChecked = false;
                    if (BilthovenCheckbox.IsChecked == true)
                    {
                        BilthovenCheckbox.IsChecked = false;
                    }
                    if (AlmereCheckbox.IsChecked == true)
                    {
                        AlmereCheckbox.IsChecked = false;
                    }
                    if (LelystadCheckbox.IsChecked == true)
                    {
                        LelystadCheckbox.IsChecked = false;
                    }
                }
                else
                {
                    MessageBox.Show("Selecteer op ze minst 1 functie");
                }
            }
            else
            {
                MessageBox.Show("Kan geen medewerker toevoegen zonder geldige gegevens");
            }
        }
        private void IsBezorger_Checked(object sender, RoutedEventArgs e)
        {
            bezorger = true;
        }

        private void IsDepotpersoneel_Checked(object sender, RoutedEventArgs e)
        {
            depotpersoneel = true;
        }

        private void IsSorteerpersoneel_Checked(object sender, RoutedEventArgs e)
        {
            sorteerpersoneel = true;
        }

        private void IsBezorger_Unchecked(object sender, RoutedEventArgs e)
        {
            bezorger = false;
        }

        private void IsDepotpersoneel_Unchecked(object sender, RoutedEventArgs e)
        {
            depotpersoneel = false;
        }

        private void IsSorteerpersoneel_Unchecked(object sender, RoutedEventArgs e)
        {
            sorteerpersoneel = false;
        }

        private void IsBilthoven_Checked(object sender, RoutedEventArgs e)
        {
            Bilthoven = true;
        }

        private void IsBilthoven_Unchecked(object sender, RoutedEventArgs e)
        {
            Bilthoven = false;
        }

        private void IsAlmere_Checked(object sender, RoutedEventArgs e)
        {
            Almere = true;
        }

        private void IsAlmere_Unchecked(object sender, RoutedEventArgs e)
        {
            Almere = false;
        }

        private void IsLelystad_Checked(object sender, RoutedEventArgs e)
        {
            Lelystad = true;
        }

        private void IsLelystad_Unchecked(object sender, RoutedEventArgs e)
        {
            Lelystad = false;
        }
    }
}
