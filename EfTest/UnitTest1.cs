using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Ef_gui;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EfTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void 검색어가포함된모든아티스트를반환한다()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
                "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = $"select * from Artist where [Name] like 'A%'";

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Artist> artists = new List<Artist>();
            while (reader.Read())
            {

                Artist artist = new Artist();
                artist.ArtistId = reader.GetInt32(0);
                artist.Name = reader.GetString(1);

                artists.Add(artist);
            }
            reader.Close();
            connection.Close();

            Assert.IsTrue(artists.TrueForAll(x=>x.Name.ToLower().StartsWith("a")));
//            dbview.DataSource = artists;
        }
    }
}
