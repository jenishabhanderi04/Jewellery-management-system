using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jewellary
{
    public partial class form1 : Form
    {
        public form1()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            new login().Show();
            this.Hide();
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {

            new product().Show();
            this.Hide();
        }

        private void btncustomer_Click(object sender, EventArgs e)
        {
            new newcustomer().Show();
            this.Hide();
        }

        private void btnbill_Click(object sender, EventArgs e)
        {
            new gallary().Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
