using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using MySql.Data.MySqlClient;

namespace uniqlo
{
    public partial class FormCustomer : Form
    {
        string connectionString = "server=localhost;uid=customer_uniqlo;pwd=customer;database=db_uniqlo";
        private int idUser;
        private string namaUser;
        public FormCustomer(int idUser, string namaUser)
        {
            InitializeComponent();
            load();
            LoadComboBoxPengguna();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
            this.idUser = idUser;
            this.namaUser = namaUser;
            label1.Text = "Hi, " + namaUser;
        }


        private DataTable ambilData(string kategoriPengguna = null, string idKategori = null, string searchText = null)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            // Query dasar untuk mengambil data barang
            string query = "SELECT b.*, p.nama 'pengguna', CASE WHEN NOW() BETWEEN diskon_start AND diskon_end THEN b.diskon ELSE 0 END AS jumlah_diskon FROM barang b " +
                           "JOIN kategori k ON b.id_kategori = k.id " +
                           "JOIN pengguna p ON k.id_pengguna = p.id";

            // Jika kategori pengguna tidak null, tambahkan kondisi WHERE untuk kategori pengguna
            if (!string.IsNullOrEmpty(kategoriPengguna))
            {
                query += $" WHERE p.nama = '{kategoriPengguna}'";
            }

            // Jika idKategori tidak null, tambahkan kondisi WHERE untuk id_kategori
            if (!string.IsNullOrEmpty(idKategori))
            {
                if (query.Contains("WHERE"))
                {
                    query += $" AND b.id_kategori = '{idKategori}'";  // Filter berdasarkan id_kategori
                }
                else
                {
                    query += $" WHERE b.id_kategori = '{idKategori}'";  // Filter berdasarkan id_kategori
                }
            }

            // Jika ada teks pencarian, tambahkan kondisi WHERE untuk mencari nama barang yang mengandung teks
            if (!string.IsNullOrEmpty(searchText))
            {
                if (query.Contains("WHERE"))
                {
                    query += $" AND b.nama LIKE '%{searchText}%'";
                }
                else
                {
                    query += $" WHERE b.nama LIKE '%{searchText}%'";
                }
            }

            query += " ORDER BY b.id";  // Urutkan barang berdasarkan id

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }


