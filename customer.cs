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
    public partial class customer : Form
    {
        public customer()
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
        private void btnback_Click(object sender, EventArgs e)
        {

        }

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
                    MessageBox.Show("Records Added");
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

        private void cmbname_SelectionChangeCommitted(object sender, EventArgs e)
        {
            getstudentname();
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
                    SqlCommand cmd = new SqlCommand("update ctbl set  pname = @pname, cname= @sname,add=@sdob,phone=@sst  where id=@sid", con);
                    cmd.Parameters.AddWithValue("@sid", cmbid.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@pname", txtname.Text);
                    cmd.Parameters.AddWithValue("@sname", txtcname.Text);
                    cmd.Parameters.AddWithValue("@sdob", txtadd.Text);
                    cmd.Parameters.AddWithValue("@sst", txtn.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("customer information update");
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
                    SqlCommand cmd = new SqlCommand("delete from ctbl where cname ='" + txtcname.Text + "' ", con);
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

        private void customer_Load(object sender, EventArgs e)
        {

        }
    }
}
