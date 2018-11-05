using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ef_gui;

namespace Ef
{
    public class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
                "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

            SqlCommand command = new SqlCommand();
//            command.Connection = connection;

            command.CommandText = "select * from Artist where Name like 'a%'";

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Artist> artists = new List<Artist>();
            while (reader.Read())
            {
                Artist artist = new Artist();
                int artistId = reader.GetInt32(0);
                string name = reader.GetString(1);

                artists.Add(artist);
            }
            reader.Close();
            connection.Close();



        }
    }
}
