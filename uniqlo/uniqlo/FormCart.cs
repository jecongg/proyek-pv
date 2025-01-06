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
    public partial class FormCart : Form
    {
        private int idUser, idCart, previousQuantity;
        string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
        public FormCart(int idUser)
        {
            InitializeComponent();
            this.idUser = idUser;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Validasi jika cart kosong
                    MySqlCommand checkCart = new MySqlCommand("SELECT COUNT(*) FROM d_cart WHERE id_cart = @idCart", conn);
                    checkCart.Parameters.AddWithValue("@idCart", idCart);
                    int itemCount = Convert.ToInt32(checkCart.ExecuteScalar());
                    if (itemCount == 0)
                    {
                        MessageBox.Show("Cart kosong! Tambahkan item terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult result = MessageBox.Show("Apakah Anda yakin ingin melanjutkan ke pembayaran?",
                                          "Konfirmasi",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

                    // Jika pengguna memilih 'Yes'
                    if (result == DialogResult.Yes)
                    {
                        MySqlCommand updateCartStatus = new MySqlCommand("UPDATE cart SET status = 'process' WHERE id = @idCart", conn);
                        updateCartStatus.Parameters.AddWithValue("@idCart", idCart);
                        updateCartStatus.ExecuteNonQuery();
                    }
                    FormCart_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormCart_Load(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand selectIdCart = new MySqlCommand("select id from cart where id_user= @a", conn);
            selectIdCart.Parameters.AddWithValue("@a", idUser);
            idCart = Convert.ToInt32(selectIdCart.ExecuteScalar());

            MySqlCommand cekStatus = new MySqlCommand("SELECT status FROM cart WHERE id = @a", conn);
            cekStatus.Parameters.AddWithValue("@a", idCart);
            object result = cekStatus.ExecuteScalar();
            string status = "";
            if (result != null)
            {
                status = result.ToString();
            }

            if (status == "pending")
            {
                panel1.Visible = false;
                MySqlCommand selectData = new MySqlCommand("SELECT d.id_barang 'ID', b.nama 'Nama Barang', d.size 'Ukuran', d.harga 'Harga Awal', d.harga-d.diskon 'Harga Akhir', d.quantity 'Quantity', subtotal 'Subtotal' FROM d_cart d JOIN barang b ON b.id=d.id_barang WHERE id_cart = @a", conn);
                selectData.Parameters.AddWithValue("@a", idCart);
                MySqlDataAdapter adapter = new MySqlDataAdapter(selectData);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Quantity"].ReadOnly = false; // Izinkan edit hanya di kolom Quantity
                dataGridView1.Columns["ID"].ReadOnly = true; // Kolom lainnya read-only
                dataGridView1.Columns["Nama Barang"].ReadOnly = true;
                dataGridView1.Columns["Harga Awal"].ReadOnly = true;
                dataGridView1.Columns["Harga Akhir"].ReadOnly = true;
                dataGridView1.Columns["Ukuran"].ReadOnly = true;
                dataGridView1.Columns["Subtotal"].ReadOnly = true;
                if (!dataGridView1.Columns.Contains("Delete"))
                {
                    DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn
                    {
                        HeaderText = "Delete",
                        Name = "Delete",
                        Text = "Delete",
                        UseColumnTextForButtonValue = true
                    };
                    dataGridView1.Columns.Add(deleteButton);
                }
                UpdateSummary();
            }
            else if (status == "process")
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = true;
                label2.Text = "Pembayaran Cart Selesai!";
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridView1.Columns["Quantity"].HeaderCell.Style.BackColor = Color.LightBlue;
            dataGridView1.Columns["Quantity"].HeaderCell.ToolTipText = "Klik untuk mengedit kolom ini";
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Quantity"].Index) // Pastikan kolom Quantity yang diubah
            {
                int rowIndex = e.RowIndex;
                int newQuantity = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Quantity"].Value);
                if (newQuantity < 0)
                {
                    MessageBox.Show("Quantity tidak boleh bernilai negatif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity; 
                    return;
                }
                int difference = newQuantity - previousQuantity; // Hitung selisih

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        int idBarang = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["ID"].Value.ToString());
                        string size = dataGridView1.Rows[rowIndex].Cells["Ukuran"].Value.ToString();
                        // Cek jika Quantity bertambah
                        if (difference > 0)
                        {
                            int availableStock=0;
                            if (size == "NO")
                            {
                                // Cek stok di database
                                MySqlCommand cmdCheckStock = new MySqlCommand("SELECT stok_nosize FROM barang WHERE id = @a", conn);
                                cmdCheckStock.Parameters.AddWithValue("@a", idBarang);
                                availableStock = Convert.ToInt32(cmdCheckStock.ExecuteScalar());
                            }
                            else
                            {
                                MySqlCommand cmdCheckStock = new MySqlCommand("SELECT stok FROM stok WHERE id_barang = @a and size = @b", conn);
                                cmdCheckStock.Parameters.AddWithValue("@a", idBarang);
                                cmdCheckStock.Parameters.AddWithValue("@b", size);
                                availableStock = Convert.ToInt32(cmdCheckStock.ExecuteScalar());
                            }
                            
                            if (difference > availableStock) // Jika stok tidak mencukupi
                            {
                                MessageBox.Show($"Stok tidak mencukupi! Stok tersedia: {availableStock}.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                // Kembalikan nilai Quantity ke nilai sebelumnya
                                dataGridView1.Rows[rowIndex].Cells["Quantity"].Value = previousQuantity;
                                return;
                            }

                            // Update stok di tabel d_product
                            if (size == "NO")
                            {
                                MySqlCommand cmdUpdateStock = new MySqlCommand("UPDATE barang SET stok_nosize = stok_nosize - @difference WHERE id = @idBarang", conn);
                                cmdUpdateStock.Parameters.AddWithValue("@difference", difference);
                                cmdUpdateStock.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdUpdateStock.ExecuteNonQuery();
                            }
                            else
                            {
                                MySqlCommand cmdUpdateStock = new MySqlCommand("UPDATE stok SET stok = stok - @difference WHERE id_barang = @idBarang and size = @size", conn);
                                cmdUpdateStock.Parameters.AddWithValue("@difference", difference);
                                cmdUpdateStock.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdUpdateStock.Parameters.AddWithValue("@size", size);
                                cmdUpdateStock.ExecuteNonQuery();
                            }
                            
                        }
                        else if (difference < 0) // Jika Quantity berkurang
                        {
                            if (size == "NO")
                            {
                                MySqlCommand cmdUpdateStock = new MySqlCommand("UPDATE barang SET stok_nosize = stok_nosize + @difference WHERE id = @idBarang", conn);
                                cmdUpdateStock.Parameters.AddWithValue("@difference", Math.Abs(difference));
                                cmdUpdateStock.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdUpdateStock.ExecuteNonQuery();
                            }
                            else
                            {
                                MySqlCommand cmdUpdateStock = new MySqlCommand("UPDATE stok SET stok = stok + @difference WHERE id_barang = @idBarang and size = @size", conn);
                                cmdUpdateStock.Parameters.AddWithValue("@difference", Math.Abs(difference));
                                cmdUpdateStock.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdUpdateStock.Parameters.AddWithValue("@size", size);
                                cmdUpdateStock.ExecuteNonQuery();
                            }
                        }
                        int subtotal = newQuantity * Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Harga Akhir"].Value.ToString());
                        // Update quantity dan subtotal di d_cart
                        MySqlCommand cmdUpdateCart = new MySqlCommand("UPDATE d_cart SET quantity = @quantity, subtotal = @subtotal WHERE id_cart = @idCart and id_barang = @idBarang", conn);
                        cmdUpdateCart.Parameters.AddWithValue("@quantity", newQuantity);
                        cmdUpdateCart.Parameters.AddWithValue("@subtotal", subtotal);
                        cmdUpdateCart.Parameters.AddWithValue("@idCart", idCart);
                        cmdUpdateCart.Parameters.AddWithValue("@idBarang", idBarang);
                        cmdUpdateCart.ExecuteNonQuery();
                        dataGridView1.Rows[rowIndex].Cells["Subtotal"].Value = subtotal;
                        // Beri informasi ke user
                        MessageBox.Show("Quantity dan stok berhasil diperbarui.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                int idBarang = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                string namaBarang = dataGridView1.Rows[e.RowIndex].Cells["Nama Barang"].Value.ToString();
                string size = dataGridView1.Rows[e.RowIndex].Cells["Ukuran"].Value.ToString();
                int quantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value);

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        // Hapus item dari tabel d_cart
                        MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM d_cart WHERE id_cart = @idCart AND id_barang = @idBarang AND size = @size", conn);
                        deleteCmd.Parameters.AddWithValue("@idCart", idCart);
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

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin membatalkan pesanan",
                                          "Konfirmasi Pembatalan",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            // Jika pengguna memilih 'Yes'
            if (result == DialogResult.Yes)
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand updateCartStatus = new MySqlCommand("UPDATE cart SET status = 'pending' WHERE id = @idCart", conn);
                updateCartStatus.Parameters.AddWithValue("@idCart", idCart);
                updateCartStatus.ExecuteNonQuery();
                FormCart_Load(sender, e);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Quantity"].Index) // Pastikan kolom Quantity
            {
                int rowIndex = e.RowIndex;
                previousQuantity = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["Quantity"].Value);
            }
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
    }
}
