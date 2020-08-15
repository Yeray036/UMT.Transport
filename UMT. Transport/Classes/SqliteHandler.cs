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

namespace UMT.Transport.Classes
{
    public class SqliteHandler
    {
        private static string LoadConnectionString(string id = "UmtDb")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<PersonModel> LoadEmployeesOnDate(string datum)
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<PersonModel>($"select Naam, Achternaam, PersNr, Datum, Begin_tijd from WerkDagen inner join Personeel on WerkDagen.PersId = Personeel.PersNr where WerkDagen.Datum = '{datum}'", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static List<string> LoadAllEmployeesOnName()
        {
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<string>($"select Naam from Personeel", new DynamicParameters());
                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

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

        public static void SaveEmployee(PersonModel employee)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {

            }
        }
    }
}
