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
        List<CheckBox> listCheck;
        List<NumericUpDown> listSize;
        public int id;
        public FormUpdateBarang(int id)
        {
            InitializeComponent();
            this.id = id;
            listCheck = new List<CheckBox>() { xs, s, m, l, xl, xxl, xxxl };
            listSize = new List<NumericUpDown>() { numStokXS, numStokS, numStokM, numStokL, numStokXL, numStokXXL, numStokXXXL };
            loadCheckBox();
            loadBarang(id);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loadBarang(int id)
        {
            int idbrg = 0, id_pengguna = 0, harga = 0, diskon = 0, stok_nosize = 0, id_kategori = 0, returable=0;
            string nama = "", url_gambar = "", namaKategori = "", namaPengguna = "", deskripsi = "";
            DateTime diskonStart, diskonEnd;

            using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                conn.Open();

                string query = "SELECT b.id, b.nama, b.harga, b.returable, b.diskon, b.url_gambar, b.stok_nosize, k.nama as nama_kategori, k.id as id_kategori, p.nama as nama_pengguna, p.id as id_pengguna, b.deskripsi, b.diskon_start, b.diskon_end FROM barang b join kategori k ON b.id_kategori = k.id join pengguna p ON k.id_pengguna = p.id WHERE b.id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idbrg = reader.GetInt32("id");
                            id = idbrg;
                            nama = reader.GetString("nama");
                            harga = reader.GetInt32("harga");
                            diskon = reader.GetInt32("diskon");
                            url_gambar = reader.GetString("url_gambar");
                            stok_nosize = reader.GetInt32("stok_nosize");
                            returable = reader.GetInt32("returable");
                            namaKategori = reader.GetString("nama_kategori");
                            namaPengguna = reader.GetString("nama_pengguna");
                            id_kategori = reader.GetInt32("id_kategori");
                            id_pengguna = reader.GetInt32("id_pengguna");
                            deskripsi = reader.GetString("deskripsi");
                            if (!reader.IsDBNull(reader.GetOrdinal("diskon_start")))
                            {
                                diskonStart = reader.GetDateTime("diskon_start");
                                dateStart.Value = diskonStart;
                            }
                            else
                            {
                                diskonStart = DateTime.MinValue; // Atau abaikan jika tidak diperlukan
                                dateStart.Enabled = false; // Atur logika untuk menangani nilai null
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("diskon_end")))
                            {
                                diskonEnd = reader.GetDateTime("diskon_end");
                                dateEnd.Value = diskonEnd;
                            }
                            else
                            {
                                diskonEnd = DateTime.MinValue; // Atau abaikan jika tidak diperlukan
                                dateEnd.Enabled = false; // Atur logika untuk menangani nilai null
                            }
                        }
                    }
                }

                if (stok_nosize == -1)
                {
                    radioSize.Checked = true;

                    query = "SELECT size, stok FROM stok WHERE id_barang = @id_barang";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_barang", id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string size = reader.GetString("size");
                                int stok = reader.GetInt32("stok");

                                switch (size)
                                {
                                    case "XS":
                                        if(stok != 0)
                                        {
                                            xs.Checked = true;
                                        }
                                        numStokXS.Value = (decimal)stok;
                                        break;
                                    case "S":
                                        if (stok != 0)
                                        {
                                            s.Checked = true;
                                        }
                                        numStokS.Value = (decimal)stok;
                                        break;
                                    case "M":
                                        if (stok != 0)
                                        {
                                            m.Checked = true;
                                        }
                                        numStokM.Value = (decimal)stok;
                                        break;
                                    case "L":
                                        if (stok != 0)
                                        {
                                            l.Checked = true;
                                        }
                                        numStokL.Value = (decimal)stok;
                                        break;
                                    case "XL":
                                        if (stok != 0)
                                        {
                                            xl.Checked = true;
                                        }
                                        numStokXL.Value = (decimal)stok;
                                        break;
                                    case "XXL":
                                        if (stok != 0)
                                        {
                                            xxl.Checked = true;
                                        }
                                        numStokXXL.Value = (decimal)stok;
                                        break;
                                    case "3XL":
                                        if (stok != 0)
                                        {
                                            xxxl.Checked = true;
                                        }
                                        numStokXXXL.Value = (decimal)stok;
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    radioNoSize.Checked = true;
                    numStokNoSize.Value = stok_nosize;
                }
            }

            textNama.Text = nama;
            numHarga.Value = (decimal)harga;
            numDiskon.Value = (decimal)diskon;
            textBox1.Text = deskripsi;
            textGambar.Text = url_gambar;
            if(diskon == 0)
            {
                checkBoxDiskon.Checked = false;
            }
            else
            {
                checkBoxDiskon.Checked = true;
                dateEnd.Enabled = true;
                dateStart.Enabled = true;
            }


            if (returable == 1)
            {
                checkRetur.Checked = true;
            }
            pictureBox1.Load(url_gambar);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            comboBox2.Text = namaPengguna;
            comboBox2.SelectedValue = id_pengguna;
            comboBox1.Text = namaKategori;
            comboBox1.SelectedValue = id_kategori;
            radioNoSize.Enabled = false;
            radioSize.Enabled = false;

        }


        private void loadCheckBox()
        {

            MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
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
            MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
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

        private void radioNoSize_CheckedChanged(object sender, EventArgs e)
        {

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

        private void xs_CheckedChanged(object sender, EventArgs e)
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

        private void checkBoxDiskon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxDiskon.Checked)
            {
                numDiskon.Enabled = true;
                dateEnd.Enabled = true;
                dateStart.Enabled = true;
            }
            else
            {
                numDiskon.Enabled = false;
                dateStart.Enabled = false;
                dateEnd.Enabled = false;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            pictureBox1.Load(textGambar.Text);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnAddBarang_Click(object sender, EventArgs e)
        {
            if (textNama.Text == "" || numHarga.Value == 0 || textGambar.Text == "" || comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Masih ada yang kosong!");
            }
            else if (checkBoxDiskon.Checked && numDiskon.Value > numHarga.Value)
            {
                MessageBox.Show("Diskon tidak boleh lebih dari harga!");
            }
            else
            {
                updateBarang();
            }
        }

        private void updateBarang()
        {
            MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            conn.Open();
            
            MySqlCommand cmd = new MySqlCommand("update barang set nama = @nama, harga = @harga, diskon = @diskon, url_gambar = @url_gambar, stok_nosize = @stok, id_kategori = @id_kategori, deskripsi = @deskripsi, diskon_start = @start, diskon_end = @end, returable = @k where id = @id", conn);
            cmd.Parameters.AddWithValue("@nama", textNama.Text);
            cmd.Parameters.AddWithValue("@harga", numHarga.Value);
            cmd.Parameters.AddWithValue("@url_gambar", textGambar.Text);
            cmd.Parameters.AddWithValue("@deskripsi", textBox1.Text);
            if (checkBoxDiskon.Checked)
            {
                cmd.Parameters.AddWithValue("@diskon", numDiskon.Value);
                cmd.Parameters.AddWithValue("@start", dateStart.Value);
                cmd.Parameters.AddWithValue("@end", dateEnd.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@diskon", 0);
                cmd.Parameters.AddWithValue("@start", null);
                cmd.Parameters.AddWithValue("@end", null);
            }
            if (radioNoSize.Checked)
            {
                cmd.Parameters.AddWithValue("@stok", numStokNoSize.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@stok", -1);
            }
            if (checkRetur.Checked)
            {
                cmd.Parameters.AddWithValue("@k", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@k", 0);
            }
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@id_kategori", comboBox1.SelectedValue);
            cmd.ExecuteNonQuery();
            if (radioSize.Checked)
            {
                for (int i = 0; i < listCheck.Count; i++)
                {
                    if (listCheck[i].Checked)
                    {
                        cmd = new MySqlCommand("update stok set stok = @stok where id_barang = @id AND size = @size", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@size", listCheck[i].Text);
                        cmd.Parameters.AddWithValue("@stok", listSize[i].Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            conn.Close();
            MessageBox.Show("Berhasil Update Barang");
            
            
        }

        private bool cekBarangValid()
        {
            MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
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
    }
}
