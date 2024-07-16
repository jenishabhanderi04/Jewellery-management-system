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
    public partial class newcustomer: Form
    {
        public newcustomer()
        {
            InitializeComponent();
            displaycustomer();
            Reset();
            fill();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-0GE4MF2C\SQLEXPRESS;Initial Catalog=student;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void displaycustomer()
        {
            con.Open();
            string Query = "select * from ctbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void fill()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Select pid from producttbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("pid", typeof(int));
            dt.Load(rdr);
            cmbid.ValueMember = "pid";
            cmbid.DataSource = dt;
            con.Close();

        }
        private void getstudentname()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from producttbl where pid=@pid", con);
            cmd.Parameters.AddWithValue("@pid", cmbid.SelectedValue.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtname.Text = dr["pname"].ToString();
            }
            con.Close();
        }
        private void Reset()
        {
            txtname.Text = "";
            //cmbst.SelectedIndex = -1;
            cmbid.SelectedIndex = -1;
            txtadd.Text = "";
            txtcname.Text = "";
            txtn.Text = "";
        }
        int Key = 0;
        private void btnadd_Click(object sender, EventArgs e)
        {
            if (txtname.Text == "" || cmbid.SelectedIndex == -1 || txtadd.Text == "" || txtcname.Text == "" || txtn.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ctbl (id,pname,cname,address,phone) values(@sid,@sname,@sdob,@sst,@phone)", con);
                    cmd.Parameters.AddWithValue("@sid", cmbid.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@sname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sdob", txtcname.Text);
                    cmd.Parameters.AddWithValue("@sst", txtadd.Text);
                    cmd.Parameters.AddWithValue("@phone", txtn.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information Added ");
                    con.Close();
                    displaycustomer();
                    Reset();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }

        }

        private void btnedit_Click(object sender, EventArgs e)
        {

            if (txtname.Text == "" || cmbid.SelectedIndex == -1 || txtadd.Text == "" || txtcname.Text == "" || txtn.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update ctbl set  pname = @pname, cname= @sname,address=@sdob,phone=@sst  where id=@sid", con);
                    cmd.Parameters.AddWithValue("@sid", cmbid.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@pname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sname", txtcname.Text);
                    cmd.Parameters.AddWithValue("@sdob", txtadd.Text);
                    cmd.Parameters.AddWithValue("@sst", txtn.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("information updated");
                    con.Close();
                    displaycustomer();
                    Reset();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           // getstudentname();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {

            if (Key != 0)
            {
                MessageBox.Show("select teacher");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from ctbl where id ='" + cmbid.Text + "' ", con);
                    //cmd.Parameters.AddWithValue("@stkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("record deleted");
                    con.Close();
                    displaycustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            cmd = new SqlCommand("select * from ctbl where id='" + cmbid.Text + "';", con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                rdr.Read();
                txtname.Text = rdr.GetValue(1).ToString();

                txtcname.Text = rdr.GetValue(2).ToString();
                txtadd.Text = rdr.GetValue(3).ToString();
                txtn.Text = rdr.GetValue(4).ToString();
              //  txtpay.Text = dr.GetValue(5).ToString();
               // cmbdis.Text = dr.GetValue(6).ToString();
            }
            con.Close();
            //sda.Fill(dt);
            //    dataGridView1.DataSource = dt;
        }

        private void cmbid_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getstudentname();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new form1().Show();
            this.Hide();
        }
    }
}
