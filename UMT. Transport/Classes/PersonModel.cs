﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UMT.Transport.Classes
{
    public class PersonModel
    {
        public string Bedrijfs_Naam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int PersNr { get; set; }
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
    }

    public class AllEmployeesPerDepot
    {
        public string Bedrijfs_Naam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string PersNr { get; set; }
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
        public string Bezorger { get; set; }
        public string Depot_personeel { get; set; }
        public string Sorteer_personeel { get; set; }
    }

    public class Functions
    {
        public int Bezorger { get; set; }
        public int Depot_personeel { get; set; }
        public int Sorteer_personeel { get; set; }
    }

    public class Depots
    {
        public string Bilthoven { get; set; }
        public string Almere { get; set; }
        public string Lelystad { get; set; }
    }

    public class AddNewEmployeeId
    {
        public string Bedrijfsnaam { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int PersNr { get; set; }
        public string Bezorger { get; set; }
        public string Depot_personeel { get; set; }
        public string Sorteer_personeel { get; set; }
        public string Bilthoven { get; set; }
        public string Almere { get; set; }
        public string Lelystad { get; set; }
    }

    public class DeleteEmployeeWorkday 
    {
        public string Datum { get; set; }
        public string Begin_tijd { get; set; }
        public string Eind_tijd { get; set; }
        public string PersId { get; set; }
        public string Bilthoven { get; set; }
        public string Almere { get; set; }
        public string Lelystad { get; set; }
        public string Bezorger { get; set; }
        public string Depot_personeel { get; set; }
        public string Sorteer_personeel { get; set; }
    }
}
