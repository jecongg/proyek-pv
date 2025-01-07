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

namespace uniqlo
{
    public partial class FormCetak : Form
    {
        DataUniqlo dataUniqlo = new DataUniqlo();

        public FormCetak(int id_htrans)
        {
            InitializeComponent();
            loadNota(id_htrans);
        }

        private void loadData()
        {
            // Koneksi ke database
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";

            // Daftar nama tabel SQL yang sesuai dengan DataTable di DataSet
            string[] tableNames = { "barang", "cart", "d_cart", "d_trans", "h_trans", "kategori", "pengguna", "stok", "user" };

            try
            {
                // Nonaktifkan constraints sementara
                dataUniqlo.EnforceConstraints = false;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open(); // Membuka koneksi

                    foreach (string tableName in tableNames)
                    {
                        try
                        {
                            // Pastikan DataTable dengan nama tabel ada di dalam DataSet
                            if (dataUniqlo.Tables.Contains(tableName))
                            {
                                // Kosongkan DataTable agar data lama tidak tercampur
                                dataUniqlo.Tables[tableName].Clear();

                                // Query untuk mengambil data dari tabel SQL
                                string query = $"SELECT * FROM {tableName}";

                                // Gunakan MySqlDataAdapter untuk mengisi data
                                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                                {
                                    // Isi DataTable dengan data dari database
                                    adapter.Fill(dataUniqlo.Tables[tableName]);
                                }
                            }
                            else
                            {
                                MessageBox.Show($"DataTable '{tableName}' tidak ditemukan di dalam DataSet.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log tabel yang bermasalah
                            MessageBox.Show($"Error saat memproses tabel '{tableName}': {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                // Aktifkan kembali constraints
                dataUniqlo.EnforceConstraints = true;

                // Pesan sukses
                MessageBox.Show("Data dari SQL berhasil dipindahkan ke DataSet!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException sqlEx)
            {
                // Menangani error SQL
                MessageBox.Show($"SQL Error: {sqlEx.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Menangani error umum
                MessageBox.Show($"Error: {ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void loadNota(int id_htrans)
        {
            try
            {
                Nota nota = new Nota(); // class nota dari Nota.rpt
                nota.SetParameterValue("ID_HTrans", 9); // parameter nya dari file Nota.rpt
                crystalReportViewer1.ReportSource = nota;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
