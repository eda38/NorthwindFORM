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
using Microsoft;
using System.IO;
using System.Data.OleDb;

using Microsoft.Office.Interop.Excel;


namespace FORM
{
    public partial class Form4 : Form
    {   //SALES FORM CODES
        SqlDataAdapter adapt;
        System.Data.DataTable dt;
        SqlCommand Comm;
        SqlConnection connection = new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");
        Form1 frm1 = new Form1();
        public Form4()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Set the IsMdiContainer property to true.
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Dataset1.Products' table. You can move, or remove it, as needed.
            //this.productsTableAdapter1.Fill(this.Dataset1.Products);
            // TODO: This line of code loads data into the 'dataSet2.Employees' table. You can move, or remove it, as needed.
            this.employeesTableAdapter.Fill(this.dataSet2.Employees);
            // TODO: This line of code loads data into the 'dataSet1.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.dataSet1.Products);

            //CREATES LISTVIEW'S COLUMNS
            listView1.Columns.Add("EmployeeID", 75);
            listView1.Columns.Add("ProductID", 75);
            listView1.Columns.Add("ProductName", 120);
            listView1.Columns.Add("UnitPrice", 100);
            listView1.Columns.Add("Quantity", 75);
            listView1.Columns.Add("TOTAL", 75);
        }       
        public void Showdata(string data) // SHOW 
        {
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(data, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
       public void Showemployees(string data)
        {
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1
            SqlDataAdapter da = new SqlDataAdapter(data, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter ProductID")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Enter ProductID";
                textBox1.ForeColor = Color.Silver;
            }
        }
       private void button5_Click(object sender, EventArgs e)// SHOW PRODUCTS
        {
            Showdata("Select* from Products");
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)//SEARCH
        {   // SEARCHS ACCORDING TO PRODUCTID
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1
            adapt = new SqlDataAdapter("select * from Products where ProductID like '%" + textBox1.Text + "%'", conn);
            dt = new System.Data.DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)//CLEAR BUTTON
        {   // CLEARS TEXBOXES
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            numericUpDown1.Value = 0;
        }
        private void button6_Click(object sender, EventArgs e)//CLOSE BUTTON
        {
            System.Windows.Forms.Application.Exit();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   //THE ROW THAT I CLICK IN THE DATAGRIDVIEW1 COMES TO THE TEXTBOXES           
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   // EMPLOYEEID INFORMATION COMES TO THE TEXTBOX9
            textBox9.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }
        private void button7_Click(object sender, EventArgs e)// SHOW ASSISTANTS
        {
            Showemployees("Select* from Employees");
        }
        private void button1_Click(object sender, EventArgs e)
        {   // TRANSFERS DATA IN TEXTBOXSES TO LISTVIEW            
            int quantity = Convert.ToInt32(numericUpDown1.Value);
            double price = Convert.ToDouble(textBox6.Text);
            double total = price * quantity;
            textBox10.Text = total.ToString();
            string EmployeeID = " ", ProductID = " ", ProductName = " ", UnitPrice = " ", Quantity = " ", TOTAL = " ";
            EmployeeID = textBox9.Text;
            ProductID = textBox2.Text;
            ProductName = textBox3.Text;
            UnitPrice = textBox6.Text;
            Quantity = Convert.ToString(numericUpDown1.Value);
            TOTAL = textBox10.Text;
            String[] productInfo = { EmployeeID, ProductID, ProductName, UnitPrice, Quantity, TOTAL };
            bool UnitsInStock = false;
            //IF STOCK NUMBER IS 0 SHOWS WARNING            
            if (textBox7.Text == "0") 
            {
                UnitsInStock = true;
                MessageBox.Show(textBox7.Text + "OUT OF STOCK");
            }
            // IF ANY TEXTBOXES IS EMPTY SHOWS WARNING          
            if(textBox2.Text=="" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" )
            {               
                MessageBox.Show("Fill the empty boxes!!!!!!!!!!");                
             }
            else if  (UnitsInStock == false) 
            {
                ListViewItem list = new ListViewItem(productInfo);
                if (EmployeeID != " " && ProductID != "" && ProductName != "" && UnitPrice != "" && Quantity != "" && TOTAL != " ")
                {
                    listView1.Items.Add(list);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {   //CLEARS LISTVIEW
            listView1.Items.Clear();
        }
        public void button4_Click(object sender, EventArgs e) // BUY
        {        
            // IT ADDS THE PRODUCTS IN THE LISTVIEW (RECEIPT PART) TO MY OrderInfo TABLE
            if (listView1.Items.Count != 0)
            {
                string query = "insert into OrderInfo (EmployeeID,ProductID,ProductName,UnitPrice,Quantity,TOTAL) values(@EmployeeID,@ProductID,@ProductName,@UnitPrice,@Quantity,@TOTAL)";
                connection = new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");              
                foreach (ListViewItem info in listView1.Items)
                {                   
                    Comm = new SqlCommand(query, connection);
                    Comm.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(info.SubItems[0].Text));
                    Comm.Parameters.AddWithValue("@ProductID", info.SubItems[1].Text);
                    Comm.Parameters.AddWithValue("@ProductName", info.SubItems[2].Text);
                    Comm.Parameters.AddWithValue("@UnitPrice", Convert.ToDouble(info.SubItems[3].Text));
                    Comm.Parameters.AddWithValue("@Quantity", info.SubItems[4].Text);
                    Comm.Parameters.AddWithValue("@TOTAL", Convert.ToDouble(info.SubItems[5].Text));              
                    connection.Open();
                    Comm.ExecuteNonQuery();
                    int ID = Convert.ToInt32(info.SubItems[1].Text);
                    int Quantity= Convert.ToInt32(info.SubItems[4].Text);
                    //purchased items is deducted from stock
                    string query1 = " Update Products set UnitsInStock = UnitsInStock - " + Quantity + "where ProductId = " + ID;
                    Comm = new SqlCommand(query1, connection);
                    Comm.ExecuteNonQuery();
                    connection.Close();                            
                }
                Showdata("select*from Products");
                MessageBox.Show("Your order has been received.");
                    listView1.Items.Clear();                   
            }
            else
            {
                MessageBox.Show("Your order could not be received. Please check the products you put the basket .");
            }
        }
        private void button8_Click(object sender, EventArgs e) 
            // REMOVES PRODUCT, ROW THAT I PICK IN LISTVIEW1
        {
            {
                int number = listView1.SelectedItems.Count;
                foreach (ListViewItem del in listView1.SelectedItems)
                {
                    del.Remove();
                }
                MessageBox.Show(number + " records have been deleted.");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {  if(checkBox2.Checked== true)
                {
                    checkBox2.Checked = false;
                }
                Showdata("select*from Products where UnitsInStock>0");
            }
            else
            {
                Showdata("select*from Products");
            }               
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true )
            {   if (checkBox1.Checked == true)
                {
                    checkBox1.Checked = false;
                }
               Showdata("select*from Products where UnitsInStock=0");
            }
            else
            {
                Showdata("select*from Products");
            }
        }
    }
}

