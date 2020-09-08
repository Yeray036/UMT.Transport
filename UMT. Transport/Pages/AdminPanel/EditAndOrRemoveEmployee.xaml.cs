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
    /// </summary>
    public partial class EditAndOrRemoveEmployee : Page
    {
        public static bool bezorger;
        public static bool depotpersoneel;
        public static bool sorteerpersoneel;
        public static bool Bilthoven;
        public static bool Almere;
        public static bool Lelystad;

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

        }

        private void RemoveEmployee_Btn(object sender, RoutedEventArgs e)
        {

        }

        private void FillNameComboBox(object sender, SelectionChangedEventArgs e)
        {
            NameComboBox.SelectedItem = null;
            LastNameFieldComboBox.ItemsSource = null;
            PersNrTextbox.Text = "";
            if (BezorgerCheckbox.IsChecked == true)
            {
                BezorgerCheckbox.IsChecked = false;
            }
            if (DepotCheckbox.IsChecked == true)
            {
                DepotCheckbox.IsChecked = false;
            }
            if (SorteerCheckbox.IsChecked == true)
            {
                SorteerCheckbox.IsChecked = false;
            }
            if (Bilthoven == true)
            {
                BilthovenCheckbox.IsChecked = false;
            }
            if (Almere == true)
            {
                AlmereCheckbox.IsChecked = false;
            }
            if (Lelystad == true)
            {
                LelystadCheckbox.IsChecked = false;
            }
            List<string> names = new List<string>();
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

        private void PlaceLastNameBasedOnName(object sender, SelectionChangedEventArgs e)
        {
            if (NameComboBox.SelectedItem != null)
            {
                if (BezorgerCheckbox.IsChecked == true)
                {
                    BezorgerCheckbox.IsChecked = false;
                }
                if (DepotCheckbox.IsChecked == true)
                {
                    DepotCheckbox.IsChecked = false;
                }
                if (SorteerCheckbox.IsChecked == true)
                {
                    SorteerCheckbox.IsChecked = false;
                }
                if (Bilthoven == true)
                {
                    BilthovenCheckbox.IsChecked = false;
                }
                if (Almere == true)
                {
                    AlmereCheckbox.IsChecked = false;
                }
                if (Lelystad == true)
                {
                    LelystadCheckbox.IsChecked = false;
                }
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
                if (BezorgerCheckbox.IsChecked == true)
                {
                    BezorgerCheckbox.IsChecked = false;
                }
                if (DepotCheckbox.IsChecked == true)
                {
                    DepotCheckbox.IsChecked = false;
                }
                if (SorteerCheckbox.IsChecked == true)
                {
                    SorteerCheckbox.IsChecked = false;
                }
                if (Bilthoven == true)
                {
                    BilthovenCheckbox.IsChecked = false;
                }
                if (Almere == true)
                {
                    AlmereCheckbox.IsChecked = false;
                }
                if (Lelystad == true)
                {
                    LelystadCheckbox.IsChecked = false;
                }
                var PersNr = SqliteHandler.LoadEmployeePersNr(NameComboBox.SelectedItem.ToString(), LastNameFieldComboBox.SelectedValue.ToString());
                PersNrTextbox.Text = PersNr[0];
            }
        }

        private void ActivateFunctieAndDepotEdit(object sender, RoutedEventArgs e)
        {
            if (this.PersNrTextbox.Text != String.Empty)
            {
                int persNr = int.Parse(this.PersNrTextbox.Text);
                List<Functions> employeeFunctions = SqliteHandler.GetFunctieBasedOnPersNr(persNr);
                if (employeeFunctions != null)
                {
                    if (employeeFunctions[0].Bezorger != 0)
                    {
                        BezorgerCheckbox.IsChecked = true;
                    }
                    if (employeeFunctions[0].Depot_personeel != 0)
                    {
                        DepotCheckbox.IsChecked = true;
                    }
                    if (employeeFunctions[0].Sorteer_personeel != 0)
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
    }
}
