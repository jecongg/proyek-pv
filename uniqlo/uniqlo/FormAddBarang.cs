using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uniqlo
{
    public partial class FormAddBarang : Form
    {
        private FormAdmin fa = new FormAdmin();
        public FormAddBarang()
        {
            InitializeComponent();
        }

        private void btnAddImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp", // Filter hanya untuk file gambar
                Title = "Pilih Gambar Barang"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Mendapatkan path file yang dipilih
                string selectedImagePath = openFileDialog.FileName;

                // Menampilkan gambar di PictureBox
                pictureBox1.Image = Image.FromFile(selectedImagePath);

            }
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

        private void btnAddBarang_Click(object sender, EventArgs e)
        {
            Image gambar = pictureBox1.Image;
            string nama = textNama.Text;
            
            fa.addBrg();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                numDiskon.Enabled = true;
            }
            if (!radioButton1.Checked)
            {
                numDiskon.Enabled = false;
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
    }
}
