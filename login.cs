using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace jewellary
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-0GE4MF2C\SQLEXPRESS;Initial Catalog=student;Integrated Security=True");
     ////   public login();

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string selectCommand = "select  count(Username) as Total from logintbl where Username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "'";
                SqlCommand comm = new SqlCommand(selectCommand, con);
                con.Open();
                int rows = (int)comm.ExecuteScalar();

                con.Close();
                if (rows == 1)
                {
                   new form1().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("invalid password or username");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("error");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
