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
    public partial class FormDetailBarang : Form
    {
        public FormDetailBarang()
        {
            InitializeComponent();
        }

        private void FormDetailBarang_Load(object sender, EventArgs e)
        {
            string imageUrl = "https://brandslogos.com/wp-content/uploads/images/large/uniqlo-logo.png";
            string imageItem1 = "https://image.uniqlo.com/UQ/ST3/us/imagesgoods/462369/item/usgoods_15_462369.jpg";
            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = client.DownloadData(imageUrl);
                    byte[] imageData2 = client.DownloadData(imageItem1);

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

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            FormCart f = new FormCart();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
