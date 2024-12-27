using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace uniqlo
{
    public partial class FormUpdateBarang : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
        public FormUpdateBarang(int id)
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadBarang(int id)
        {
            int idbrg=0, harga=0, diskon=0, stok=0, id_kategori=0;
            string nama = "", url_gambar = "";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT b.id, b.nama, b.harga, b.diskon, b.url_gambar, CASE WHEN b.stok_nosize = -1 THEN s.stok ELSE b.stok_nosize END AS total_stok FROM barang b LEFT JOIN stok s ON b.id = s.id_barang AND b.stok_nosize = -1 WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        idbrg = reader.GetInt32("id");
                        nama = reader.GetString("nama");
                        harga = reader.GetInt32("harga");
                        diskon = reader.GetInt32("diskon");
                        url_gambar = reader.GetString("url_gambar");
                        stok = reader.GetInt32("total_stok");
                        id_kategori = reader.GetInt32("id_kategori");
                    }
                }
            }

            textNama.Text = nama;
            
        }

        private void loadCheckBox()
        {

            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from pengguna", conn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "nama";
            comboBox2.ValueMember = "id";
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from kategori where id_pengguna= @a", conn);
            cmd.Parameters.AddWithValue("@a", comboBox2.SelectedValue);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "nama";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;
        }

        private void FormUpdateBarang_Load(object sender, EventArgs e)
        {

        }
    }
}
