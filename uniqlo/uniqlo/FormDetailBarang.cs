using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using MySql.Data.MySqlClient;

namespace uniqlo
{
    public partial class FormDetailBarang : Form
    {
        private string idBarang;
        public FormDetailBarang(string id)
        {
            InitializeComponent();
            this.idBarang = id;
        }

        private void FormDetailBarang_Load(object sender, EventArgs e)
        {
            LoadDetailBarang();
        }

        private void LoadDetailBarang()
        {
            // Contoh: Ambil data barang dari database
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM barang WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idBarang);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Contoh: Tampilkan data barang di kontrol
                        label7.Text = reader["nama"].ToString();
                        label6.Text = $"Harga : Rp{int.Parse(reader["harga"].ToString()):N0}";
                        pictureBox2.Load(reader["url_gambar"].ToString());
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        label3.Text = "Kode Produk: " + reader["kode_barang"].ToString();
                        label4.Text = "Detail : " + reader["deskripsi"].ToString();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FormCart f = new FormCart();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
