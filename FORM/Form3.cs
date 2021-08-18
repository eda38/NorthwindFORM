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

namespace FORM
{
    public partial class Form3 : Form
    {
        public Form3() // CUSTOMER FORM CODES
        {
            InitializeComponent();
            this.IsMdiContainer = true;// Set the IsMdiContainer property to true.
        }
        SqlConnection connection = new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");
        SqlDataAdapter adapt;
        DataTable dt;
        SqlCommand Comm;
        int i = 0;
        Form1 frm1 = new Form1();      
        public void Showdata(string data) // SHOW 
        {
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1
            SqlDataAdapter da = new SqlDataAdapter(data, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Showdata("Select* from Customers");
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)//SEARCH
        {   //SEARCHS ACCORDING TO CUSTOMERID
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1 // USING THE FUNCTION IN FORM1
            adapt = new SqlDataAdapter("select * from Customers where CustomerID like '%" + textBox1.Text + "%'", conn);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void button2_Click(object sender, EventArgs e)//ADD BUTTON
        {
            string query = "INSERT INTO Customers( CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) VALUES(@CustomerID,@CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax )";
            Comm = new SqlCommand(query, connection);
            Comm.Parameters.AddWithValue("@CustomerID", textBox11.Text);//ADDS THE VALUE COMES FROM TEXTBOX11 TO THE CUSTOMERID COLUMN
            Comm.Parameters.AddWithValue("@CompanyName", textBox2.Text);
            Comm.Parameters.AddWithValue("@ContactName", textBox3.Text);
            Comm.Parameters.AddWithValue("@ContactTitle", textBox4.Text);
            Comm.Parameters.AddWithValue("@Address", textBox5.Text);
            Comm.Parameters.AddWithValue("@City", textBox6.Text);
            Comm.Parameters.AddWithValue("@Region", textBox7.Text);
            Comm.Parameters.AddWithValue("@PostalCode", textBox8.Text);
            Comm.Parameters.AddWithValue("@Country", textBox9.Text);
            Comm.Parameters.AddWithValue("@Phone", textBox10.Text);
            Comm.Parameters.AddWithValue("@Fax", textBox12.Text);
            connection.Open();
            Comm.ExecuteNonQuery();
            connection.Close();
            Showdata("Select* from Customers");
        }
        private void button3_Click(object sender, EventArgs e)//CLEAR BUTTON
        {   //CLEANS INSIDE OF THE TEXTBOXES
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
            textBox11.Clear();
            textBox12.Clear();        
        }
        private void button6_Click(object sender, EventArgs e)//CLOSE BUTTON
        {
            Application.Exit();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //THE ROW THAT I PICK COMES TO THE INDEXED TEXTBOX
            textBox11.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox12.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }
        private void button4_Click(object sender, EventArgs e)//UPDATE BUTTON
        {   // WITH THE dataGridView1_CellContentClick WE TAKE THAT ROW TO THE TEXTBOXES 
            //AFTER WE MAKE THE REQUIRED CHANGES WITH THE UPDATE BUTTON CHANGES ADDED
            connection.Open();
            string Update = ("Update Customers Set CompanyName=@CompanyName, ContactTitle= @ContactTitle, Address=@Address, City=@City, Region=@Region, PostalCode=@PostalCode, Country=@Country, Phone=@Phone, Fax=@Fax Where CustomerID=@CustomerID" );
            SqlCommand Comm = new SqlCommand(Update, connection);
            Comm.Parameters.AddWithValue("@CompanyName", textBox2.Text);
            Comm.Parameters.AddWithValue("@ContactName", textBox3.Text);
            Comm.Parameters.AddWithValue("@ContactTitle", textBox4.Text);
            Comm.Parameters.AddWithValue("@Address", textBox5.Text);
            Comm.Parameters.AddWithValue("@City", textBox6.Text);
            Comm.Parameters.AddWithValue("@Region", textBox7.Text);
            Comm.Parameters.AddWithValue("@PostalCode", textBox8.Text);
            Comm.Parameters.AddWithValue("@Country", textBox9.Text);
            Comm.Parameters.AddWithValue("@Phone", textBox10.Text);
            Comm.Parameters.AddWithValue("@Fax", textBox12.Text);
            Comm.Parameters.AddWithValue("@CustomerID", dataGridView1.Rows[i].Cells[0].Value);
            Comm.ExecuteNonQuery();
            connection.Close();
            Showdata("Select* from Customers");
        }      
        public void Delete(string CustomerID)
        {   // DELETES ACCORDING TO CUSTOMERID
            connection.Open();
            string Delete = "Delete from Customers Where CustomerID = @CustomerID";
            SqlCommand Comm = new SqlCommand(Delete, connection);
            Comm.Parameters.AddWithValue("@CustomerID", CustomerID);
            Comm.ExecuteNonQuery();
            connection.Close();
        }
        private void button5_Click(object sender, EventArgs e)// DELETE BUTTON
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
               string CustomerID = Convert.ToString(drow.Cells[0].Value);
                Delete(CustomerID);
                Showdata("Select* from Customers");
            }
        }       
    }
}
