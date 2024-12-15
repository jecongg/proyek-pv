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
    public partial class FormCashier : Form
    {
        public FormCashier()
        {
            InitializeComponent();
            label8.Text = "Rp639.000";
            label8.Font = new Font("Arial", 8, FontStyle.Strikeout | FontStyle.Bold);
        }
        
        private void load()
        {
            
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormCashier_Load(object sender, EventArgs e)
        {
            string imageUrl = "https://brandslogos.com/wp-content/uploads/images/large/uniqlo-logo.png";
            string imageItem1 = "https://image.uniqlo.com/UQ/ST3/us/imagesgoods/462369/item/usgoods_15_462369.jpg";
            string imageItem2 = "https://image.uniqlo.com/UQ/ST3/WesternCommon/imagesgoods/433635/item/goods_23_433635.jpg?width=2000";
            string imageItem3 = "https://tse2.mm.bing.net/th?id=OIP.mspf-7Hwrvn8YNTmGDgVywHaHa&pid=Api&P=0&h=180";
            string imageItem4 = "https://image.uniqlo.com/UQ/ST3/WesternCommon/imagesgoods/460331/sub/goods_460331_sub14.jpg?width=600";
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(imageUrl);
                    byte[] imageData2 = client.DownloadData(imageItem1);
                    byte[] imageData3 = client.DownloadData(imageItem2);
                    byte[] imageData4 = client.DownloadData(imageItem3);
                    byte[] imageData5 = client.DownloadData(imageItem4);

                    using (var stream = new System.IO.MemoryStream(imageData))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    using (var stream = new System.IO.MemoryStream(imageData2))
                    {
                        pictureBox2.Image = Image.FromStream(stream);
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    using (var stream = new System.IO.MemoryStream(imageData3))
                    {
                        pictureBox3.Image = Image.FromStream(stream);
                        pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    using (var stream = new System.IO.MemoryStream(imageData4))
                    {
                        pictureBox4.Image = Image.FromStream(stream);
                        pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                    using (var stream = new System.IO.MemoryStream(imageData5))
                    {
                        pictureBox5.Image = Image.FromStream(stream);
                        pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
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

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            FormDetailBarang f = new FormDetailBarang();
            f.ShowDialog();
            this.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDetailBarang f = new FormDetailBarang();
            f.ShowDialog();
            this.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDetailBarang f = new FormDetailBarang();
            f.ShowDialog();
            this.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormDetailBarang f = new FormDetailBarang();
            f.ShowDialog();
            this.Show();
        }
    }
}
