using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using MySql.Data.MySqlClient;

namespace uniqlo
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
            cmbRole.Items.Add("Admin");
           
            cmbRole.Items.Add("Cashier");
            cmbRole.Items.Add("Customer");
        }

       
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
            string username = textUsername.Text;
            string password = textPassword.Text;
            string nama = textNama.Text;
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Harap isi semua field.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO user (nama, username, password, role) VALUES (@nama ,@Username, @Password, @Role)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nama", nama);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", role);

                        command.ExecuteNonQuery();
                        //if (result > 0)
                        //{
                            MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textUsername.Clear();
                            textNama.Clear();
                            textPassword.Clear();
                            cmbRole.SelectedIndex = -1;
                            cmbRole.Text = "";
                            //FormLogin FormLogin = new FormLogin();
                            //FormLogin.Show();
                            this.Hide();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=db_uniqlo";
            string username = textUsername.Text;
            string password = textPassword.Text;
            string nama = textNama.Text;
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Harap isi semua field.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM user WHERE username = @Username";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Username", username);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Username sudah terdaftar, silakan gunakan username yang berbeda.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textUsername.Clear();
                            textPassword.Clear();
                            textNama.Clear();
                            cmbRole.SelectedIndex = -1;
                            cmbRole.Text = "";
                            return;
                        }
                    }

                    // Jika username belum ada, lakukan registrasi
                    string query = "INSERT INTO user (nama, username, password, role) VALUES (@Nama, @Username, @Password, @Role)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nama", nama);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", role);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        textUsername.Clear();
                        textPassword.Clear();
                        textNama.Clear();
                        cmbRole.SelectedIndex = -1;
                        cmbRole.Text = "";
                        // FormLogin FormLogin = new FormLogin();
                        // FormLogin.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void FormRegister_Load(object sender, EventArgs e)
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
