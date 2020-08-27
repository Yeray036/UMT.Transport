using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMT.Transport.Classes
{
    public class PersonModel
    {
        public string Bedrijfs_Naam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int PersNr { get; set; }
        public string Datum { get; set; }
        public string Jaar { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
    }

    public class AllEmployeesPerDepot
    {
        public string Bedrijfs_Naam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int PersNr { get; set; }
        public string Datum { get; set; }
        public string Jaar { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
        public int Bezorger { get; set; }
        public int Depot_personeel { get; set; }
        public int Sorteer_personeel { get; set; }
    }

    public class Functions
    {
        public int Bezorger { get; set; }
        public int Depot_personeel { get; set; }
        public int Sorteer_personeel { get; set; }
    }
}
