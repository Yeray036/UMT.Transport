using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UMT.Transport.Classes
{
    public class PersonModel
    {
        public string Bedrijfsnaam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int PersNr { get; set; }
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
    }

    public class SaveNewPerson
    {
        public int PersId { get; set; }
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
        public int Depot { get; set; }
        public int Functie { get; set; }
    }

    public class AllEmployeesPerDepot
    {
        public string Bedrijfsnaam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string PersNr { get; set; }
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
        public string Functie { get; set; }
    }

    public class Functions
    {
        public int Functie { get; set; }
    }

    public class Depots
    {
        public int Depot { get; set; }
    }

    public class EmployeeHasFunctie
    {
        public static int Bezorger { get; set; }
        public static int Sorteerpersoneel { get; set; }
        public static int Depotpersoneel { get; set; }
    }

    public class EmployeeNameLastnameAndPersnr
    {
        public string Naam { get; set; }
    }

    public class EmployeeVacationCount
    {
        public int hasCount { get; set; }
        public string start { get; set; }
        public string eind { get; set; }
    }

    public class EmployeeVacation
    {
        public string Verlof_start { get; set; }
        public string Verlof_eind { get; set; }
        public int PersNr { get; set; }
    }
    public class FullVacationList
    {
        public string Naam { get; set; }
        public string Verlof_start { get; set; }
        public string Verlof_eind { get; set; }
        public int PersNr { get; set; }
    }

    public class PersoneelTabel
    {
        public string Bedrijfsnaam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int PersNr { get; set; }
    }

    public class FunctieTabel
    {
        public int PersNr { get; set; }
        public string BezorgerInput { get; set; }
        public string DepotwerkInput { get; set; }
        public string SorteerwerkInput { get; set; }
    }
    public class DepotTabel
    {
        public int PersNr { get; set; }
        public string BilthovenInput { get; set; }
        public string AlmereInput { get; set; }
        public string LelystadInput { get; set; }
    }
    public class CurrentWorkdaysEmployees
    {
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
        public string Functie { get; set; }
    }
}
