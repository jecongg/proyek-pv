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

namespace uniqlo
{
    public partial class FormCetakLaporan : Form
    {
        public FormCetakLaporan()
        {
            InitializeComponent();
            loadLaporan();
        }

        private void loadLaporan()
        {
            try
            {
                LaporanUniqlo laporan = new LaporanUniqlo(); // LaporanPenjualan.rpt
                crystalReportViewer1.ReportSource = laporan;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormCetakLaporan_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

}
