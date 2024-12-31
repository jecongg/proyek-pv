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
        private int idUser;
        string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
        public FormDetailBarang(string id, int idUser)
        {
            InitializeComponent();
            this.idBarang = id;
            this.idUser = idUser;
        }

        private void FormDetailBarang_Load(object sender, EventArgs e)
        {
            LoadDetailBarang();
        }

        private string selectedSize = null;

        private void LoadDetailBarang()
        {
            // Contoh: Ambil data barang dari database
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

                        int stokNoSize = Convert.ToInt32(reader["stok_nosize"]);
                        if (stokNoSize != -1)
                        {
                            groupBox1.Visible = false;
                        }
                        else
                        {
                            groupBox1.Visible = true;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select id from cart where id_user = @a", conn);
            cmd.Parameters.AddWithValue("@a", idUser);
            if (cmd.ExecuteScalar() == null)
            {
                MessageBox.Show("kosong brok");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SetActiveButton(Button activeButton)
        {
            foreach (Control control in this.Controls)
            {
                ResetButtons(control);
            }

            activeButton.BackColor = Color.Black;
            activeButton.ForeColor = Color.White;
            activeButton.FlatStyle = FlatStyle.Flat;
            activeButton.FlatAppearance.BorderColor = Color.Black;
            activeButton.FlatAppearance.BorderSize = 2;
        }

        private void ResetButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.White;
                    button.ForeColor = Color.Black;
                    button.FlatStyle = FlatStyle.Standard;
                }
                else
                {
                    ResetButtons(control);
                }
            }
        }

        private void aturTampilan(Button btn)
        {
            int stock = GetStockBySize(btn.Text);
            if (stock == 0)
            {
                labelStock.Text = "Out of stock";
                button2.Enabled = false;
            }
            else
            {
                labelStock.Text = $"In Stock: {stock}";
                button2.Enabled = true;
            }
            numericUpDown1.Maximum = stock;
            SetActiveButton(btn);
            selectedSize = btn.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private int GetStockBySize(string size)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT stok FROM stok WHERE id_barang = @id AND size = @size";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idBarang);
                cmd.Parameters.AddWithValue("@size", size);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
    }
}
