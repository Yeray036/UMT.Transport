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
    /// Interaction logic for EditAndOrRemoveEmployee.xaml
    /// SOFTWARE CREATED BY Yeray Guzmán Padrón.
    /// GITHUB: https://github.com/yeray036
    /// </summary>
    public partial class EditAndOrRemoveEmployee : Page
    {
        public static bool bezorger;
        public static bool depotpersoneel;
        public static bool sorteerpersoneel;
        public static bool Bilthoven;
        public static bool Almere;
        public static bool Lelystad;
        static string selectedCompany { get; set; }

        public EditAndOrRemoveEmployee()
        {
            InitializeComponent();
            SqliteHandler.LoadAllEmployeesOnName("");
            if (UcDepots.SelectedDepot != "Bilthoven")
            {
                SqliteHandler.Bedrijven = null;
                CompanyComboBox.Visibility = Visibility.Hidden;
                List<string> names = new List<string>();

                foreach (var item in SqliteHandler.LoadAllEmployeesOnName(""))
                {
                    if (names.Contains(item))
                    {
                        continue;
                    }
                    else
                    {
                        names.Add(item);
                    }
                }
                NameComboBox.ItemsSource = names;
            }
            else
            {
                CompanyComboBox.ItemsSource = SqliteHandler.LoadAllCompanyNames();
            }
            if (UcDepots.SelectedDepot == "Bilthoven")
            {
                BilthovenCheckbox.Content = "Bilthoven / Al geactiveerd";
            }
            if (UcDepots.SelectedDepot == "Almere")
            {
                AlmereCheckbox.Content = "Almere / Al geactiveerd";
            }
            if (UcDepots.SelectedDepot == "Lelystad")
            {
                LelystadCheckbox.Content = "Lelystad / Al geactiveerd";
            }
            CurrentEmployeeListDataGrid.ItemsSource = SqliteHandler.SaveNewEmployee(null, null, null);
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

        private void UpdateEmployee_Btn(object sender, RoutedEventArgs e)
        {
            if (NameComboBox.Text != string.Empty && LastNameFieldComboBox.Text != string.Empty && PersNrTextbox.Text != string.Empty)
            {
                if (bezorger != false || depotpersoneel != false || sorteerpersoneel != false)
                {
                    PersoneelTabel newEmployee = new PersoneelTabel();
                    FunctieTabel newEmployeeFuncties = new FunctieTabel();
                    DepotTabel newEmployeeDepots = new DepotTabel();
                    if (Bilthoven == true)
                    {
                        newEmployeeDepots.BilthovenInput = "1";
                    }
                    else
                    {
                        newEmployeeDepots.BilthovenInput = null;
                    }
                    if (Almere == true)
                    {
                        newEmployeeDepots.AlmereInput = "2";
                    }
                    else
                    {
                        newEmployeeDepots.AlmereInput = null;
                    }
                    if (Lelystad == true)
                    {
                        newEmployeeDepots.LelystadInput = "3";
                    }
                    else
                    {
                        newEmployeeDepots.LelystadInput = null;
                    }
                    if (UcDepots.SelectedDepot == "Bilthoven")
                    {
                        newEmployee.Bedrijfsnaam = CompanyComboBox.SelectedItem.ToString();
                    }
                    else
                    {
                        newEmployee.Bedrijfsnaam = null;
                    }
                    newEmployee.Voornaam = NameComboBox.Text;
                    newEmployee.Achternaam = LastNameFieldComboBox.Text;
                    newEmployee.PersNr = int.Parse(PersNrTextbox.Text);
                    newEmployeeDepots.PersNr = int.Parse(PersNrTextbox.Text);
                    newEmployeeFuncties.PersNr = int.Parse(PersNrTextbox.Text);
                    if (bezorger == true)
                    {
                        newEmployeeFuncties.BezorgerInput = "1";
                    }
                    else
                    {
                        newEmployeeFuncties.BezorgerInput = null;
                    }
                    if (depotpersoneel == true)
                    {
                        newEmployeeFuncties.DepotwerkInput = "3";
                    }
                    else
                    {
                        newEmployeeFuncties.DepotwerkInput = null;
                    }
                    if (sorteerpersoneel == true)
                    {
                        newEmployeeFuncties.SorteerwerkInput = "2";
                    }
                    else
                    {
                        newEmployeeFuncties.SorteerwerkInput = null;
                    }
                    SqliteHandler.UpdateEmployee(newEmployee, newEmployeeFuncties, newEmployeeDepots);
                    CurrentEmployeeListDataGrid.ItemsSource = SqliteHandler.SavedPersonReturnList;
                    MessageBox.Show($"{NameComboBox.Text} {LastNameFieldComboBox.Text} is geupdate");
                    NameComboBox.Text = null;
                    NameComboBox.ItemsSource = null;
                    LastNameFieldComboBox.Text = null;
                    PersNrTextbox.Text = null;
                    ResetFunctieAndDepotSelection();
                }
            }
        }

        private void RemoveEmployee_Btn(object sender, RoutedEventArgs e)
        {
            if (PersNrTextbox.Text != null)
            {
                if (MessageBox.Show($"Weet je zeker dat je {NameComboBox.Text} {LastNameFieldComboBox.Text} wilt verwijderen {Environment.NewLine}en al zijn gewerkte dagen?", "Verwijder medewerker", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    selectedCompany = this.CompanyComboBox.SelectedItem.ToString();
                    SqliteHandler.DeleteEmployee(this.PersNrTextbox.Text);
                    CurrentEmployeeListDataGrid.ItemsSource = SqliteHandler.SavedPersonReturnList;
                    ResetFunctieAndDepotSelection();
                    NameComboBox.Text = null;
                    NameComboBox.ItemsSource = null;
                    LastNameFieldComboBox.Text = null;
                    PersNrTextbox.Text = null;
                    CompanyComboBox.Text = null;
                }
                else
                {
                    MessageBox.Show($"{NameComboBox.Text} {LastNameFieldComboBox.Text} verwijderen, is geannuleerd");
                }
            }
        }

        private void FillNameComboBox(object sender, SelectionChangedEventArgs e)
        {
            List<string> names;
            if (CompanyComboBox.Text != null)
            {
                ResetFunctieAndDepotSelection();
                names = new List<string>();
                foreach (var item in SqliteHandler.LoadAllEmployeesOnName(this.CompanyComboBox.SelectedItem.ToString()))
                {
                    if (names.Contains(item))
                    {
                        continue;
                    }
                    else
                    {
                        names.Add(item);
                    }
                }
                NameComboBox.ItemsSource = names;
            }
            else
            {
                names = new List<string>();
                CompanyComboBox.ItemsSource = SqliteHandler.LoadAllCompanyNames();
                foreach (var item in SqliteHandler.LoadAllEmployeesOnName(selectedCompany))
                {
                    if (names.Contains(item))
                    {
                        continue;
                    }
                    else
                    {
                        names.Add(item);
                    }
                }
                NameComboBox.ItemsSource = names;
            }
        }

        private void PlaceLastNameBasedOnName(object sender, SelectionChangedEventArgs e)
        {
            if (NameComboBox.SelectedItem != null)
            {
                ResetFunctieAndDepotSelection();
                LastNameFieldComboBox.SelectedItem = "";
                PersNrTextbox.Text = "";
                var LastName = SqliteHandler.LoadAllEmployeesOnLastName(NameComboBox.SelectedValue.ToString());
                if (LastName.Count > 2)
                {
                    LastNameFieldComboBox.ItemsSource = LastName;
                }
                else
                {
                    LastNameFieldComboBox.ItemsSource = LastName;
                    LastNameFieldComboBox.Text = LastName[0];
                }
            }
            if (NameComboBox.SelectedItem != null && LastNameFieldComboBox.SelectedItem != null)
            {
                var PersNr = SqliteHandler.LoadEmployeePersNr(NameComboBox.SelectedItem.ToString(), LastNameFieldComboBox.SelectedValue.ToString());
                PersNrTextbox.Text = PersNr[0];
            }
        }

        private void PlacePersNrBasedOnNameAndLastname(object sender, SelectionChangedEventArgs e)
        {
            if (NameComboBox.SelectedItem != null && LastNameFieldComboBox.SelectedItem != null)
            {
                ResetFunctieAndDepotSelection();
                var PersNr = SqliteHandler.LoadEmployeePersNr(NameComboBox.SelectedItem.ToString(), LastNameFieldComboBox.SelectedValue.ToString());
                PersNrTextbox.Text = PersNr[0];
            }
        }

        private void ActivateFunctieAndDepotEdit(object sender, RoutedEventArgs e)
        {
            ResetFunctieAndDepotSelection();
            if (this.PersNrTextbox.Text != String.Empty)
            {
                int persNr = int.Parse(this.PersNrTextbox.Text);
                List<Functions> employeeFunctions = SqliteHandler.GetFunctieBasedOnPersNr(persNr);
                if (employeeFunctions != null)
                {
                    if (EmployeeHasFunctie.Bezorger == 1)
                    {
                        BezorgerCheckbox.IsChecked = true;
                    }
                    if (EmployeeHasFunctie.Depotpersoneel == 3)
                    {
                        DepotCheckbox.IsChecked = true;
                    }
                    if (EmployeeHasFunctie.Sorteerpersoneel == 2)
                    {
                        SorteerCheckbox.IsChecked = true;
                    }
                }
                SqliteHandler.GetActiveDepotsFromEmployee(persNr);
                if (Bilthoven == true)
                {
                    BilthovenCheckbox.IsChecked = true;
                }
                if (Almere == true)
                {
                    AlmereCheckbox.IsChecked = true;
                }
                if (Lelystad == true)
                {
                    LelystadCheckbox.IsChecked = true;
                }
            }
            else
            {
                return;
            }
        }

        public void ResetFunctieAndDepotSelection()
        {
            BezorgerCheckbox.IsChecked = false;
            bezorger = false;
            DepotCheckbox.IsChecked = false;
            depotpersoneel = false;
            SorteerCheckbox.IsChecked = false;
            sorteerpersoneel = false;
            BilthovenCheckbox.IsChecked = false;
            Bilthoven = false;
            AlmereCheckbox.IsChecked = false;
            Almere = false;
            LelystadCheckbox.IsChecked = false;
            Lelystad = false;
            EmployeeHasFunctie.Bezorger = 0;
            EmployeeHasFunctie.Depotpersoneel = 0;
            EmployeeHasFunctie.Sorteerpersoneel = 0;
        }
    }
}
