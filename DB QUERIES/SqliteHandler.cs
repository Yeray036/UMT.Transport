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
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using UMT.Transport.Pages.AdminPanel;
using UMT.Transport.UserControls;

namespace UMT.Transport.Classes
{
    /// SOFTWARE CREATED BY Yeray Guzmán Padrón.
    /// GITHUB: https://github.com/yeray036
    
    public class SqliteHandler
    {

        /*
        =================
        Functies: 
        1 = Bezorger
        2 = Sorteer_personeel
        3 = Depot_personeel
        =================
        Depots: 
        1 = Bilthoven
        2 = Almere
        3 = Lelystad
        =================
         */
        public static List<string> Bedrijven;
        public static dynamic SavedPersonReturnList;
        public static string CurrentEmployeeName;

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
                DateTime selectedDate = Convert.ToDateTime(datum);
                selectedDate.ToString("yyyy-MM-dd");
                string convertedDatum;
                string[] checkDate = selectedDate.ToString("yyyy-MM-dd").Split('-');
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputBilthovenBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 1 and Werkdagen.Functie = 1", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 1 and Werkdagen.Functie = 3", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 1 and Werkdagen.Functie = 2", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd, Werkdagen.Functie from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 1", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        switch (outputBilthovenAllEmployees.ToList()[i].Functie)
                                        {
                                            case "1":
                                                outputBilthovenAllEmployees.ToList()[i].Functie = "Bezorger";
                                                break;
                                            case "2":
                                                outputBilthovenAllEmployees.ToList()[i].Functie = "Sorteerpersoneel";
                                                break;
                                            case "3":
                                                outputBilthovenAllEmployees.ToList()[i].Functie = "Depotpersoneel";
                                                break;
                                            default:
                                                break;
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
                                    var outputAlmereBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 2 and Werkdagen.Functie = 1", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 2 and Werkdagen.Functie = 3", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 2 and Werkdagen.Functie = 2", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd, Werkdagen.Functie from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 2", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        switch (outputAlmereAllEmployees.ToList()[i].Functie)
                                        {
                                            case "1":
                                                outputAlmereAllEmployees.ToList()[i].Functie = "Bezorger";
                                                break;
                                            case "2":
                                                outputAlmereAllEmployees.ToList()[i].Functie = "Sorteerpersoneel";
                                                break;
                                            case "3":
                                                outputAlmereAllEmployees.ToList()[i].Functie = "Depotpersoneel";
                                                break;
                                            default:
                                                break;
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
                                    var outputLelystadBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 3 and Werkdagen.Functie = 1", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 3 and Werkdagen.Functie = 3", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 3 and Werkdagen.Functie = 2", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd, Werkdagen.Functie from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Depots on Depots.Depot = Werkdagen.Depot left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum = '{checkDate[0]}-{checkDate[1]}-{checkDate[2]}' and Werkdagen.Depot = 3", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        switch (outputLelystadAllEmployees.ToList()[i].Functie)
                                        {
                                            case "1":
                                                outputLelystadAllEmployees.ToList()[i].Functie = "Bezorger";
                                                break;
                                            case "2":
                                                outputLelystadAllEmployees.ToList()[i].Functie = "Sorteerpersoneel";
                                                break;
                                            case "3":
                                                outputLelystadAllEmployees.ToList()[i].Functie = "Depotpersoneel";
                                                break;
                                            default:
                                                break;
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
                                    var outputBilthovenBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 1 and Werkdagen.Functie = 1 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 1 and Werkdagen.Functie = 3 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 1 and Werkdagen.Functie = 2 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd, Werkdagen.Functie from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 1 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputBilthovenAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputBilthovenAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputBilthovenAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        switch (outputBilthovenAllEmployees.ToList()[i].Functie)
                                        {
                                            case "1":
                                                outputBilthovenAllEmployees.ToList()[i].Functie = "Bezorger";
                                                break;
                                            case "2":
                                                outputBilthovenAllEmployees.ToList()[i].Functie = "Sorteerpersoneel";
                                                break;
                                            case "3":
                                                outputBilthovenAllEmployees.ToList()[i].Functie = "Depotpersoneel";
                                                break;
                                            default:
                                                break;
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
                                    var outputAlmereBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 2 and Werkdagen.Functie = 1 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 2 and Werkdagen.Functie = 3 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 2 and Werkdagen.Functie = 2 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd, Werkdagen.Functie from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 2 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputAlmereAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputAlmereAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputAlmereAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        switch (outputAlmereAllEmployees.ToList()[i].Functie)
                                        {
                                            case "1":
                                                outputAlmereAllEmployees.ToList()[i].Functie = "Bezorger";
                                                break;
                                            case "2":
                                                outputAlmereAllEmployees.ToList()[i].Functie = "Sorteerpersoneel";
                                                break;
                                            case "3":
                                                outputAlmereAllEmployees.ToList()[i].Functie = "Depotpersoneel";
                                                break;
                                            default:
                                                break;
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
                                    var outputLelystadBezorger = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 3 and Werkdagen.Functie = 1 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadBezorger.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadBezorger.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadBezorger.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 3 and Werkdagen.Functie = 3 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadDepotWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadDepotWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadDepotWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<PersonModel>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 3 and Werkdagen.Functie = 2 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadSorteerWerk.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadSorteerWerk.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadSorteerWerk.ToList()[i].Datum = dt.ToShortDateString();
                                    }
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<AllEmployeesPerDepot>($"select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr, Datum, Begin_tijd, Eind_tijd, Werkdagen.Functie from Werkdagen left join Personeel on Personeel.PersNr = Werkdagen.PersId left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where Werkdagen.Datum between '{year}-{Firstdaysplit[1]}-{Firstdaysplit[0]}' and '{year}-{Lastdaysplit[1]}-{Lastdaysplit[0]}' and Werkdagen.Depot = 3 order by Werkdagen.Datum ASC", new DynamicParameters());
                                    for (int i = 0; i < outputLelystadAllEmployees.ToList().Count; i++)
                                    {
                                        DateTime dt = Convert.ToDateTime(outputLelystadAllEmployees.ToList()[i].Datum);
                                        dt.ToString("dd-MM-yyyy");
                                        outputLelystadAllEmployees.ToList()[i].Datum = dt.ToShortDateString();
                                        switch (outputLelystadAllEmployees.ToList()[i].Functie)
                                        {
                                            case "1":
                                                outputLelystadAllEmployees.ToList()[i].Functie = "Bezorger";
                                                break;
                                            case "2":
                                                outputLelystadAllEmployees.ToList()[i].Functie = "Sorteerpersoneel";
                                                break;
                                            case "3":
                                                outputLelystadAllEmployees.ToList()[i].Functie = "Depotpersoneel";
                                                break;
                                            default:
                                                break;
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
                                        var outputBilthovenBezorger = cnn.Query<string>($"select Voornaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where PersoneelHasDepot.Depot = 1 and PersoneelHasFunctie.Functie = 1 and Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputBilthovenBezorger.ToList();
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<string>($"select Voornaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 1 and PersoneelHasFunctie.Functie = 3", new DynamicParameters());
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<string>($"select Voornaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 1 and PersoneelHasFunctie.Functie = 2", new DynamicParameters());
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where PersoneelHasDepot.Depot = 1", new DynamicParameters());
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
                                        var outputBilthovenAllEmployeeAdminPanel = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where PersoneelHasDepot.Depot = 1 and Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputBilthovenAllEmployeeAdminPanel.ToList();
                                    }
                                    else
                                    {
                                        var outputBilthovenAdminPanel = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where PersoneelHasDepot.Depot = 1", new DynamicParameters());
                                        return outputBilthovenAdminPanel.ToList();
                                    }
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2 and PersoneelHasFunctie.Functie = 1", new DynamicParameters());
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2 and PersoneelHasFunctie.Functie = 3", new DynamicParameters());
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2 and PersoneelHasFunctie.Functie = 2", new DynamicParameters());
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where PersoneelHasDepot.Depot = 2", new DynamicParameters());
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
                                        var outputAlmereAllEmployeeAdminPanel = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where PersoneelHasDepot.Depot = 2 and Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputAlmereAllEmployeeAdminPanel.ToList();
                                    }
                                    else
                                    {
                                        var outputAlmereAdminPanel = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where PersoneelHasDepot.Depot = 2", new DynamicParameters());
                                        return outputAlmereAdminPanel.ToList();
                                    }
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3 and PersoneelHasFunctie.Functie = 1", new DynamicParameters());
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3 and PersoneelHasFunctie.Functie = 3", new DynamicParameters());
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3 and PersoneelHasFunctie.Functie = 2", new DynamicParameters());
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where PersoneelHasDepot.Depot = 3", new DynamicParameters());
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
                                        var outputLelystadAllEmployeeAdminPanel = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf where PersoneelHasDepot.Depot = 3 and Bedrijven.Naam = '{Bedrijf}'", new DynamicParameters());
                                        return outputLelystadAllEmployeeAdminPanel.ToList();
                                    }
                                    else
                                    {
                                        var outputLelystadAdminPanel = cnn.Query<string>($"select Voornaam from PersoneelHasDepot inner join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where PersoneelHasDepot.Depot = 3", new DynamicParameters());
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
                                    var outputBilthovenBezorger = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 1 and PersoneelHasFunctie.Functie = 1 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenBezorger.ToList();
                                case "DepotWerk":
                                    var outputBilthovenDepotWerk = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 1 and PersoneelHasFunctie.Functie = 3 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputBilthovenSorteerWerk = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 1 and PersoneelHasFunctie.Functie = 2 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputBilthovenSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputBilthovenAllEmployees = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr WHERE Voornaam = '{Name}' and PersoneelHasDepot.Depot = 1", new DynamicParameters());
                                    return outputBilthovenAllEmployees.ToList();
                                case "AdminPanel":
                                    var outputBilthovenAdminPanel = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr WHERE Voornaam = '{Name}' and PersoneelHasDepot.Depot = 1", new DynamicParameters());
                                    return outputBilthovenAdminPanel.ToList();
                                default:
                                    return null;
                            }
                        case "Almere":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputAlmereBezorger = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2 and PersoneelHasFunctie.Functie = 1 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereBezorger.ToList();
                                case "DepotWerk":
                                    var outputAlmereDepotWerk = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2 and PersoneelHasFunctie.Functie = 3 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputAlmereSorteerWerk = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2 and PersoneelHasFunctie.Functie = 2 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputAlmereSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputAlmereAllEmployees = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr WHERE Voornaam = '{Name}' and PersoneelHasDepot.Depot = 2", new DynamicParameters());
                                    return outputAlmereAllEmployees.ToList();
                                case "AdminPanel":
                                    var outputAlmereAdminPanel = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr WHERE Voornaam = '{Name}' and PersoneelHasDepot.Depot = 2", new DynamicParameters());
                                    return outputAlmereAdminPanel.ToList();
                                default:
                                    return null;
                            }
                        case "Lelystad":
                            switch (UcFunctions.SelectedFunction)
                            {
                                case "Bezorger":
                                    var outputLelystadBezorger = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3 and PersoneelHasFunctie.Functie = 1 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadBezorger.ToList();
                                case "DepotWerk":
                                    var outputLelystadDepotWerk = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3 and PersoneelHasFunctie.Functie = 3 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadDepotWerk.ToList();
                                case "SorteerWerk":
                                    var outputLelystadSorteerWerk = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join PersoneelHasFunctie on PersoneelHasFunctie.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3 and PersoneelHasFunctie.Functie = 2 and Voornaam = '{Name}'", new DynamicParameters());
                                    return outputLelystadSorteerWerk.ToList();
                                case "AllEmployees":
                                    var outputLelystadAllEmployees = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr WHERE Voornaam = '{Name}' and PersoneelHasDepot.Depot = 3", new DynamicParameters());
                                    return outputLelystadAllEmployees.ToList();
                                case "AdminPanel":
                                    var outputLelystadAdminPanel = cnn.Query<string>($"select Achternaam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr WHERE Voornaam = '{Name}' and PersoneelHasDepot.Depot = 3", new DynamicParameters());
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
                            var outputBilthoven = cnn.Query<Functions>($"select Functie from PersoneelHasFunctie left join Personeel on Personeel.PersNr = PersoneelHasFunctie.PersNr left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr where PersoneelHasFunctie.PersNr = '{PersNr}' and PersoneelHasDepot.Depot = 1", new DynamicParameters());
                            foreach (var item in outputBilthoven)
                            {
                                if (item.Functie == 1)
                                {
                                    EmployeeHasFunctie.Bezorger = 1;
                                }
                                if (item.Functie == 2)
                                {
                                    EmployeeHasFunctie.Sorteerpersoneel = 2;
                                }
                                if (item.Functie == 3)
                                {
                                    EmployeeHasFunctie.Depotpersoneel = 3;
                                }
                            }
                            return outputBilthoven.ToList();
                        case "Almere":
                            var outputAlmere = cnn.Query<Functions>($"select Functie from PersoneelHasFunctie left join Personeel on Personeel.PersNr = PersoneelHasFunctie.PersNr left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr where PersoneelHasFunctie.PersNr = '{PersNr}' and PersoneelHasDepot.Depot = 2", new DynamicParameters());
                            foreach (var item in outputAlmere)
                            {
                                if (item.Functie == 1)
                                {
                                    EmployeeHasFunctie.Bezorger = 1;
                                }
                                if (item.Functie == 2)
                                {
                                    EmployeeHasFunctie.Sorteerpersoneel = 2;
                                }
                                if (item.Functie == 3)
                                {
                                    EmployeeHasFunctie.Depotpersoneel = 3;
                                }
                            }
                            return outputAlmere.ToList();
                        case "Lelystad":
                            var outputLelystad = cnn.Query<Functions>($"select Functie from PersoneelHasFunctie left join Personeel on Personeel.PersNr = PersoneelHasFunctie.PersNr left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr where PersoneelHasFunctie.PersNr = '{PersNr}' and PersoneelHasDepot.Depot = 3", new DynamicParameters());
                            foreach (var item in outputLelystad)
                            {
                                if (item.Functie == 1)
                                {
                                    EmployeeHasFunctie.Bezorger = 1;
                                }
                                if (item.Functie == 2)
                                {
                                    EmployeeHasFunctie.Sorteerpersoneel = 2;
                                }
                                if (item.Functie == 3)
                                {
                                    EmployeeHasFunctie.Depotpersoneel = 3;
                                }
                            }
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
                    var outputActiveDepot = cnn.Query<Depots>($"select Depot from PersoneelHasDepot left join Personeel on Personeel.PersNr = PersoneelHasDepot.PersNr where Personeel.PersNr = '{PersNr}'", new DynamicParameters());
                    for (int i = 0; i < outputActiveDepot.ToList().Count; i++)
                    {
                        if (outputActiveDepot.ToList()[i].Depot == 1)
                        {
                            EditAndOrRemoveEmployee.Bilthoven = true;
                        }
                        if (outputActiveDepot.ToList()[i].Depot == 2)
                        {
                            EditAndOrRemoveEmployee.Almere = true;
                        }
                        if (outputActiveDepot.ToList()[i].Depot == 3)
                        {
                            EditAndOrRemoveEmployee.Lelystad = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public static void SaveNewEmployeeWorkDay(SaveNewPerson employee)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    string[] checkDate = employee.Datum.Split('-');
                    if (checkDate[2].Length == 1)
                    {
                        employee.Datum = $"{checkDate[0]}-{checkDate[1]}-0{checkDate[2]}";
                    }
                    var count = cnn.Query<EmployeeVacationCount>($"select Verlof_start as start, Verlof_eind as eind, count() as hasCount from Verlof where Verlof.PersNr = {employee.PersId} and '{employee.Datum}' BETWEEN Verlof_start and Verlof_eind", new DynamicParameters());
                    if (count.ToList()[0].hasCount >= 1)
                    {
                        MessageBox.Show($"{CurrentEmployeeName} is met verlof van {count.ToList()[0].start} tot en met {count.ToList()[0].eind}.");
                    }
                    else
                    {
                        cnn.Execute($"insert into Werkdagen (Datum, Begin_tijd, Eind_tijd, PersId, Depot, Functie) select '{employee.Datum}', '{employee.Begin_tijd}', '{employee.Eind_tijd}', {employee.PersId}, {employee.Depot}, {employee.Functie} where '{employee.Datum}' not BETWEEN '{count.ToList()[0].start}' and '{count.ToList()[0].eind}'");
                    }
                }
            }
            catch (Exception ex)
            {
                if (employee != null)
                {
                    MessageBox.Show($"Je hebt {CurrentEmployeeName} al ingepland op {employee.Datum} {Environment.NewLine}en of begin tijd {employee.Begin_tijd} of eind tijd {employee.Eind_tijd} is hetzelfde");
                }
                else
                {
                    MessageBox.Show($"Admin error report: {Environment.NewLine}{ex.Message}");
                }
            }
        }

        public static dynamic SaveNewEmployee(PersoneelTabel newPerson, FunctieTabel functieTabel, DepotTabel depotTabel)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    if (newPerson != null && functieTabel != null && depotTabel != null)
                    {
                        cnn.Execute($"insert into Personeel (Voornaam, Achternaam, PersNr, Bedrijf) values (@Voornaam, @Achternaam, @PersNr, @Bedrijfsnaam)", newPerson);
                        if (functieTabel.BezorgerInput != null && functieTabel.BezorgerInput.Contains("1"))
                        {
                            cnn.Execute($"insert into PersoneelHasFunctie (PersNr, Functie) values (@PersNr, 1)", functieTabel);
                        }
                        if (functieTabel.SorteerwerkInput != null && functieTabel.SorteerwerkInput.Contains("2"))
                        {
                            cnn.Execute($"insert into PersoneelHasFunctie (PersNr, Functie) values (@PersNr, 2)", functieTabel);
                        }
                        if (functieTabel.DepotwerkInput != null && functieTabel.DepotwerkInput.Contains("3"))
                        {
                            cnn.Execute($"insert into PersoneelHasFunctie (PersNr, Functie) values (@PersNr, 3)", functieTabel);
                        }
                        if (depotTabel.BilthovenInput != null && depotTabel.BilthovenInput.Contains("1"))
                        {
                            cnn.Execute($"insert into PersoneelHasDepot (PersNr, Depot) values (@PersNr, 1)", depotTabel);
                        }
                        if (depotTabel.AlmereInput != null && depotTabel.AlmereInput.Contains("2"))
                        {
                            cnn.Execute($"insert into PersoneelHasDepot (PersNr, Depot) values (@PersNr, 2)", depotTabel);
                        }
                        if (depotTabel.LelystadInput != null && depotTabel.LelystadInput.Contains("3"))
                        {
                            cnn.Execute($"insert into PersoneelHasDepot (PersNr, Depot) values (@PersNr, 3)", depotTabel);
                        }
                    }
                    if (UcDepots.SelectedDepot == "Bilthoven")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 1", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return AllEmployees.ToList();
                    }
                    if (UcDepots.SelectedDepot == "Almere")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 2", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return AllEmployees.ToList();
                    }
                    if (UcDepots.SelectedDepot == "Lelystad")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 3", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return AllEmployees.ToList();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gebruiker bestaat al");
                MessageBox.Show("Admin error report: " + Environment.NewLine + ex.Message);
                return null;
            }
        }

        public static void UpdateEmployee(PersoneelTabel newPerson, FunctieTabel functieTabel, DepotTabel depotTabel)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    if (newPerson != null && functieTabel != null && depotTabel != null)
                    {
                        switch (UcDepots.SelectedDepot)
                        {
                            case "Bilthoven":
                                cnn.Execute($"update Personeel set Voornaam='{newPerson.Voornaam}', Achternaam='{newPerson.Achternaam}', Bedrijf='{newPerson.Bedrijfsnaam}' where PersNr='{newPerson.PersNr}'", newPerson);
                                break;
                            case "Almere":
                                cnn.Execute($"update Personeel set Voornaam='{newPerson.Voornaam}', Achternaam='{newPerson.Achternaam}' where PersNr='{newPerson.PersNr}'", newPerson);
                                break;
                            case "Lelystad":
                                cnn.Execute($"update Personeel set Voornaam='{newPerson.Voornaam}', Achternaam='{newPerson.Achternaam}' where PersNr='{newPerson.PersNr}'", newPerson);
                                break;
                            default:
                                break;
                        }
                        if (functieTabel.BezorgerInput != null && functieTabel.BezorgerInput.Contains("1"))
                        {
                            cnn.Execute($"INSERT INTO PersoneelHasFunctie (PersNr, Functie) select {functieTabel.PersNr}, 1 where not EXISTS(select 1 from PersoneelHasFunctie where PersNr = {functieTabel.PersNr} and Functie = 1)");
                        }
                        else
                        {
                            cnn.Execute($"delete from PersoneelHasFunctie where PersNr = {functieTabel.PersNr} and Functie = 1");
                        }

                        if (functieTabel.SorteerwerkInput != null && functieTabel.SorteerwerkInput.Contains("2"))
                        {
                            cnn.Execute($"INSERT INTO PersoneelHasFunctie (PersNr, Functie) select {functieTabel.PersNr}, 2 where not EXISTS(select 1 from PersoneelHasFunctie where PersNr = {functieTabel.PersNr} and Functie = 2)");
                        }
                        else
                        {
                            cnn.Execute($"delete from PersoneelHasFunctie where PersNr = {functieTabel.PersNr} and Functie = 2");
                        }

                        if (functieTabel.DepotwerkInput != null && functieTabel.DepotwerkInput.Contains("3"))
                        {
                            cnn.Execute($"INSERT INTO PersoneelHasFunctie (PersNr, Functie) select {functieTabel.PersNr}, 3 where not EXISTS(select 1 from PersoneelHasFunctie where PersNr = {functieTabel.PersNr} and Functie = 3)");
                        }
                        else
                        {
                            cnn.Execute($"delete from PersoneelHasFunctie where PersNr = {functieTabel.PersNr} and Functie = 3");
                        }

                        if (depotTabel.BilthovenInput != null && depotTabel.BilthovenInput.Contains("1"))
                        {
                            cnn.Execute($"insert into PersoneelHasDepot (PersNr, Depot) select {depotTabel.PersNr}, 1 where not EXISTS(select 1 from PersoneelHasDepot where PersNr = {depotTabel.PersNr} and Depot = 1)");
                        }
                        else
                        {
                            cnn.Execute($"delete from PersoneelHasDepot where PersNr = {depotTabel.PersNr} and Depot = 1");
                        }

                        if (depotTabel.AlmereInput != null && depotTabel.AlmereInput.Contains("2"))
                        {
                            cnn.Execute($"insert into PersoneelHasDepot (PersNr, Depot) select {depotTabel.PersNr}, 2 where not EXISTS(select 1 from PersoneelHasDepot where PersNr = {depotTabel.PersNr} and Depot = 2)");
                        }
                        else
                        {
                            cnn.Execute($"delete from PersoneelHasDepot where PersNr = {depotTabel.PersNr} and Depot = 2");
                        }

                        if (depotTabel.LelystadInput != null && depotTabel.LelystadInput.Contains("3"))
                        {
                            cnn.Execute($"insert into PersoneelHasDepot (PersNr, Depot) select {depotTabel.PersNr}, 3 where not EXISTS(select 1 from PersoneelHasDepot where PersNr = {depotTabel.PersNr} and Depot = 3)");
                        }
                        else
                        {
                            cnn.Execute($"delete from PersoneelHasDepot where PersNr = {depotTabel.PersNr} and Depot = 3");
                        }
                    }
                    if (UcDepots.SelectedDepot == "Bilthoven")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 1", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return;
                    }
                    if (UcDepots.SelectedDepot == "Almere")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 2", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return;
                    }
                    if (UcDepots.SelectedDepot == "Lelystad")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 3", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Geen veranderingen uigevoerd.");
                MessageBox.Show(ex.Message);
            }
        }

        public static void DeleteEmployee(string PersNr)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    cnn.Execute($"delete from PersoneelHasFunctie where PersNr = {PersNr}");
                    cnn.Execute($"delete from PersoneelHasDepot where PersNr = {PersNr}");
                    cnn.Execute($"delete from Werkdagen where PersId = {PersNr}");
                    cnn.Execute($"delete from Verlof where PersNr = {PersNr}");
                    cnn.Execute($"delete from Personeel where PersNr = {PersNr}");
                    if (UcDepots.SelectedDepot == "Bilthoven")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 1", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return;
                    }
                    if (UcDepots.SelectedDepot == "Almere")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 2", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return;
                    }
                    if (UcDepots.SelectedDepot == "Lelystad")
                    {
                        var AllEmployees = cnn.Query<PersoneelTabel>("select Naam as Bedrijfsnaam, Voornaam, Achternaam, Personeel.PersNr from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr left join Bedrijven on Bedrijven.Naam = Personeel.Bedrijf left join Depots on Depots.Id = PersoneelHasDepot.Depot where Depots.Id = 3", new DynamicParameters());
                        SavedPersonReturnList = AllEmployees.ToList();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Werknemer kon niet worden verwijderd.");
                MessageBox.Show(ex.Message);
            }
        }

        public static List<EmployeeNameLastnameAndPersnr> GetEmployeeNamesAndPersNr()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            var BilthovenEmployees = cnn.Query<EmployeeNameLastnameAndPersnr>($"select Voornaam || ' ' || Achternaam as Naam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 1", new DynamicParameters());
                            return BilthovenEmployees.ToList();
                        case "Almere":
                            var AlmereEmployees = cnn.Query<EmployeeNameLastnameAndPersnr>($"select Voornaam || ' ' || Achternaam as Naam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 2", new DynamicParameters());
                            return AlmereEmployees.ToList();
                        case "Lelystad":
                            var LelystadEmployees = cnn.Query<EmployeeNameLastnameAndPersnr>($"select Voornaam || ' ' || Achternaam as Naam from Personeel left join PersoneelHasDepot on PersoneelHasDepot.PersNr = Personeel.PersNr where PersoneelHasDepot.Depot = 3", new DynamicParameters());
                            return LelystadEmployees.ToList();
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

        public static List<FullVacationList> fullVacations()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var VacationList = cnn.Query<FullVacationList>($"select Voornaam || ' ' || Achternaam as Naam, Verlof_start, Verlof_eind, Verlof.PersNr from Verlof left join Personeel on Personeel.PersNr = Verlof.PersNr", new DynamicParameters());
                    for (int i = 0; i < VacationList.ToList().Count; i++)
                    {
                        DateTime dt = Convert.ToDateTime(VacationList.ToList()[i].Verlof_start);
                        dt.ToString("dd-MM-yyyy");
                        VacationList.ToList()[i].Verlof_start = dt.ToShortDateString();
                        DateTime dt2 = Convert.ToDateTime(VacationList.ToList()[i].Verlof_eind);
                        dt2.ToString("dd-MM-yyyy");
                        VacationList.ToList()[i].Verlof_eind = dt2.ToShortDateString();
                    }
                    return VacationList.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kan verlof schema niet ophalen: {Environment.NewLine} {ex.Message}");
                return null;
            }
        }

        public static void InsertVacationIntoDb(EmployeeVacation employeeVacation)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    if (employeeVacation != null)
                    {
                        cnn.Execute($"insert into Verlof (Verlof_start, Verlof_eind, PersNr) values ('{employeeVacation.Verlof_start}', '{employeeVacation.Verlof_eind}', {employeeVacation.PersNr})");
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Contact admin " + ex.Message);
                return;
            }
        }

        public static dynamic GetAllWorkdaysFromEmployee(int PersNr)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    switch (UcDepots.SelectedDepot)
                    {
                        case "Bilthoven":
                            var BilthovenWorkdays = cnn.Query<CurrentWorkdaysEmployees>($"select Werkdagen.Datum, Werkdagen.Begin_tijd, Werkdagen.Eind_tijd, Werkdagen.Functie from Werkdagen where Werkdagen.PersId = {PersNr} and Werkdagen.Depot = 1", new DynamicParameters());
                            for (int i = 0; i < BilthovenWorkdays.ToList().Count; i++)
                            {
                                DateTime dt = Convert.ToDateTime(BilthovenWorkdays.ToList()[i].Datum);
                                dt.ToString("dd-MM-yyyy");
                                BilthovenWorkdays.ToList()[i].Datum = dt.ToShortDateString();
                                if (BilthovenWorkdays.ToList()[i].Functie == "1")
                                {
                                    BilthovenWorkdays.ToList()[i].Functie = "Bezorger";
                                }
                                if (BilthovenWorkdays.ToList()[i].Functie == "2")
                                {
                                    BilthovenWorkdays.ToList()[i].Functie = "Sorteerpersoneel";
                                }
                                if (BilthovenWorkdays.ToList()[i].Functie == "3")
                                {
                                    BilthovenWorkdays.ToList()[i].Functie = "Depotpersoneel";
                                }
                            }
                            return BilthovenWorkdays.ToList();
                        case "Almere":
                            var AlmereWorkdays = cnn.Query<CurrentWorkdaysEmployees>($"select Werkdagen.Datum, Werkdagen.Begin_tijd, Werkdagen.Eind_tijd, Werkdagen.Functie from Werkdagen where Werkdagen.PersId = {PersNr} and Werkdagen.Depot = 2", new DynamicParameters());
                            for (int i = 0; i < AlmereWorkdays.ToList().Count; i++)
                            {
                                DateTime dt = Convert.ToDateTime(AlmereWorkdays.ToList()[i].Datum);
                                dt.ToString("dd-MM-yyyy");
                                AlmereWorkdays.ToList()[i].Datum = dt.ToShortDateString();
                                if (AlmereWorkdays.ToList()[i].Functie == "1")
                                {
                                    AlmereWorkdays.ToList()[i].Functie = "Bezorger";
                                }
                                if (AlmereWorkdays.ToList()[i].Functie == "2")
                                {
                                    AlmereWorkdays.ToList()[i].Functie = "Sorteerpersoneel";
                                }
                                if (AlmereWorkdays.ToList()[i].Functie == "3")
                                {
                                    AlmereWorkdays.ToList()[i].Functie = "Depotpersoneel";
                                }
                            }
                            return AlmereWorkdays.ToList();
                        case "Lelystad":
                            var LelystadWorkdays = cnn.Query<CurrentWorkdaysEmployees>($"select Werkdagen.Datum, Werkdagen.Begin_tijd, Werkdagen.Eind_tijd, Werkdagen.Functie from Werkdagen where Werkdagen.PersId = {PersNr} and Werkdagen.Depot = 3", new DynamicParameters());
                            for (int i = 0; i < LelystadWorkdays.ToList().Count; i++)
                            {
                                DateTime dt = Convert.ToDateTime(LelystadWorkdays.ToList()[i].Datum);
                                dt.ToString("dd-MM-yyyy");
                                LelystadWorkdays.ToList()[i].Datum = dt.ToShortDateString();
                                if (LelystadWorkdays.ToList()[i].Functie == "1")
                                {
                                    LelystadWorkdays.ToList()[i].Functie = "Bezorger";
                                }
                                if (LelystadWorkdays.ToList()[i].Functie == "2")
                                {
                                    LelystadWorkdays.ToList()[i].Functie = "Sorteerpersoneel";
                                }
                                if (LelystadWorkdays.ToList()[i].Functie == "3")
                                {
                                    LelystadWorkdays.ToList()[i].Functie = "Depotpersoneel";
                                }
                            }
                            return LelystadWorkdays.ToList();
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
        public static void RemoveSelectedWorkdays(List<string> selectedDates, int PersNr, List<string> selectedDateTimes)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    for (int i = 0; i < selectedDates.Count; i++)
                    {
                        cnn.Execute($"delete from Werkdagen where Werkdagen.Datum = '{selectedDates[i]}' and Werkdagen.Begin_tijd = '{selectedDateTimes[i]}' and Werkdagen.PersId = {PersNr}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
