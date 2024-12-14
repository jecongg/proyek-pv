using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uniqlo
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void btnAddBrg_Click(object sender, EventArgs e)
        {
            FormAddBarang fa = new FormAddBarang();
            fa.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        public void addBrg()
        {

        }
    }
}
