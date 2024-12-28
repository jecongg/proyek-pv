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
            int idbrg=0, harga=0, diskon=0, stok_nosize=0, stok_xs = 0, stok_s = 0, stok_m = 0, stok_l = 0, stok_xl = 0, stok_xxl = 0, stok_3xl = 0, id_kategori =0;
            string nama = "", url_gambar = "";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT b.id, b.nama, b.harga, b.diskon, b.url_gambar, b.stok_nosize FROM barang b WHERE b.id = @id";
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
                        id_kategori = reader.GetInt32("id_kategori");
                        if (reader.GetInt32("stok_nosize") == -1)
                        {
                            radioSize.Checked = true;
                            query = "select size, stok from stok where id_barang = @id_barang";
                            cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id_barang", id);
                            using (MySqlDataReader reader1 = cmd.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    string size = reader1["size"].ToString();
                                    int stok = Convert.ToInt32(reader1["stok"]);

                                    switch (size)
                                    {
                                        case "XS":
                                            numStokXS.Value = (decimal)stok;
                                            break;
                                        case "S":
                                            numStokS.Value = stok;
                                            break;
                                        case "M":
                                            numStokM.Value = stok;
                                            break;
                                        case "L":
                                            numStokL.Value = stok;
                                            break;
                                        case "XL":
                                            numStokL.Value = stok;
                                            break;
                                        case "XXL":
                                            numStokXXL.Value = stok;
                                            break;
                                        case "3XL":
                                            numStokXXXL.Value = stok;
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            radioNoSize.Checked = true;
                            numStokNoSize.Value = reader.GetInt32("stok_nosize");
                        }
                        
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (xs.Checked)
            {
                numStokXS.Enabled = true;
            }
            else
            {
                numStokXS.Enabled = false;
            }
        }

        private void s_CheckedChanged(object sender, EventArgs e)
        {
            if (s.Checked)
            {
                numStokS.Enabled = true;
            }
            else
            {
                numStokS.Enabled = false;
            }
        }

        private void m_CheckedChanged(object sender, EventArgs e)
        {
            if (m.Checked)
            {
                numStokM.Enabled = true;
            }
            else
            {
                numStokM.Enabled = false;
            }
        }

        private void l_CheckedChanged(object sender, EventArgs e)
        {
            if (l.Checked)
            {
                numStokL.Enabled = true;
            }
            else
            {
                numStokL.Enabled = false;
            }
        }

        private void xl_CheckedChanged(object sender, EventArgs e)
        {
            if (xl.Checked)
            {
                numStokXL.Enabled = true;
            }
            else
            {
                numStokXL.Enabled = false;
            }
        }

        private void xxl_CheckedChanged(object sender, EventArgs e)
        {
            if (xxl.Checked)
            {
                numStokXXL.Enabled = true;
            }
            else
            {
                numStokXXL.Enabled = false;
            }
        }

        private void xxxl_CheckedChanged(object sender, EventArgs e)
        {
            if (xxxl.Checked)
            {
                numStokXXXL.Enabled = true;
            }
            else
            {
                numStokXXXL.Enabled = false;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            pictureBox1.Load(textGambar.Text);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void checkBoxDiskon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDiskon.Checked)
            {
                numDiskon.Enabled = true;
            }
            else
            {
                numDiskon.Enabled = false;
            }
        }

        private void radioSize_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSize.Checked)
            {
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true;
            }
        }
    }
}
