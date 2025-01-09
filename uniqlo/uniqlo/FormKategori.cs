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
    public partial class FormKategori : Form
    {
        string connectionString = "server=192.168.0.23;uid=root;pwd=;database=db_uniqlo";

        public FormKategori()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from pengguna", conn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "nama";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex==-1 || textBox1.Text == "")
            {
                MessageBox.Show("Masih ada yang kosong!");
            }
            else
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select count(*) from kategori where nama= @a and id_pengguna= @b", conn);
                cmd.Parameters.AddWithValue("@a", textBox1.Text);
                cmd.Parameters.AddWithValue("@b", comboBox1.SelectedValue);
                int ada = Convert.ToInt32(cmd.ExecuteScalar());

                if (ada != 0)
                {
                    MessageBox.Show("Kategori sudah ada!");
                }
                else
                {
                    cmd = new MySqlCommand("INSERT INTO `db_uniqlo`.`kategori` (`nama`, `id_pengguna`) VALUES ( @a , @b );", conn);
                    cmd.Parameters.AddWithValue("@a", textBox1.Text);
                    cmd.Parameters.AddWithValue("@b", comboBox1.SelectedValue);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kategori berhasil ditambahkan!");
                }
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
