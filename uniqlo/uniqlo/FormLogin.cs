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

namespace uniqlo
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //Method buat login, ada pengecekan inputan kosong, user yang bisa masuk hanya admin dan cashier
            if(textUsername.Text=="" || textPassword.Text == "")
            {
                MessageBox.Show("Masih kosong!");
            }
            else
            {
                if(textUsername.Text=="admin" && textPassword.Text == "admin")
                {
                    this.Hide();
                    FormAdmin f = new FormAdmin();
                    f.ShowDialog();
                    this.Show();
                }
                else if(textUsername.Text=="cashier" && textPassword.Text == "cashier")
                {
                    this.Hide();
                    FormCashier f = new FormCashier();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Username atau password salah!");
                    textUsername.Text = "";
                    textPassword.Text = "";
                }
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            string imageUrl = "https://brandslogos.com/wp-content/uploads/images/large/uniqlo-logo.png";

            try
            {
                // Menggunakan WebClient untuk mengunduh gambar
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(imageUrl);

                    // Konversi byte[] ke Image
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
    }
}
