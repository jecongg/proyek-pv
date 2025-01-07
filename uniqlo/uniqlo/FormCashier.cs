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
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;


namespace uniqlo
{
    public partial class FormCashier : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
        int previousQuantity;
        int idCart;
        DataUniqlo dataUniqlo = new DataUniqlo();
        public FormCashier()
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

        private void FormCashier_Load(object sender, EventArgs e)
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
            idCart = Convert.ToInt32(textBox1.Text);
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cekStatus = new MySqlCommand("SELECT status FROM cart WHERE id = @a", conn);
            cekStatus.Parameters.AddWithValue("@a", idCart);
            object result = cekStatus.ExecuteScalar();
            string status = "";
            if (result != null)
            {
                status = result.ToString();
            }
            else
            {
                MessageBox.Show("Cart tidak ditemukan!");
                reset();
                return;
            }

            if (status == "process")
            {
                groupBox2.Visible = true;
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
            else
            {
                reset();
                MessageBox.Show("Status cart masih pending!");
            }
        }

        private void reset()
        {
            groupBox2.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
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
                reset();
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
                            int availableStock = 0;
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

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Harap masukkan ID Cart!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idCart = Convert.ToInt32(textBox1.Text);
            int idUser = 0; // Ambil ID user yang terkait dengan cart
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Ambil ID user dari tabel cart
                        MySqlCommand cmdGetUser = new MySqlCommand("SELECT id_user FROM cart WHERE id = @idCart", conn, transaction);
                        cmdGetUser.Parameters.AddWithValue("@idCart", idCart);
                        object result = cmdGetUser.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Cart tidak ditemukan!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        idUser = (int)result;

                        // Insert ke h_trans
                        MySqlCommand cmdInsertHTrans = new MySqlCommand(
                            "INSERT INTO h_trans (id_user, created_at) VALUES (@idUser, NOW())", conn, transaction);
                        cmdInsertHTrans.Parameters.AddWithValue("@idUser", idUser);
                        cmdInsertHTrans.ExecuteNonQuery();

                        // Ambil id_htrans terakhir
                        int idHTrans = (int)cmdInsertHTrans.LastInsertedId;

                        // Ambil data dari d_cart
                        MySqlCommand cmdGetDCart = new MySqlCommand(
                            "SELECT id_barang, quantity, harga, diskon, subtotal, size " +
                            "FROM d_cart WHERE id_cart = @idCart", conn, transaction);
                        cmdGetDCart.Parameters.AddWithValue("@idCart", idCart);

                        using (MySqlDataReader reader = cmdGetDCart.ExecuteReader())
                        {
                            var cartItems = new List<(int idBarang, int quantity, int harga, int diskon, int subtotal, string size)>();

                            while (reader.Read())
                            {
                                cartItems.Add((
                                    reader.GetInt32("id_barang"),
                                    reader.GetInt32("quantity"),
                                    reader.GetInt32("harga"),
                                    reader.GetInt32("diskon"),
                                    reader.GetInt32("subtotal"),
                                    reader.GetString("size")
                                ));
                            }

                            // Tutup DataReader
                            reader.Close();

                            foreach (var item in cartItems)
                            {
                                int idBarang = item.idBarang;

                                // Dapatkan nama barang dari tabel barang
                                string namaBarang = "";
                                MySqlCommand cmdGetBarang = new MySqlCommand(
                                    "SELECT nama FROM barang WHERE id = @idBarang", conn, transaction);
                                cmdGetBarang.Parameters.AddWithValue("@idBarang", idBarang);
                                object namaResult = cmdGetBarang.ExecuteScalar();
                                if (namaResult != null)
                                {
                                    namaBarang = namaResult.ToString();
                                }

                                // Insert ke d_trans
                                MySqlCommand cmdInsertDTrans = new MySqlCommand(
                                    "INSERT INTO d_trans (id_htrans, id_barang, nama_barang, quantity, harga, diskon, subtotal, size) " +
                                    "VALUES (@idHTrans, @idBarang, @namaBarang, @quantity, @harga, @diskon, @subtotal, @size)", conn, transaction);
                                cmdInsertDTrans.Parameters.AddWithValue("@idHTrans", idHTrans);
                                cmdInsertDTrans.Parameters.AddWithValue("@idBarang", idBarang);
                                cmdInsertDTrans.Parameters.AddWithValue("@namaBarang", namaBarang);
                                cmdInsertDTrans.Parameters.AddWithValue("@quantity", item.quantity);
                                cmdInsertDTrans.Parameters.AddWithValue("@harga", item.harga);
                                cmdInsertDTrans.Parameters.AddWithValue("@diskon", item.diskon);
                                cmdInsertDTrans.Parameters.AddWithValue("@subtotal", item.subtotal);
                                cmdInsertDTrans.Parameters.AddWithValue("@size", item.size);
                                cmdInsertDTrans.ExecuteNonQuery();
                            }
                        }


                        // Hapus data dari d_cart
                        MySqlCommand cmdDeleteDCart = new MySqlCommand(
                            "DELETE FROM d_cart WHERE id_cart = @idCart", conn, transaction);
                        cmdDeleteDCart.Parameters.AddWithValue("@idCart", idCart);
                        cmdDeleteDCart.ExecuteNonQuery();

                        // Hapus data dari cart
                        MySqlCommand cmdDeleteCart = new MySqlCommand(
                            "DELETE FROM cart WHERE id = @idCart", conn, transaction);
                        cmdDeleteCart.Parameters.AddWithValue("@idCart", idCart);
                        cmdDeleteCart.ExecuteNonQuery();

                        // Commit transaksi
                        transaction.Commit();

                        // Beri informasi ke user
                        MessageBox.Show("Data berhasil diproses!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                        this.Hide();
                        FormCetak form = new FormCetak(idHTrans);
                        form.ShowDialog();
                        this.Show();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaksi jika terjadi error
                        transaction.Rollback();
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
