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
        public static List<string> Bedrijven;
        //Connection string.
        private static string LoadConnectionString(string id = "UmtDb")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //Load all employees that work on the selected calendar date.
        public static dynamic LoadEmployeesOnDate(string datum, string year)
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
                                    var outputBilthovenAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Bilthoven left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}' AND WerkDagen.Jaar = '{year}'", new DynamicParameters());
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
                                    var outputAlmereAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Almere left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}' AND WerkDagen.Jaar = '{year}'", new DynamicParameters());
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
                                    var outputLelystadAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Lelystad left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}' AND WerkDagen.Jaar = '{year}'", new DynamicParameters());
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
        public static dynamic LoadEmployeesOnWeek(string FirstDay, string LastDay,  string year)
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
                                    var outputBilthovenBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Functie.Bezorger AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Functie.Depot_personeel AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Functie.Sorteer_personeel AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Bilthoven left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Personeel.PersNr AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputBilthovenAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Functie.Bezorger AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Functie.Depot_personeel AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Functie.Sorteer_personeel AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Almere left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Personeel.PersNr AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputAlmereAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Functie.Bezorger AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Functie.Depot_personeel AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Functie.Sorteer_personeel AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Jaar, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Lelystad left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Personeel.PersNr AND WerkDagen.Datum between '{FirstDay}' AND '{LastDay}' AND WerkDagen.Jaar = '{year}' order by WerkDagen.Datum ASC", new DynamicParameters());
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

        //LoadAllEmployeesNames from db.
        public static List<string> LoadAllEmployeesOnName(string Bedrijf)
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
                                    var outputBilthovenBedrijven = cnn.Query<string>($"select Naam from Bedrijven", new DynamicParameters());
                                    Bedrijven = new List<string>();
                                    foreach (var BedrijfsNaam in outputBilthovenBedrijven.ToList())
                                    {
                                        Bedrijven.Add(BedrijfsNaam);
                                    }
                                    if (Bedrijf != String.Empty)
                                    {
                                        var outputBilthovenBezorger = cnn.Query<string>($"select Voornaam from Depots left join Functie on Depots.Bilthoven = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Bezorger AND Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputBilthovenBezorger.ToList();
                                    }
                                    else
                                    {
                                        return null;
                                    }
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
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputBilthovenBezorger = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Bilthoven = Functie.Bezorger inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Bezorger AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Bilthoven = Functie.Depot_personeel inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Depot_personeel AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Bilthoven = Functie.Sorteer_personeel inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Depots.Bilthoven = Functie.Sorteer_personeel AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<string>($"select Achternaam from Depots inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Almere = Functie.Bezorger inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Depots.Almere = Functie.Bezorger AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Almere = Functie.Depot_personeel inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Depots.Almere = Functie.Depot_personeel AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Almere = Functie.Sorteer_personeel inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Depots.Almere = Functie.Sorteer_personeel AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<string>($"select Achternaam from Depots inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Lelystad = Functie.Bezorger inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Depots.Lelystad = Functie.Bezorger AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Lelystad = Functie.Depot_personeel inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Depots.Lelystad = Functie.Depot_personeel AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<string>($"select Achternaam from Depots left join Functie on Depots.Lelystad = Functie.Sorteer_personeel inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Depots.Lelystad = Functie.Sorteer_personeel AND Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<string>($"select Achternaam from Depots inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Voornaam = '{Name}'", new DynamicParameters());
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

        //Load employee id from lastname string out of the db.
        public static List<string> LoadEmployeePersNr(string Name, string LastName)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>($"select PersNr from Personeel where Voornaam = '{Name}' AND Achternaam = '{LastName}'", new DynamicParameters());
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

        public static List<Functions> GetFunctieBasedOnPersNr(int PersNr)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            var outputBilthoven = cnn.Query<Functions>($"select Bezorger, Depot_personeel, Sorteer_personeel from Functie inner join Personeel on Functie.Bezorger = Personeel.PersNr left join Depots on Depots.Bilthoven = Personeel.PersNr where Personeel.PersNr = {PersNr} and Depots.Bilthoven = Depots.Bilthoven", new DynamicParameters());
                            return outputBilthoven.ToList();
                        case "Almere":
                            var outputAlmere = cnn.Query<Functions>($"select Bezorger, Depot_personeel, Sorteer_personeel from Functie inner join Personeel on Functie.Bezorger = Personeel.PersNr left join Depots on Depots.Almere = Personeel.PersNr where Personeel.PersNr = {PersNr} and Depots.Almere = Depots.Almere", new DynamicParameters());
                            return outputAlmere.ToList();
                        case "Lelystad":
                            var outputLelystad = cnn.Query<Functions>($"select Bezorger, Depot_personeel, Sorteer_personeel from Functie inner join Personeel on Functie.Bezorger = Personeel.PersNr left join Depots on Depots.Lelystad = Personeel.PersNr where Personeel.PersNr = {PersNr} and Depots.Lelystad = Depots.Lelystad", new DynamicParameters());
                            return outputLelystad.ToList();
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

        public static void SaveNewEmployeeWorkDay(PersonModel employee, AllEmployeesPerDepot allEmployees)
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
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Bilthoven, Bezorger) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "DepotWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Bilthoven, Depot_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "SorteerWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Bilthoven, Sorteer_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "AllEmployees":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Bilthoven, Bezorger, Depot_personeel, Sorteer_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @Bezorger, @Depot_personeel, @Sorteer_personeel)", allEmployees);
                                    return;
                                default:
                                    MessageBox.Show("ERROR, neem contact op met admin.");
                                    return;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Almere, Bezorger) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "DepotWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Almere, Depot_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "SorteerWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Almere, Sorteer_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "AllEmployees":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Almere) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr)", allEmployees);
                                    return;
                                default:
                                    MessageBox.Show("ERROR, neem contact op met admin.");
                                    return;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Lelystad, Bezorger) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "DepotWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Lelystad, Depot_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "SorteerWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Lelystad, Sorteer_personeel) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "AllEmployees":
                                    cnn.Execute("insert into WerkDagen (Datum, Jaar, Begin_tijd, Eind_tijd, PersId, Lelystad) values (@Datum, @Jaar, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr)", allEmployees);
                                    return;
                                default:
                                    MessageBox.Show("ERROR, neem contact op met admin.");
                                    return;
                            }
                        default:
                            MessageBox.Show("ERROR, geen depot geselecteerd");
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (employee == null)
                {
                    MessageBox.Show($"Je hebt {allEmployees.PersNr} al ingepland op {allEmployees.Datum}-{allEmployees.Jaar} {Environment.NewLine}en of begin tijd {allEmployees.Begin_tijd} of eind tijd {allEmployees.Eind_tijd} is hetzelfde");
                }
                else
                {
                    MessageBox.Show($"Je hebt {employee.PersNr} al ingepland op {employee.Datum}-{employee.Jaar} {Environment.NewLine}en of begin tijd {employee.Begin_tijd} of eind tijd {employee.Eind_tijd} is hetzelfde");
                }
                MessageBox.Show($"Admin error report: {Environment.NewLine}{ex.Message}");
            }
        }
    }
}
