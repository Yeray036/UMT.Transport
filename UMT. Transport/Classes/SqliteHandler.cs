using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UMT.Transport.Pages.AdminPanel;
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
        public static dynamic LoadEmployeesOnDate(string datum)
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
                                    var outputBilthovenBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Bilthoven = Functie.Bezorger and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Bilthoven = Functie.Depot_personeel and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Bilthoven = Functie.Sorteer_personeel and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Bilthoven left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        if (outputBilthovenAllEmployees.ToList()[i].Bezorger != null)
                                        {
                                            outputBilthovenAllEmployees.ToList()[i].Bezorger = "✓";
                                        }
                                        if (outputBilthovenAllEmployees.ToList()[i].Depot_personeel != null)
                                        {
                                            outputBilthovenAllEmployees.ToList()[i].Depot_personeel = "✓";
                                        }
                                        if (outputBilthovenAllEmployees.ToList()[i].Sorteer_personeel != null)
                                        {
                                            outputBilthovenAllEmployees.ToList()[i].Sorteer_personeel = "✓";
                                        }
                                    }
                                    return outputBilthovenAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Almere = Functie.Bezorger and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Almere = Functie.Depot_personeel and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Almere = Functie.Sorteer_personeel and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Almere left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        if (outputAlmereAllEmployees.ToList()[i].Bezorger != null)
                                        {
                                            outputAlmereAllEmployees.ToList()[i].Bezorger = "✓";
                                        }
                                        if (outputAlmereAllEmployees.ToList()[i].Depot_personeel != null)
                                        {
                                            outputAlmereAllEmployees.ToList()[i].Depot_personeel = "✓";
                                        }
                                        if (outputAlmereAllEmployees.ToList()[i].Sorteer_personeel != null)
                                        {
                                            outputAlmereAllEmployees.ToList()[i].Sorteer_personeel = "✓";
                                        }
                                    }
                                    return outputAlmereAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Lelystad = Functie.Bezorger and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Lelystad = Functie.Depot_personeel and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where WerkDagen.Lelystad = Functie.Sorteer_personeel and WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Lelystad left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf WHERE WerkDagen.Datum = '{datum}'", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        if (outputLelystadAllEmployees.ToList()[i].Bezorger != null)
                                        {
                                            outputLelystadAllEmployees.ToList()[i].Bezorger = "✓";
                                        }
                                        if (outputLelystadAllEmployees.ToList()[i].Depot_personeel != null)
                                        {
                                            outputLelystadAllEmployees.ToList()[i].Depot_personeel = "✓";
                                        }
                                        if (outputLelystadAllEmployees.ToList()[i].Sorteer_personeel != null)
                                        {
                                            outputLelystadAllEmployees.ToList()[i].Sorteer_personeel = "✓";
                                        }
                                    }
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
        public static dynamic LoadEmployeesOnWeek(string FirstDay, string LastDay, string year)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var Firstdaysplit = FirstDay.Split('-');
                    if (Firstdaysplit[0].Length < 2)
                    {
                        FirstDay = $"0{FirstDay}";
                        Firstdaysplit[0] = $"0{Firstdaysplit[0]}";
                    }
                    var Lastdaysplit = LastDay.Split('-');
                    if (Lastdaysplit[0].Length < 2)
                    {
                        LastDay = $"0{LastDay}";
                        Lastdaysplit[0] = $"0{Lastdaysplit[0]}";
                    }
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputBilthovenBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Functie.Bezorger AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Functie.Depot_personeel AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Functie.Sorteer_personeel AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Bilthoven left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Bilthoven = Personeel.PersNr AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        if (outputBilthovenAllEmployees.ToList()[i].Bezorger != null)
                                        {
                                            outputBilthovenAllEmployees.ToList()[i].Bezorger = "✓";
                                        }
                                        if (outputBilthovenAllEmployees.ToList()[i].Depot_personeel != null)
                                        {
                                            outputBilthovenAllEmployees.ToList()[i].Depot_personeel = "✓";
                                        }
                                        if (outputBilthovenAllEmployees.ToList()[i].Sorteer_personeel != null)
                                        {
                                            outputBilthovenAllEmployees.ToList()[i].Sorteer_personeel = "✓";
                                        }
                                    }
                                    return outputBilthovenAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Functie.Bezorger AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Functie.Depot_personeel AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Functie.Sorteer_personeel AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Almere left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Almere = Personeel.PersNr AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        if (outputAlmereAllEmployees.ToList()[i].Bezorger != null)
                                        {
                                            outputAlmereAllEmployees.ToList()[i].Bezorger = "✓";
                                        }
                                        if (outputAlmereAllEmployees.ToList()[i].Depot_personeel != null)
                                        {
                                            outputAlmereAllEmployees.ToList()[i].Depot_personeel = "✓";
                                        }
                                        if (outputAlmereAllEmployees.ToList()[i].Sorteer_personeel != null)
                                        {
                                            outputAlmereAllEmployees.ToList()[i].Sorteer_personeel = "✓";
                                        }
                                    }
                                    return outputAlmereAllEmployees.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Bezorger = WerkDagen.Bezorger left join Personeel on Personeel.PersNr = Functie.Bezorger left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Functie.Bezorger AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Depot_personeel = WerkDagen.Depot_personeel left join Personeel on Personeel.PersNr = Functie.Depot_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Functie.Depot_personeel AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd from WerkDagen inner join Functie on Functie.Sorteer_personeel = WerkDagen.Sorteer_personeel left join Personeel on Personeel.PersNr = Functie.Sorteer_personeel left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Functie.Sorteer_personeel AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfs_Naam, Voornaam, Achternaam, PersNr, Datum, Begin_tijd, Eind_tijd, Bezorger, Depot_personeel, Sorteer_personeel from WerkDagen inner join Personeel on Personeel.PersNr = WerkDagen.Lelystad left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf  where WerkDagen.Lelystad = Personeel.PersNr AND WerkDagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' order by WerkDagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        if (outputLelystadAllEmployees.ToList()[i].Bezorger != null)
                                        {
                                            outputLelystadAllEmployees.ToList()[i].Bezorger = "✓";
                                        }
                                        if (outputLelystadAllEmployees.ToList()[i].Depot_personeel != null)
                                        {
                                            outputLelystadAllEmployees.ToList()[i].Depot_personeel = "✓";
                                        }
                                        if (outputLelystadAllEmployees.ToList()[i].Sorteer_personeel != null)
                                        {
                                            outputLelystadAllEmployees.ToList()[i].Sorteer_personeel = "✓";
                                        }
                                    }
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

        public static List<string> LoadAllCompanyNames()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var outputBilthovenBedrijven = cnn.Query<string>($"select Naam from Bedrijven", new DynamicParameters());
                    Bedrijven = new List<string>();
                    foreach (var BedrijfsNaam in outputBilthovenBedrijven.ToList())
                    {
                        Bedrijven.Add(BedrijfsNaam);
                    }
                    return Bedrijven;
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
                                case "AdminPanel":
                                    var outputAdminPanelBedrijven = cnn.Query<string>($"select Naam from Bedrijven", new DynamicParameters());
                                    Bedrijven = new List<string>();
                                    foreach (var BedrijfsNaam in outputAdminPanelBedrijven.ToList())
                                    {
                                        Bedrijven.Add(BedrijfsNaam);
                                    }
                                    if (Bedrijf != String.Empty)
                                    {
                                        var outputBilthovenAllEmployeeAdminPanel = cnn.Query<string>($"select Voornaam from Personeel left join Depots on Depots.Bilthoven = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputBilthovenAllEmployeeAdminPanel.ToList();
                                    }
                                    else
                                    {
                                        var outputBilthovenAdminPanel = cnn.Query<string>($"select Voornaam from Depots inner join Personeel on Depots.Bilthoven = Personeel.PersNr", new DynamicParameters());
                                        return outputBilthovenAdminPanel.ToList();
                                    }
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
                                case "AdminPanel":
                                    var outputAdminPanelBedrijven = cnn.Query<string>($"select Naam from Bedrijven", new DynamicParameters());
                                    Bedrijven = new List<string>();
                                    foreach (var BedrijfsNaam in outputAdminPanelBedrijven.ToList())
                                    {
                                        Bedrijven.Add(BedrijfsNaam);
                                    }
                                    if (Bedrijf != String.Empty)
                                    {
                                        var outputAlmereAllEmployeeAdminPanel = cnn.Query<string>($"select Voornaam from Personeel left join Depots on Depots.Almere = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputAlmereAllEmployeeAdminPanel.ToList();
                                    }
                                    else
                                    {
                                        var outputAlmereAdminPanel = cnn.Query<string>($"select Voornaam from Depots inner join Personeel on Depots.Almere = Personeel.PersNr", new DynamicParameters());
                                        return outputAlmereAdminPanel.ToList();
                                    }
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
                                case "AdminPanel":
                                    var outputAdminPanelBedrijven = cnn.Query<string>($"select Naam from Bedrijven", new DynamicParameters());
                                    Bedrijven = new List<string>();
                                    foreach (var BedrijfsNaam in outputAdminPanelBedrijven.ToList())
                                    {
                                        Bedrijven.Add(BedrijfsNaam);
                                    }
                                    if (Bedrijf != String.Empty)
                                    {
                                        var outputLelystadAllEmployeeAdminPanel = cnn.Query<string>($"select Voornaam from Personeel left join Depots on Depots.Lelystad = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputLelystadAllEmployeeAdminPanel.ToList();
                                    }
                                    else
                                    {
                                        var outputLelystadAdminPanel = cnn.Query<string>($"select Voornaam from Depots inner join Personeel on Depots.Lelystad = Personeel.PersNr", new DynamicParameters());
                                        return outputLelystadAdminPanel.ToList();
                                    }
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
                                case "AdminPanel":
                                    var outputBilthovenAdminPanel = cnn.Query<string>($"select Achternaam from Depots inner join Personeel on Depots.Bilthoven = Personeel.PersNr WHERE Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenAdminPanel.ToList();
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
                                case "AdminPanel":
                                    var outputAlmereAdminPanel = cnn.Query<string>($"select Achternaam from Depots inner join Personeel on Depots.Almere = Personeel.PersNr WHERE Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereAdminPanel.ToList();
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
                                case "AdminPanel":
                                    var outputLelystadAdminPanel = cnn.Query<string>($"select Achternaam from Depots inner join Personeel on Depots.Lelystad = Personeel.PersNr WHERE Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadAdminPanel.ToList();
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

        public static void GetActiveDepotsFromEmployee(int PersNr)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            var outputBilthoven = cnn.Query<Depots>($"select Depots.Bilthoven, Depots.Almere, Depots.Lelystad from Depots where Depots.Bilthoven = {PersNr}", new DynamicParameters());
                            for (int i = 0; i < outputBilthoven.ToList().Count; i++)
                            {
                                if (outputBilthoven.ToList()[i].Bilthoven == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Bilthoven = true;
                                }
                                if (outputBilthoven.ToList()[i].Almere == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Almere = true;
                                }
                                if (outputBilthoven.ToList()[i].Lelystad == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Lelystad = true;
                                }
                            }
                            return;
                        case "Almere":
                            var outputAlmere = cnn.Query<Depots>($"select Depots.Bilthoven, Depots.Almere, Depots.Lelystad from Depots where Depots.Almere = {PersNr}", new DynamicParameters());
                            for (int i = 0; i < outputAlmere.ToList().Count; i++)
                            {
                                if (outputAlmere.ToList()[i].Bilthoven == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Bilthoven = true;
                                }
                                if (outputAlmere.ToList()[i].Almere == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Almere = true;
                                }
                                if (outputAlmere.ToList()[i].Lelystad == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Lelystad = true;
                                }
                            }
                            return;
                        case "Lelystad":
                            var outputLelystad = cnn.Query<Depots>($"select Depots.Bilthoven, Depots.Almere, Depots.Lelystad from Depots where Depots.Lelystad = {PersNr}", new DynamicParameters());
                            for (int i = 0; i < outputLelystad.ToList().Count; i++)
                            {
                                if (outputLelystad.ToList()[i].Bilthoven == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Bilthoven = true;
                                }
                                if (outputLelystad.ToList()[i].Almere == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Almere = true;
                                }
                                if (outputLelystad.ToList()[i].Lelystad == $"{PersNr}")
                                {
                                    EditAndOrRemoveEmployee.Lelystad = true;
                                }
                            }
                            return;
                        default:
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
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
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Bilthoven, Bezorger) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "DepotWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Bilthoven, Depot_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "SorteerWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Bilthoven, Sorteer_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "AllEmployees":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Bilthoven, Bezorger, Depot_personeel, Sorteer_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @Bezorger, @Depot_personeel, @Sorteer_personeel)", allEmployees);
                                    return;
                                default:
                                    MessageBox.Show("ERROR, neem contact op met admin.");
                                    return;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Almere, Bezorger) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "DepotWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Almere, Depot_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "SorteerWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Almere, Sorteer_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "AllEmployees":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Almere, Bezorger, Depot_personeel, Sorteer_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @Bezorger, @Depot_personeel, @Sorteer_personeel)", allEmployees);
                                    return;
                                default:
                                    MessageBox.Show("ERROR, neem contact op met admin.");
                                    return;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Lelystad, Bezorger) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "DepotWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Lelystad, Depot_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "SorteerWerk":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Lelystad, Sorteer_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @PersNr)", employee);
                                    return;
                                case "AllEmployees":
                                    cnn.Execute("insert into WerkDagen (Datum, Begin_tijd, Eind_tijd, PersId, Lelystad, Bezorger, Depot_personeel, Sorteer_personeel) values (@Datum, @Begin_tijd, @Eind_tijd, @PersNr, @PersNr, @Bezorger, @Depot_personeel, @Sorteer_personeel)", allEmployees);
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
                    MessageBox.Show($"Je hebt {allEmployees.PersNr} al ingepland op {allEmployees.Datum} {Environment.NewLine}en of begin tijd {allEmployees.Begin_tijd} of eind tijd {allEmployees.Eind_tijd} is hetzelfde");
                }
                else
                {
                    MessageBox.Show($"Je hebt {employee.PersNr} al ingepland op {employee.Datum} {Environment.NewLine}en of begin tijd {employee.Begin_tijd} of eind tijd {employee.Eind_tijd} is hetzelfde");
                }
                MessageBox.Show($"Admin error report: {Environment.NewLine}{ex.Message}");
            }
        }

        public static List<AddNewEmployeeId> SaveNewEmployee(AddNewEmployeeId newEmployeeId)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            if (newEmployeeId != null)
                            {
                                cnn.Execute($"insert into Personeel (Voornaam, Achternaam, PersNr, Bedrijf) values (@Voornaam, @Achternaam, @PersNr, @Bedrijfsnaam)", newEmployeeId);
                                cnn.Execute($"insert into Functie (Bezorger, Depot_personeel, Sorteer_personeel) values (@Bezorger, @Depot_personeel, @Sorteer_personeel)", newEmployeeId);
                                cnn.Execute($"insert into Depots (Bilthoven, Almere, Lelystad) values (@PersNr, @Almere, @Lelystad)", newEmployeeId);
                            }
                            var AllEmployeesBilthoven = cnn.Query<AddNewEmployeeId>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, PersNr, Bezorger, Depot_personeel, Sorteer_personeel, Almere, Bilthoven, Lelystad from Personeel inner join Functie on Functie.Bezorger = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf inner join Depots where Personeel.PersNr = Bilthoven");
                            for (int i = 0; i < AllEmployeesBilthoven.ToList().Count; i++)
                            {
                                if (AllEmployeesBilthoven.ToList()[i].Bezorger != null)
                                {
                                    AllEmployeesBilthoven.ToList()[i].Bezorger = "✓";
                                }
                                if (AllEmployeesBilthoven.ToList()[i].Depot_personeel != null)
                                {
                                    AllEmployeesBilthoven.ToList()[i].Depot_personeel = "✓";
                                }
                                if (AllEmployeesBilthoven.ToList()[i].Sorteer_personeel != null)
                                {
                                    AllEmployeesBilthoven.ToList()[i].Sorteer_personeel = "✓";
                                }
                                if (AllEmployeesBilthoven.ToList()[i].Bilthoven != null)
                                {
                                    AllEmployeesBilthoven.ToList()[i].Bilthoven = "✓";
                                }
                                if (AllEmployeesBilthoven.ToList()[i].Almere != null)
                                {
                                    AllEmployeesBilthoven.ToList()[i].Almere = "✓";
                                }
                                if (AllEmployeesBilthoven.ToList()[i].Lelystad != null)
                                {
                                    AllEmployeesBilthoven.ToList()[i].Lelystad = "✓";
                                }
                            }
                            return AllEmployeesBilthoven.ToList();
                        case "Almere":
                            if (newEmployeeId != null)
                            {
                                cnn.Execute($"insert into Personeel (Voornaam, Achternaam, PersNr) values (@Voornaam, @Achternaam, @PersNr)", newEmployeeId);
                                cnn.Execute($"insert into Functie (Bezorger, Depot_personeel, Sorteer_personeel) values (@Bezorger, @Depot_personeel, @Sorteer_personeel)", newEmployeeId);
                                cnn.Execute($"insert into Depots (Almere, Bilthoven, Lelystad) values (@PersNr, @Bilthoven, @Lelystad)", newEmployeeId);
                            }
                            var AllEmployeesAlmere = cnn.Query<AddNewEmployeeId>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, PersNr, Bezorger, Depot_personeel, Sorteer_personeel, Almere, Bilthoven, Lelystad from Personeel inner join Functie on Functie.Bezorger = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf inner join Depots where Personeel.PersNr = Almere");
                            for (int i = 0; i < AllEmployeesAlmere.ToList().Count; i++)
                            {
                                if (AllEmployeesAlmere.ToList()[i].Bezorger != null)
                                {
                                    AllEmployeesAlmere.ToList()[i].Bezorger = "✓";
                                }
                                if (AllEmployeesAlmere.ToList()[i].Depot_personeel != null)
                                {
                                    AllEmployeesAlmere.ToList()[i].Depot_personeel = "✓";
                                }
                                if (AllEmployeesAlmere.ToList()[i].Sorteer_personeel != null)
                                {
                                    AllEmployeesAlmere.ToList()[i].Sorteer_personeel = "✓";
                                }
                                if (AllEmployeesAlmere.ToList()[i].Bilthoven != null)
                                {
                                    AllEmployeesAlmere.ToList()[i].Bilthoven = "✓";
                                }
                                if (AllEmployeesAlmere.ToList()[i].Almere != null)
                                {
                                    AllEmployeesAlmere.ToList()[i].Almere = "✓";
                                }
                                if (AllEmployeesAlmere.ToList()[i].Lelystad != null)
                                {
                                    AllEmployeesAlmere.ToList()[i].Lelystad = "✓";
                                }
                            }
                            return AllEmployeesAlmere.ToList();
                        case "Lelystad":
                            if (newEmployeeId != null)
                            {
                                cnn.Execute($"insert into Personeel (Voornaam, Achternaam, PersNr) values (@Voornaam, @Achternaam, @PersNr)", newEmployeeId);
                                cnn.Execute($"insert into Functie (Bezorger, Depot_personeel, Sorteer_personeel) values (@Bezorger, @Depot_personeel, @Sorteer_personeel)", newEmployeeId);
                                cnn.Execute($"insert into Depots (Lelystad, Bilthoven, Almere) values (@PersNr, @Bilthoven, @Almere)", newEmployeeId);
                            }
                            var AllEmployeesLelystad = cnn.Query<AddNewEmployeeId>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, PersNr, Bezorger, Depot_personeel, Sorteer_personeel, Almere, Bilthoven, Lelystad from Personeel inner join Functie on Functie.Bezorger = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf inner join Depots where Personeel.PersNr = Lelystad");
                            for (int i = 0; i < AllEmployeesLelystad.ToList().Count; i++)
                            {
                                if (AllEmployeesLelystad.ToList()[i].Bezorger != null)
                                {
                                    AllEmployeesLelystad.ToList()[i].Bezorger = "✓";
                                }
                                if (AllEmployeesLelystad.ToList()[i].Depot_personeel != null)
                                {
                                    AllEmployeesLelystad.ToList()[i].Depot_personeel = "✓";
                                }
                                if (AllEmployeesLelystad.ToList()[i].Sorteer_personeel != null)
                                {
                                    AllEmployeesLelystad.ToList()[i].Sorteer_personeel = "✓";
                                }
                                if (AllEmployeesLelystad.ToList()[i].Bilthoven != null)
                                {
                                    AllEmployeesLelystad.ToList()[i].Bilthoven = "✓";
                                }
                                if (AllEmployeesLelystad.ToList()[i].Almere != null)
                                {
                                    AllEmployeesLelystad.ToList()[i].Almere = "✓";
                                }
                                if (AllEmployeesLelystad.ToList()[i].Lelystad != null)
                                {
                                    AllEmployeesLelystad.ToList()[i].Lelystad = "✓";
                                }
                            }
                            return AllEmployeesLelystad.ToList();
                        default:
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gebruiker bestaat al");
                MessageBox.Show("Admin error report: " + Environment.NewLine + ex.Message);
                return null;
            }
        }
    }
}
