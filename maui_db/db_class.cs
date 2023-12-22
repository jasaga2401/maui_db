using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace maui_db
{
    internal class db_class
    {
        public static string _dbPath = string.Empty;

        public static void create_db()
        {

            var databaseName = "MyDatabase.db";
            _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName);
        
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS MyTable (uid INTEGER PRIMARY KEY, " +
                    "Text_Entry VARCHAR(2048) NULL)";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static void add_data()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO MyTable VALUES (NULL, @Entry);";
                insertCommand.Parameters.AddWithValue("@Entry", "Original data .. ");
                
                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static List<String> get_data()
        {
            List<String> entries = new List<string>();

            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT Text_Entry from MyTable", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }   

        public static void delete_data(string var_item)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename={_dbPath}"))
            {
                db.Open();

                SqliteCommand deleteCommand = new SqliteCommand();
                deleteCommand.Connection = db;

                Console.WriteLine("Abount to delete data "+var_item);

                // Use parameterized query to prevent SQL injection attacks
                deleteCommand.CommandText = "DELETE FROM MyTable WHERE Text_Entry = @Entry;";
                deleteCommand.Parameters.AddWithValue("@Entry", var_item);
                
                deleteCommand.ExecuteReader();

                db.Close();
            }
        }
    }
}
