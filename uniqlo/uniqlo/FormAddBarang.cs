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
    public partial class FormAddBarang : Form
    {
        string connectionString = "server=192.168.0.23;uid=admin_uniqlo;pwd=admin;database=db_uniqlo";
        List<CheckBox> listCheck;
        List<NumericUpDown> listSize;
        public FormAddBarang()
        {
            InitializeComponent();
            listCheck = new List<CheckBox>() { xs, s, m, l, xl, xxl, xxxl };
            listSize = new List<NumericUpDown>() { numStokXS, numStokS, numStokM, numStokL, numStokXL, numStokXXL, numStokXXXL };
            load();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FormAddBarang_Load(object sender, EventArgs e)
        {
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool cekBarangValid()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select count(*) from barang where nama = @a and id_kategori = @b", conn);
            cmd.Parameters.AddWithValue("@a", textNama.Text);
            cmd.Parameters.AddWithValue("@b", comboBox1.SelectedValue);
            int ada = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            if (ada != 0)
            {
                return false;
            }
            return true;
        }

        private void insertBarang()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("insert into barang (nama, harga, diskon, url_gambar, stok_nosize, id_kategori, kode_barang, deskripsi, diskon_start, diskon_end, returable) VALUES (@a , @b , @c , @d , @e , @f, @g, @h, @i, @j, @k)", conn);
            cmd.Parameters.AddWithValue("@a", textNama.Text);
            cmd.Parameters.AddWithValue("@b", numHarga.Value);
            if (checkBoxDiskon.Checked)
            {
                cmd.Parameters.AddWithValue("@c", numDiskon.Value);
                cmd.Parameters.AddWithValue("@i", dateStart.Value);
                cmd.Parameters.AddWithValue("@j", dateEnd.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@c", 0);
                cmd.Parameters.AddWithValue("@i", null);
                cmd.Parameters.AddWithValue("@j", null);
            }
            cmd.Parameters.AddWithValue("@d", textGambar.Text);
            if (radioNoSize.Checked)
            {
                cmd.Parameters.AddWithValue("@e", numStokNoSize.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@e", -1);
            }
            if (checkRetur.Checked)
            {
                cmd.Parameters.AddWithValue("@k", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@k", 0);
            }
            cmd.Parameters.AddWithValue("@f", comboBox1.SelectedValue);
            cmd.Parameters.AddWithValue("@g", textBox2.Text);
            cmd.Parameters.AddWithValue("@h", textBox1.Text);
            cmd.ExecuteNonQuery();
            cmd = new MySqlCommand("select max(id) from barang", conn);
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            if (radioSize.Checked)
            {
                for (int i = 0; i < listCheck.Count; i++)
                {
                    if (listCheck[i].Checked)
                    {
                        //disini mending entah dicheck ato gk size tetep dimasukkan ke stok meskipun stok = 0 biar gampang update
                        cmd = new MySqlCommand("insert into stok (id_barang, size, stok) values (@a , @b , @c)", conn);
                        cmd.Parameters.AddWithValue("@a", id);
                        cmd.Parameters.AddWithValue("@b", listCheck[i].Text);
                        cmd.Parameters.AddWithValue("@c", listSize[i].Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            
            conn.Close();
            MessageBox.Show("Barang berhasil ditambahkan");
        }

        private void btnAddBarang_Click(object sender, EventArgs e)
        {
            if(textNama.Text=="" || numHarga.Value==0 || textGambar.Text=="" || comboBox1.SelectedIndex==-1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Masih ada yang kosong!");
            }
            else if (checkBoxDiskon.Checked && numDiskon.Value > numHarga.Value)
            {
                MessageBox.Show("Diskon tidak boleh lebih dari harga!");
            }
            else if (!cekBarangValid())
            {
                MessageBox.Show("Barang sudah ada!");
            }
            else
            {
                if (radioSize.Checked)
                {
                    bool valid = false;
                    foreach (var item in listCheck)
                    {
                        if (item.Checked)
                        {
                            valid = true;
                        }
                    }
                    if (!valid)
                    {
                        MessageBox.Show("Pilih size yang tersedia!");
                    }
                    else
                    {
                        insertBarang();
                    }
                }
                else
                {
                    insertBarang();
                }
            }
            
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
                dateStart.Enabled = true;
                dateEnd.Enabled = true;
            }
            else
            {
                numDiskon.Enabled = false;
                dateEnd.Enabled = false;
                dateStart.Enabled = false;
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
    }
}
