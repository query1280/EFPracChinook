using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ef_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
                "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"select * from Artist where [Name] like @Name + '%'";

            command.Parameters.AddWithValue("@Name", txtsearch.Text);

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


            artistBindingSource.DataSource = artists;
        }

        private void tsbadd_Click(object sender, EventArgs e)
        {
            Artist artist = new Artist();
            ArtistForm form = new ArtistForm(artist);
            form.ShowDialog();

            btnsearch.PerformClick();
        }

        private void tsbupdate_Click(object sender, EventArgs e)
        {
            Artist artist = (Artist) artistBindingSource.Current;
            ArtistForm form = new ArtistForm(artist);
            form.ShowDialog();

            btnsearch.PerformClick();
        }

        private void tsbdelete_Click(object sender, EventArgs e)
        {
            Artist artist = (Artist) artistBindingSource.Current;
            int artistId=artist.ArtistId;
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
                "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = $"delete Artist where ArtistId = {artistId}";

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
    
}
