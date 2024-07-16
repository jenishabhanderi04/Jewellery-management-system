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
    public partial class product : Form
    {
        public product()
        {
            InitializeComponent();
            displayproduct();
            Reset();

        }
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-0GE4MF2C\SQLEXPRESS;Initial Catalog=student;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        private void displayproduct()
        {
            con.Open();
            string Query = "select * from producttbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void Reset()
        {
            txtpid.Text = "";
            txtprice.Text = "";
            txtt.Text = "";
            cmbname.SelectedIndex = 0;
            cmbdis.SelectedIndex = 0;
            txtqty.Text = "";
            txtpay.Text = "";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        int qty;
        int discount, total, price, pay;
        int Key = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            /* txtpid.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
             cmbname.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
             txtqty.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
             cmbdis.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
             txtpay.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
             txtt.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
             txtprice.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();*/

            /*try
            {
                MessageBox.Show(dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + dataGridView1.SelectedRows[0].Cells[1].Value.ToString());

                dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtpid.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmbname.SelectedItem = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtqty.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtprice.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

                txtpay.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                cmbdis.SelectedItem = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + "\n\n\n\n" + error.StackTrace);
            }


            if (txtpid.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }*/
              if(e.RowIndex > 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];


                txtpid.Text = row.Cells["pid"].Value.ToString();
                cmbname.Text = row.Cells["pid"].Value.ToString();
                txtqty.Text = row.Cells["pid"].Value.ToString();
                txtprice.Text = row.Cells["pid"].Value.ToString();
                txtt.Text = row.Cells["pid"].Value.ToString();
                txtpay.Text = row.Cells["pid"].Value.ToString();
                cmbdis.Text = row.Cells["pid"].Value.ToString();
            }




        }



        private void btnedit_Click(object sender, EventArgs e)
        {
            if (txtpid.Text == "" || txtprice.Text == "" || txtqty.Text == "" || cmbname.SelectedIndex == -1 || cmbdis.SelectedIndex == -1 || txtt.Text == "" || txtpay.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update producttbl set pname=@pname,qty=@qty,total=@total,payment=@payment,discount=@discount ,price = @price  where pid=@pid", con);
                    cmd.Parameters.AddWithValue("@pid", txtpid.Text);
                    cmd.Parameters.AddWithValue("@pname", cmbname.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@qty", txtqty.Text);
                    cmd.Parameters.AddWithValue("@discount", cmbdis.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@price", txtprice.Text);
                    cmd.Parameters.AddWithValue("@total", txtt.Text);
                    cmd.Parameters.AddWithValue("@payment", txtpay.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("product update");
                    con.Close();
                    displayproduct();
                    Reset();
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
            cmd = new SqlCommand("select * from producttbl where pid='" + txtpid.Text + "';", con);
          SqlDataReader  dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                cmbname.Text = dr.GetValue(1).ToString();

                txtqty.Text = dr.GetValue(2).ToString();
                txtprice.Text = dr.GetValue(3).ToString();
                txtt.Text = dr.GetValue(4).ToString();
                txtpay.Text = dr.GetValue(5).ToString();
                cmbdis.Text = dr.GetValue(6).ToString();
            }
            con.Close();
            //sda.Fill(dt);
        //    dataGridView1.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            new form1().Show();
            this.Hide();
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            form1 mn = new form1();
            mn.Show();
            this.Hide();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //code for total
            decimal total=0;
            total=Convert.ToDecimal(txtqty.Text)*Convert.ToDecimal(txtprice.Text);
            txtt.Text = Convert.ToString(total);
            int id;
            decimal discount = 10;
            // MessageBox.Show(Convert.ToString(cmbdis.SelectedIndex));
            txtpay.Text = Convert.ToString(discount);

            //dicount code
            if (cmbdis.SelectedIndex==0)
            {
                discount = total * 1;
                txtpay.Text = Convert.ToString(discount);
            }
            else if(cmbdis.SelectedIndex==1)
            {
                discount = total * 5 / 100;
                total -= discount;
                txtpay.Text = Convert.ToString(total);


            }
            //int row = 0;
            //     dataGridView1.Rows.Add();
            //   row = dataGridView1.Rows.Count - 2;

            string name;
            id = int.Parse(txtpid.Text);
            name = (cmbname.Text);
            //  discount = (cmbdis.SelectedIndex);
        /*    price = int.Parse(txtprice.Text);
            total = qty * price;
            txtt.Text = "" + total;*/
       //     discount = (cmbdis.SelectedIndex);
          /*  if (cmbdis.SelectedIndex == 1)

                discount = total * 5 / 100;

            else

                discount = 0;*/
         //   pay = total - discount;
          //  txtpay.Text = "" + pay;


            if (txtpid.Text == "" || txtprice.Text == "" || txtqty.Text == "" || cmbname.SelectedIndex == -1 || cmbdis.SelectedIndex == -1 || txtt.Text == "" || txtpay.Text == "")
            {
                MessageBox.Show("missing information");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into producttbl(pid,pname,qty,price,total,payment,discount) values(@pid,@pname,@qty,@price,@total,@payment,@discount)", con);
                    cmd.Parameters.AddWithValue("@pid", txtpid.Text);
                    cmd.Parameters.AddWithValue("@pname", cmbname.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@qty", txtqty.Text);
                    cmd.Parameters.AddWithValue("@discount", cmbdis.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@price", txtprice.Text);
                    cmd.Parameters.AddWithValue("@total", txtt.Text);
                    cmd.Parameters.AddWithValue("@payment", txtpay.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("product added");
                    con.Close();
                    displayproduct();
                    Reset();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }





            /*  dataGridView1["pid" ,row].Value = txtpid.Text;
              dataGridView1["pname", row].Value = cmbname.Text;
              dataGridView1["qty", row].Value = txtqty.Text;
              dataGridView1["price", row].Value = txtprice.Text;
              dataGridView1["total", row].Value = txtt.Text;
              dataGridView1["paymnent", row].Value = txtpay.Text;
              dataGridView1["discount", row].Value = discount;
            */


        }

        private void product_Load(object sender, EventArgs e)
        {

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (Key != 0)
            {
                MessageBox.Show("select student");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from producttbl where pid = '" + txtpid.Text + "'", con);
                    //cmd.Parameters.AddWithValue("@stkey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("student deleted");
                    con.Close();
                    displayproduct();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
