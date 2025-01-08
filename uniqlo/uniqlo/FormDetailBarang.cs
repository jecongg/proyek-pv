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
using MySql.Data.MySqlClient;

namespace uniqlo
{
    public partial class FormDetailBarang : Form
    {
        private string idBarang;
        private int idUser, harga, diskon;
        string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
        public FormDetailBarang(string id, int idUser)
        {
            InitializeComponent();
            this.idBarang = id;
            this.idUser = idUser;
        }

        private void FormDetailBarang_Load(object sender, EventArgs e)
        {
            LoadDetailBarang();
        }

        private string selectedSize = null;

        private void LoadDetailBarang()
        {
            // Contoh: Ambil data barang dari database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT *, CASE WHEN NOW() BETWEEN diskon_start AND diskon_end THEN diskon ELSE 0 END AS jumlah_diskon FROM barang WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idBarang);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Contoh: Tampilkan data barang di kontrol
                        label7.Text = reader["nama"].ToString();
                        harga = int.Parse(reader["harga"].ToString());
                        diskon = int.Parse(reader["jumlah_diskon"].ToString());
                        labelHarga.Text = $"Rp{harga:N0}";
                        if (diskon != 0)
                        {
                            labelHarga.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            
                            labelHargaAkhir.Text = $"Rp{harga-diskon:N0}";
                        }
                        else
                        {
                            labelHargaAkhir.Visible = false;
                        }
                        pictureBox2.Load(reader["url_gambar"].ToString());
                        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                        label3.Text = "Kode Produk: " + reader["id"].ToString();
                        label4.Text = "Detail : " + reader["deskripsi"].ToString();

                        int stokNoSize = Convert.ToInt32(reader["stok_nosize"]);
                        if (stokNoSize != -1)
                        {
                            if (stokNoSize == 0)
                            {
                                labelStock.Text = "Out of stock";
                                button2.Enabled = false;
                            }
                            else
                            {
                                labelStock.Text = $"In Stock: {stokNoSize}";
                            }
                            panel1.Location = new Point(panel1.Location.X, 175);
                            panel1.Refresh();
                            numericUpDown1.Maximum = stokNoSize;
                            selectedSize = "NO";
                            groupBox1.Visible = false;
                        }
                        else
                        {
                            groupBox1.Visible = true;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedSize == null)
            {
                MessageBox.Show("Silakan pilih size terlebih dahulu!");
            }
            else if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Quantity tidak boleh 0!");
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                // Mengecek apakah sudah ada data di tabel `cart` untuk user tertentu
                MySqlCommand cmdCheck = new MySqlCommand("SELECT id FROM cart WHERE id_user = @a", conn);
                cmdCheck.Parameters.AddWithValue("@a", idUser);

                object result = cmdCheck.ExecuteScalar();
                int idCart;
                if (result == null)
                {
                    // Jika tidak ada, tambahkan row baru ke tabel `cart`
                    MySqlCommand cmdInsert = new MySqlCommand("INSERT INTO cart (id_user, created_at) VALUES (@a, @b)", conn);
                    cmdInsert.Parameters.AddWithValue("@a", idUser);
                    cmdInsert.Parameters.AddWithValue("@b", DateTime.Now); // Misalnya ingin menyimpan waktu pembuatan
                    cmdInsert.ExecuteNonQuery();

                    // Ambil ID dari row yang baru diinsert
                    MySqlCommand cmdGetId = new MySqlCommand("SELECT LAST_INSERT_ID()", conn);
                    idCart = Convert.ToInt32(cmdGetId.ExecuteScalar());
                }
                else
                {
                    // Jika sudah ada, ambil ID cart tersebut
                    idCart = Convert.ToInt32(result);
                }
                MySqlCommand checkDCart = new MySqlCommand("SELECT quantity FROM d_cart WHERE id_cart = @a AND id_barang = @b AND size = @c", conn);
                checkDCart.Parameters.AddWithValue("@a", idCart);
                checkDCart.Parameters.AddWithValue("@b", idBarang);
                checkDCart.Parameters.AddWithValue("@c", selectedSize);

                object existingQuantity = checkDCart.ExecuteScalar();

                if (existingQuantity != null)
                {
                    // Jika sudah ada, update quantity
                    int newQuantity = Convert.ToInt32(existingQuantity) + (int)numericUpDown1.Value;
                    decimal newSubtotal = (harga - diskon) * newQuantity;

                    MySqlCommand updateDCart = new MySqlCommand("UPDATE d_cart SET quantity = @c, subtotal = @d WHERE id_cart = @a AND id_barang = @b AND size = @e", conn);
                    updateDCart.Parameters.AddWithValue("@a", idCart);
                    updateDCart.Parameters.AddWithValue("@b", idBarang);
                    updateDCart.Parameters.AddWithValue("@c", newQuantity);
                    updateDCart.Parameters.AddWithValue("@d", newSubtotal);
                    updateDCart.Parameters.AddWithValue("@e", selectedSize);
                    updateDCart.ExecuteNonQuery();
                }
                else
                {
                    // Jika belum ada, lakukan insert
                    MySqlCommand insertDCart = new MySqlCommand("INSERT INTO d_cart (id_cart, id_barang, quantity, harga, diskon, subtotal, size) VALUES (@a, @b, @c, @d, @e, @f, @g)", conn);
                    insertDCart.Parameters.AddWithValue("@a", idCart);
                    insertDCart.Parameters.AddWithValue("@b", idBarang);
                    insertDCart.Parameters.AddWithValue("@c", numericUpDown1.Value);
                    insertDCart.Parameters.AddWithValue("@d", harga);
                    insertDCart.Parameters.AddWithValue("@e", diskon);
                    insertDCart.Parameters.AddWithValue("@f", (harga - diskon) * numericUpDown1.Value);
                    insertDCart.Parameters.AddWithValue("@g", selectedSize);
                    insertDCart.ExecuteNonQuery();
                }

                if (selectedSize != "NO")
                {
                    MySqlCommand updateStok = new MySqlCommand("update stok set stok = stok - @a where id_barang = @b and size = @c", conn);
                    updateStok.Parameters.AddWithValue("@a", numericUpDown1.Value);
                    updateStok.Parameters.AddWithValue("@b", idBarang);
                    updateStok.Parameters.AddWithValue("@c", selectedSize);
                    updateStok.ExecuteNonQuery();
                }
                else
                {
                    MySqlCommand updateStok = new MySqlCommand("update barang set stok_nosize = stok_nosize - @a where id = @b", conn);
                    updateStok.Parameters.AddWithValue("@a", numericUpDown1.Value);
                    updateStok.Parameters.AddWithValue("@b", idBarang);
                    updateStok.ExecuteNonQuery();
                }
                
                conn.Close();
                this.Dispose();
                MessageBox.Show("Barang berhasil ditambahkan ke cart!");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SetActiveButton(Button activeButton)
        {
            foreach (Control control in this.Controls)
            {
                ResetButtons(control);
            }

            activeButton.BackColor = Color.Black;
            activeButton.ForeColor = Color.White;
            activeButton.FlatStyle = FlatStyle.Flat;
            activeButton.FlatAppearance.BorderColor = Color.Black;
            activeButton.FlatAppearance.BorderSize = 2;
        }

        private void ResetButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = Color.White;
                    button.ForeColor = Color.Black;
                    button.FlatStyle = FlatStyle.Standard;
                }
                else
                {
                    ResetButtons(control);
                }
            }
        }

        private void aturTampilan(Button btn)
        {
            int stock = GetStockBySize(btn.Text);
            if (stock == 0)
            {
                labelStock.Text = "Out of stock";
                button2.Enabled = false;
            }
            else
            {
                labelStock.Text = $"In Stock: {stock}";
                button2.Enabled = true;
            }
            numericUpDown1.Maximum = stock;
            SetActiveButton(btn);
            selectedSize = btn.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            aturTampilan((Button)sender);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            if (!btn.Enabled)
            {
                e.Graphics.Clear(Color.LightGray); // Warna latar tombol
                TextRenderer.DrawText(
                    e.Graphics,
                    btn.Text,
                    btn.Font,
                    btn.ClientRectangle,
                    Color.DarkGray, // Warna teks
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private int GetStockBySize(string size)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT stok FROM stok WHERE id_barang = @id AND size = @size";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idBarang);
                cmd.Parameters.AddWithValue("@size", size);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}
