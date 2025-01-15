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
    public partial class FormRetur : Form
    {
        int idNota;
        public FormRetur()
        {
            InitializeComponent();
        }

        private void UpdateSummary()
        {
            try
            {
                int totalItems = 0;
                int subtotal = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);
                    int rowSubtotal = Convert.ToInt32(row.Cells["Subtotal"].Value);
                    totalItems += quantity;
                    subtotal += rowSubtotal;
                }

                double ppn = subtotal * 0.12; // PPN 12%
                label10.Text = $"Total Item(s): {totalItems}";
                label11.Text = $"Subtotal Produk: Rp{subtotal:n0}";
                label12.Text = $"Subtotal : Rp{subtotal:n0}";
                label13.Text = $"Termasuk PPN (12%) : Rp{ppn:n0}";
                label14.Text = $"Total Pesanan : Rp{subtotal:n0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan saat menghitung ringkasan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormRetur_Load(object sender, EventArgs e)
        {
            string imageUrl = "https://brandslogos.com/wp-content/uploads/images/large/uniqlo-logo.png";

            try
            {
                pictureBox1.Load(imageUrl);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out idNota))
            {
                MessageBox.Show("Harap masukkan ID Nota yang valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString);
            try
            {
                conn.Open();

                groupBox2.Visible = true;

                // Query untuk mengambil data
                MySqlCommand selectData = new MySqlCommand(
                    "SELECT d.id_barang 'ID', d.nama_barang 'Nama Barang', d.size 'Ukuran', d.harga 'Harga Awal', d.harga - d.diskon 'Harga Akhir', " +
                    "d.quantity 'Quantity', d.subtotal 'Subtotal', b.returable 'Returable' " +
                    "FROM d_trans d JOIN barang b ON b.id = d.id_barang WHERE id_htrans = @a", conn);
                selectData.Parameters.AddWithValue("@a", idNota);

                MySqlDataAdapter adapter = new MySqlDataAdapter(selectData);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                // Atur kolom ReadOnly
                dataGridView1.Columns["Quantity"].ReadOnly = false; // Hanya kolom Quantity yang bisa diedit
                dataGridView1.Columns["ID"].ReadOnly = true;
                dataGridView1.Columns["Nama Barang"].ReadOnly = true;
                dataGridView1.Columns["Harga Awal"].ReadOnly = true;
                dataGridView1.Columns["Harga Akhir"].ReadOnly = true;
                dataGridView1.Columns["Ukuran"].ReadOnly = true;
                dataGridView1.Columns["Subtotal"].ReadOnly = true;
                dataGridView1.Columns["Returable"].ReadOnly = true;

                // Tambahkan tombol "Retur" jika belum ada
                if (!dataGridView1.Columns.Contains("DeleteButton"))
                {
                    DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                    deleteButton.Name = "DeleteButton";
                    deleteButton.HeaderText = "Action";
                    deleteButton.Text = "Retur";
                    deleteButton.UseColumnTextForButtonValue = true;
                    deleteButton.ReadOnly = true;
                    dataGridView1.Columns.Add(deleteButton);
                }

                // Update summary total
                UpdateSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void reset()
        {
            groupBox2.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DeleteButton" && e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[e.ColumnIndex];

                // Pastikan nilai Returable valid sebelum konversi
                bool returable = row.Cells["Returable"].Value != null && Convert.ToBoolean(row.Cells["Returable"].Value);

                // Ubah style berdasarkan Returable
                if (!returable)
                {
                    cell.Style.ForeColor = Color.Gray; // Ubah warna teks
                    cell.Style.SelectionForeColor = Color.Gray; // Ubah warna teks saat dipilih
                }
                else
                {
                    cell.Style.ForeColor = Color.Black; // Warna default
                    cell.Style.SelectionForeColor = Color.Black;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "DeleteButton")
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                bool returable = Convert.ToBoolean(row.Cells["Returable"].Value);

                // Validasi apakah barang bisa diretur
                if (!returable)
                {
                    MessageBox.Show("Barang ini tidak dapat diretur.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string namaBarang = row.Cells["Nama Barang"].Value.ToString();
                int idBarang = Convert.ToInt32(row.Cells["ID"].Value);

                // Tampilkan kotak dialog konfirmasi
                DialogResult result = MessageBox.Show(
                    $"Apakah Anda yakin ingin meretur barang \"{namaBarang}\" (ID: {idBarang})?",
                    "Konfirmasi Retur",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // Jika pengguna memilih "Yes", lakukan proses retur


                    // Jika barang bisa diretur, jalankan logika retur
                    string size = dataGridView1.Rows[e.RowIndex].Cells["Ukuran"].Value.ToString();
                    int quantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value);

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(DatabaseConfig.ConnectionString))
                        {
                            conn.Open();

                            // Hapus item dari tabel d_cart
                            MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM d_trans WHERE id_htrans = @idHtrans AND id_barang = @idBarang AND size = @size", conn);
                            deleteCmd.Parameters.AddWithValue("@idHtrans", idNota);
                            deleteCmd.Parameters.AddWithValue("@idBarang", idBarang);
                            deleteCmd.Parameters.AddWithValue("@size", size);
                            deleteCmd.ExecuteNonQuery();

                            // Kembalikan stok barang
                            if (size == "NO")
                            {
                                MySqlCommand updateStockCmd = new MySqlCommand("UPDATE barang SET stok_nosize = stok_nosize + @quantity WHERE id = @idBarang", conn);
                                updateStockCmd.Parameters.AddWithValue("@quantity", quantity);
                                updateStockCmd.Parameters.AddWithValue("@idBarang", idBarang);
                                updateStockCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                MySqlCommand updateStockCmd = new MySqlCommand("UPDATE stok SET stok = stok + @quantity WHERE id_barang = @idBarang AND size = @size", conn);
                                updateStockCmd.Parameters.AddWithValue("@quantity", quantity);
                                updateStockCmd.Parameters.AddWithValue("@idBarang", idBarang);
                                updateStockCmd.Parameters.AddWithValue("@size", size);
                                updateStockCmd.ExecuteNonQuery();
                            }

                            // Hapus baris dari DataGridView
                            dataGridView1.Rows.RemoveAt(e.RowIndex);

                            MessageBox.Show($"Barang '{namaBarang}' dengan ukuran '{size}' telah dihapus dan stok diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reset();
            this.Hide();
            FormCetak form = new FormCetak(Convert.ToInt32(textBox1.Text));
            form.ShowDialog();
            this.Show();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {

        }
    }
}
