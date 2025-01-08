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
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "MMMM yyyy";
                dateTimePicker1.ShowUpDown = true;
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

            private void button2_Click(object sender, EventArgs e)
            {
            try
            {
                // Ambil nilai bulan dan tahun dari DateTimePicker
                DateTime selectedDate = dateTimePicker1.Value;
                int selectedMonth = selectedDate.Month;
                int selectedYear = selectedDate.Year;

                // Load laporan
                LaporanUniqlo laporan = new LaporanUniqlo();

                // Set parameter ke laporan
                laporan.SetParameterValue("month", selectedMonth);
                laporan.SetParameterValue("year", selectedYear);

                // Tampilkan laporan di viewer
                crystalReportViewer1.ReportSource = laporan;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        }

    }
