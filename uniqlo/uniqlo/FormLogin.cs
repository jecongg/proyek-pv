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

        private void clear()
        {
            textUsername.Text = "";
            textPassword.Text = "";
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
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
                    clear();
                }
                else if(textUsername.Text=="cashier" && textPassword.Text == "cashier")
                {
                    this.Hide();
                    FormCashier f = new FormCashier();
                    f.ShowDialog();
                    this.Show();
                    clear();
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
    }
}
