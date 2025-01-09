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
using System.Net;

namespace uniqlo
{
    public partial class FormLaporanStok : Form
    {
        string connectionString = "server=192.168.0.23;uid=root;pwd=;database=db_uniqlo";
        public FormLaporanStok()
        {
            InitializeComponent();
            load();
        }
        private void loadNota(int id_kategori)
        {
            try
            {
                LaporanStok nota = new LaporanStok(); // class nota dari Nota.rpt
                nota.SetParameterValue("id_kategori", id_kategori); // parameter nya dari file Nota.rpt
                crystalReportViewer1.ReportSource = nota;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void load()
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormLaporanStok_Load(object sender, EventArgs e)
        {
            string imageUrl = "https://brandslogos.com/wp-content/uploads/images/large/uniqlo-logo.png";

            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(imageUrl);

                    using (var stream = new System.IO.MemoryStream(imageData))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue != null && comboBox2.SelectedValue != null)
            {
                int id = Convert.ToInt32(comboBox1.SelectedValue);
                loadNota(id);

            }
            else
            {
                MessageBox.Show("Pilih Pengguna dan Jenis Pakaian dulu!");
            }
        }
    }
}
