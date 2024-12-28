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
    public partial class FormCashier : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";

        public FormCashier()
        {
            InitializeComponent();
            load();
        }

        private DataTable ambilData()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("select *, p.nama 'pengguna' from barang b join kategori k on b.id_kategori=k.id join pengguna p on k.id_pengguna=p.id", conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        private void load()
        {
            DataTable dt = ambilData();
            for(int i=0; i<dt.Rows.Count; i++)
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
                    Text = "2XS - 2XL"
                    //text size belum sesuai data
                };
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
                    Margin = new System.Windows.Forms.Padding(1, 0, 1, 0),
                    Name = "labelHarga",
                    Size = new System.Drawing.Size(60, 13),
                    TabIndex = 37,
                    //ini buat format rupiah
                    Text = $"Rp{angka:N0}"
                };
                Panel p = new Panel()
                {
                    BackColor = System.Drawing.SystemColors.ActiveCaption,
                    Location = new System.Drawing.Point(19+((i%4)*220), 13+((i/4)*347)),
                    Name = "panel2",
                    Size = new System.Drawing.Size(200, 327),
                    TabIndex = 36
                };
                //foreach (Control control in p.Controls)
                //{
                //    control.MouseEnter += (sender, e) => { p.Cursor = Cursors.Hand; };
                //    control.MouseLeave += (sender, e) => { p.Cursor = Cursors.Default; };
                //}
                p.MouseEnter += (sender, e) => { p.Cursor = Cursors.Hand; };
                p.MouseLeave += (sender, e) => { p.Cursor = Cursors.Default; };
                p.Click += click;
                pengguna.Click += click;
                harga.Click += click;
                nama.Click += click;
                ukuran.Click += click;
                p.Controls.Add(harga);
                p.Controls.Add(nama);
                p.Controls.Add(ukuran);
                p.Controls.Add(pengguna);
                p.Controls.Add(pb);
                panel1.Controls.Add(p);
                panel1.AutoScroll = true;
            }
            
        }

        private void click(object sender, EventArgs e)
        {
            this.Hide();
            FormDetailBarang f = new FormDetailBarang();
            f.ShowDialog();
            this.Show();
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
            FormCart f = new FormCart();
            f.ShowDialog();
            this.Show();
        }
    }
}
