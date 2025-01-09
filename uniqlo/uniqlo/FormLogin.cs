using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
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
    public partial class FormLogin : Form
    {
        string connectionString = "server=192.168.0.23;uid=root;pwd=;database=db_uniqlo";
        private int idUser;
        private string namaUser;
        public FormLogin()
        {
            InitializeComponent();
            clear();
        }

        private void clear()
        {
            textUsername.Text = "";
            textPassword.Text = "";
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string CekLogin(string username, string password)
        {
            string role = null;
            string hashedPassword = HashPassword(password);

            // Cek login ke database
            string query = "SELECT * FROM user WHERE username = @username AND password = @password";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())  // Using statement ensures reader is closed
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            role = reader["role"].ToString();
                            idUser = Convert.ToInt32(reader["id"].ToString());
                            namaUser = reader["nama"].ToString();
                        }
                    }
                }  
            }

            return role;  // Mengembalikan role
        }


        // Event buttonClick untuk login
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textUsername.Text;
            string password = textPassword.Text;

            textUsername.Text = "";
            textPassword.Text = "";

            // Memanggil CekLogin
            string role = CekLogin(username, password);
            if (role != null)
            {
                if (role == "Admin")
                {
                    this.Hide();
                    FormAdmin f = new FormAdmin();
                    f.ShowDialog();
                    this.Show();
                }
                else if (role == "Cashier")
                {
                    this.Hide();
                    FormCashier f = new FormCashier();
                    f.ShowDialog();
                    this.Show();
                }
                else if (role == "Customer")
                {
                    this.Hide();
                    FormCustomer f = new FormCustomer(idUser, namaUser);
                    f.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Username atau password salah!");
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
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

        private void label4_Click(object sender, EventArgs e)
        {
            FormRegister fr = new FormRegister();
            fr.ShowDialog();
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(sender, e);
            }
        }
    }
}
