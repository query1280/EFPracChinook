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
    public partial class ArtistForm : Form
    {
        private ArtistForm()
        {
            InitializeComponent();
        }

        public ArtistForm(Artist artist) :this()
        {
            _artist = artist;
        }

        private Artist _artist;

        private int GetArtistCount(char c)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString =
                "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

            SqlCommand command = new SqlCommand();
            command.CommandText = "select count(*) from Artist where Name like @Name + '%'";

            command.Parameters.AddWithValue("@Name", c);
            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count;
        }

        private void btnsave_Click(object sender, EventArgs e) //이해 안됨
        {
            _artist.Name = txtName.Text;

            if (_artist.ArtistId == 0)
            {

                int count = GetArtistCount(_artist.Name[0]);
                if (count > 3)
                    return;

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString =
                    "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

                SqlCommand command = new SqlCommand();
             
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Artist values (@Name)";

                command.Parameters.AddWithValue("@Name", _artist.Name);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString =
                    "Data Source = localhost; Initial Catalog = Chinook; Persist Security Info = True; User ID = sa; Password = tltmxpa";

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "update Artist set Name = @Name where ArtistId =@ArtistId";

                command.Parameters.AddWithValue("@Name", _artist.Name);
                command.Parameters.AddWithValue("@ArtistId", _artist.ArtistId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            Close();

        }

        private void ArtistForm_Load(object sender, EventArgs e)
        {
            txtArtistId.Text = _artist.ArtistId.ToString();
            txtName.Text = _artist.Name;
        }
    }
}
