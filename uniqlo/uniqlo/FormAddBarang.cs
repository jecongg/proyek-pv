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
            fa.addBrg();
        }
    }
}
