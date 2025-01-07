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
using System.Net;

namespace uniqlo
{
    public partial class FormCetak : Form
    {
        public FormCetak(int id_htrans)
        {
            InitializeComponent();
            loadNota(id_htrans);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void loadNota(int id_htrans)
        {
            try
            {
                NotaUniqlo nota = new NotaUniqlo(); // class nota dari Nota.rpt
                nota.SetParameterValue("ID_HTrans", id_htrans); // parameter nya dari file Nota.rpt
                crystalReportViewer1.ReportSource = nota;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormCetak_Load(object sender, EventArgs e)
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
