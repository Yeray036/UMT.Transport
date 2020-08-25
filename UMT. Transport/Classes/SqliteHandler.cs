using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UMT.Transport.UserControls;

namespace UMT.Transport.Classes
{
    public class SqliteHandler
    {
        //Connection string.
        private static string LoadConnectionString(string id = "UmtDb")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //Load all employees that work on the selected calendar date.
        public static List<PersonModel> LoadEmployeesOnDate(string datum, string year)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputBilthovenBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Bilthoven = Functie.Bezorger and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Bilthoven = Functie.Depot_personeel and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Bilthoven = Functie.Sorteer_personeel and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Bilthoven left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}' AND WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputBilthovenAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Almere = Functie.Bezorger and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Almere = Functie.Depot_personeel and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Almere = Functie.Sorteer_personeel and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Almere left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}' AND WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputAlmereAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Lelystad = Functie.Bezorger and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Lelystad = Functie.Depot_personeel and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Lelystad = Functie.Sorteer_personeel and WerkDagen.Datum = '{datum}' and WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Jaar from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Lelystad left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}' AND WerkDagen.Jaar = '{year}'", new DynamicParameters());
                                    return outputLelystadAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        default:
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //Load all employees that work on the selected calendar Week.
        public static List<PersonModel> LoadEmployeesOnWeek(string FirstDay, string LastDay,  string year)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<PersonModel>($"select Naam, Achternaam, PersNr, Datum, Begin_tijd, Jaar from WerkDagen inner join Personeel on WerkDagen.PersId = Personeel.PersNr where WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //LoadAllEmployeesNames from db.
        public static List<string> LoadAllEmployeesOnName()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputBilthovenBezorger = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Bilthoven = Functie.Bezorger inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Bezorger", new DynamicParameters());
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Bilthoven = Functie.Depot_personeel inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Depot_personeel", new DynamicParameters());
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Bilthoven = Functie.Sorteer_personeel inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Sorteer_personeel", new DynamicParameters());
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<string>($"select Voornaam from Depots inner join Personeel on Depots.Bilthoven = Personeel.PersNr", new DynamicParameters());
                                    return outputBilthovenAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Almere = Functie.Bezorger inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Depots.Almere = Functie.Bezorger", new DynamicParameters());
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Almere = Functie.Depot_personeel inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Depots.Almere = Functie.Depot_personeel", new DynamicParameters());
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Almere = Functie.Sorteer_personeel inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Depots.Almere = Functie.Sorteer_personeel", new DynamicParameters());
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<string>($"select Voornaam from Depots inner join Personeel on Depots.Almere = Personeel.PersNr", new DynamicParameters());
                                    return outputAlmereAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Lelystad = Functie.Bezorger inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Depots.Lelystad = Functie.Bezorger", new DynamicParameters());
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Lelystad = Functie.Depot_personeel inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Depots.Lelystad = Functie.Depot_personeel", new DynamicParameters());
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Lelystad = Functie.Sorteer_personeel inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Depots.Lelystad = Functie.Sorteer_personeel", new DynamicParameters());
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<string>($"select Voornaam from Depots inner join Personeel on Depots.Lelystad = Personeel.PersNr", new DynamicParameters());
                                    return outputLelystadAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        default:
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //Load Employee(s) Lastname using the firstname from db.
        public static List<string> LoadAllEmployeesOnLastName(string Name)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>($"select Achternaam from Personeel where Naam = '{Name}'", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //Load employee id from lastname string out of the db.
        public static List<string> LoadEmployeePersNr(string LastName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>($"select PersNr from Personeel where Achternaam = '{LastName}'", new DynamicParameters());
                    Console.WriteLine(output.ToList());
                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        public static void SaveNewEmployeeWorkDay(PersonModel employee)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, PersId) values (@Datum, @Jaar, @Begin_tijd, @PersNr)", employee);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Je hebt {employee.PersNr} al om {employee.Begin_tijd} ingepland op {employee.Datum}-{employee.Jaar}");
            }
        }
    }
}