        private void load()
        {
            panel1.Controls.Clear();

            string kategoriPengguna = null;
            string idKategori = null;
            string searchText = textBox1.Text;  // Ambil teks pencarian dari TextBox

            // Jika ComboBox1 dipilih, ambil kategori pengguna dari ComboBox1
            if (comboBox1.SelectedItem != null)
            {
                var selectedItem = comboBox1.SelectedItem as DataRowView;
                kategoriPengguna = selectedItem["nama"].ToString();  // Ambil id_pengguna dari ComboBox1
            }

            // Jika ComboBox2 dipilih, ambil id kategori dari ComboBox2
            if (comboBox2.SelectedItem != null)
            {
                var selectedItemKategori = comboBox2.SelectedItem as DataRowView;
                idKategori = selectedItemKategori["id"].ToString();  // Ambil id_kategori dari ComboBox2
            }

            // Ambil data barang berdasarkan kategori pengguna, id kategori, dan teks pencarian
            DataTable dt = ambilData(kategoriPengguna, idKategori, searchText);

            // Display data in the panel
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PictureBox pb = new PictureBox()
                {
                    Enabled = false,
                    Location = new System.Drawing.Point(19, 1),
                    Margin = new System.Windows.Forms.Padding(1),
                    Name = "picture",
                    Size = new System.Drawing.Size(153, 192),
                    TabIndex = 37,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    TabStop = false
                };
                pb.Load(dt.Rows[i]["url_gambar"].ToString());

                Label pengguna = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(16, 218),
                    Margin = new System.Windows.Forms.Padding(1, 0, 1, 0),
                    Name = "labelPengguna",
                    Size = new System.Drawing.Size(41, 13),
                    TabIndex = 37,
                    Text = dt.Rows[i]["pengguna"].ToString()
                };

                
                Label ukuran = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(130, 218),
                    Margin = new System.Windows.Forms.Padding(1, 0, 1, 0),
                    Name = "labelUkuran",
                    Size = new System.Drawing.Size(55, 13),
                    TabIndex = 37,
                };

                if (dt.Rows[i]["stok_nosize"].ToString() == "-1")
                {
                    ukuran.Text = "XS - 2XL";
                }
                else
                {
                    ukuran.Text = "No Size";
                }

                Label nama = new Label()
                {
                    AutoSize = false,
                    Location = new System.Drawing.Point(16, 244),
                    Margin = new System.Windows.Forms.Padding(1, 0, 1, 0),
                    Name = "labelNama",
                    Size = new System.Drawing.Size(138, 26),
                    TabIndex = 37,
                    Text = dt.Rows[i]["nama"].ToString()
                };

                int angka = int.Parse(dt.Rows[i]["harga"].ToString());
                Label harga = new Label()
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(16, 283),
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 10F),
                    Margin = new System.Windows.Forms.Padding(1, 0, 1, 0),
                    Name = "labelHarga",
                    Size = new System.Drawing.Size(60, 13),
                    TabIndex = 37,
                    Text = $"Rp{angka:N0}"  // Format price as Indonesian currency
                };

                Panel p = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaption,
                    Location = new System.Drawing.Point(19 + ((i % 4) * 220), 13 + ((i / 4) * 347)),
                    Name = "panel2",
                    Size = new System.Drawing.Size(200, 327),
                    TabIndex = 36
                };
                string id = dt.Rows[i]["id"].ToString();
                int diskon = int.Parse(dt.Rows[i]["jumlah_diskon"].ToString());
                if (diskon != 0)
                {
                    Label hargaAkhir = new Label()
                    {
                        AutoSize = true,
                        Location = new System.Drawing.Point(100, 283),
                        Margin = new System.Windows.Forms.Padding(1, 0, 1, 0),
                        Font = new System.Drawing.Font("Microsoft Sans Serif", 10F),
                        Name = "labelHargaAkhir",
                        Size = new System.Drawing.Size(65, 13),
                        TabIndex = 37,
                        ForeColor = Color.Red,
                        Text = $"Rp{angka - diskon:N0}"  // Format price as Indonesian currency
                    };
                    harga.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, (byte)0);
                    p.Controls.Add(hargaAkhir);
                    hargaAkhir.Click += (sender, e) =>
                    {
                        this.Hide();
                        FormDetailBarang f = new FormDetailBarang(id, idUser);
                        f.ShowDialog();
                        this.Show();
                    };
                }
                
                p.MouseEnter += (sender, e) => { p.Cursor = Cursors.Hand; };
                p.MouseLeave += (sender, e) => { p.Cursor = Cursors.Default; };
                p.Click += (sender, e) =>
                {
                    this.Hide();
                    FormDetailBarang f = new FormDetailBarang(id, idUser);
                    f.ShowDialog();
                    this.Show();
                };

                pengguna.Click += (sender, e) =>
                {
                    this.Hide();
                    FormDetailBarang f = new FormDetailBarang(id, idUser);
                    f.ShowDialog();
                    this.Show();
                };

                harga.Click += (sender, e) =>
                {
                    this.Hide();
                    FormDetailBarang f = new FormDetailBarang(id, idUser);
                    f.ShowDialog();
                    this.Show();
                };

                nama.Click += (sender, e) =>
                {
                    this.Hide();
                    FormDetailBarang f = new FormDetailBarang(id, idUser);
                    f.ShowDialog();
                    this.Show();
                };

                p.Controls.Add(harga);
                p.Controls.Add(nama);
                p.Controls.Add(ukuran);
                p.Controls.Add(pengguna);
                p.Controls.Add(pb);
                panel1.Controls.Add(p);
                panel1.AutoScroll = true;
            }
        }


        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void FormCashier_Load(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FormCart f = new FormCart(idUser);
            f.ShowDialog();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadComboBoxPengguna()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from pengguna", conn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "nama";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            load();
            comboBox2.DataSource = null;
            comboBox2.SelectedIndex = -1;  // Reset selected index

            if (comboBox1.SelectedItem != null)
            {
                // Get selected value from ComboBox1
                var selectedItem = comboBox1.SelectedItem as DataRowView;
                if (selectedItem != null)
                {
                    string idPengguna = selectedItem["id"].ToString();  // Get the id of the selected pengguna

                    // Fetch new data for ComboBox2 based on ComboBox1 selection
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM kategori WHERE id_pengguna = @a", conn);
                    cmd.Parameters.AddWithValue("@a", idPengguna);  // Use id_pengguna from ComboBox1 selection
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind the result to ComboBox2
                    comboBox2.DataSource = dt;
                    comboBox2.DisplayMember = "nama";
                    comboBox2.ValueMember = "id";
                }
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}
