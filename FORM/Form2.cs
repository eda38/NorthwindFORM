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
    public partial class Form2 : Form // PRODUCT FORM CODES
    {
        Form1 frm1 = new Form1(); //ACCESS TO FORM1      
        SqlConnection connection =  new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");
        SqlDataAdapter adapt;
        DataTable dt;
        SqlCommand Comm;
        int i = 0;
        public Form2()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Set the IsMdiContainer property to true.
        }      
        public void Showdata(string data) // SHOW 
        {
            SqlConnection conn = frm1.Connect();// USES THE FUNCTION IN FORM1
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(data, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();
        }
        public void Delete(string ProductName)
        {   //DELETING ACCORDING TO PRODUCTNAME
            connection.Open();
            string Delete = "Delete from Products Where ProductName = @ProductName";
            SqlCommand Comm = new SqlCommand(Delete, connection);
            Comm.Parameters.AddWithValue("@ProductName", ProductName);
            Comm.ExecuteNonQuery();
            connection.Close();                
        }
        private void button1_Click(object sender, EventArgs e)//PRODUCT INFORMATION SHOW BUTTON 
        {
            //SHOWS THE PRODUCTS TABLE
            Showdata("Select* from Products");
        }   
        private void textBox1_KeyUp(object sender, KeyEventArgs e) //SEARCH BUTTON
        {
            //SEARCHS ACCORDING TO PRODUCTID
            SqlConnection conn = frm1.Connect();// USES THE FUNCTION IN FORM1
            conn.Open();
            adapt = new SqlDataAdapter("select * from Products where ProductID like '%" + textBox1.Text + "%'", conn);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        private void button4_Click(object sender, EventArgs e)//CLEAR BUTTON
        {
            //CLEANS INSIDE OF THE TEXTBOXES
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
        }
        private void button5_Click(object sender, EventArgs e) // CLOSE BUTTON
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e) //ADD BUTTON
        {
            string query = "INSERT INTO Products( ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued ) VALUES( @ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)";
           
            Comm = new SqlCommand(query, connection);          
            Comm.Parameters.AddWithValue("@ProductName", textBox2.Text); //ADDS THE VALUE COMES FROM TEXTBOX2 TO THE PRODUCTNAME COLUMN
            Comm.Parameters.AddWithValue("@SupplierID", textBox3.Text);
            Comm.Parameters.AddWithValue("@CategoryID", textBox4.Text);
            Comm.Parameters.AddWithValue("@QuantityPerUnit", textBox5.Text);
            Comm.Parameters.AddWithValue("@UnitPrice", textBox6.Text);
            Comm.Parameters.AddWithValue("@UnitsInStock", textBox7.Text);
            Comm.Parameters.AddWithValue("@UnitsOnOrder", textBox8.Text);
            Comm.Parameters.AddWithValue("@ReorderLevel", textBox9.Text);
            Comm.Parameters.AddWithValue("@Discontinued", textBox10.Text);           
            connection.Open();
            Comm.ExecuteNonQuery();
            connection.Close();
            Showdata("Select* from Products");
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {   //THE ROW THAT I PICK COMES TO THE INDEXED TEXTBOX
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
        }
        private void button3_Click(object sender, EventArgs e)//UPDATE
        {
            // WITH THE dataGridView1_CellContentClick WE TAKE THAT ROW TO THE TEXTBOXES 
            //AFTER WE MAKE THE REQUIRED CHANGES WITH THE UPDATE BUTTON  CHANGES ADDED
            connection.Open();
            string Update = ("Update Products Set ProductName=@ProductName, QuantityPerUnit= @QuantityPerUnit, UnitPrice=@UnitPrice, UnitsInStock =@UnitsInStock, UnitsOnOrder= @UnitsOnOrder, ReorderLevel=@ReorderLevel, Discontinued =@Discontinued Where ProductID=@ProductID");
            SqlCommand Comm = new SqlCommand(Update, connection);
            Comm.Parameters.AddWithValue("@ProductName",textBox2.Text);                    
            Comm.Parameters.AddWithValue("@QuantityPerUnit", textBox5.Text);
            Comm.Parameters.AddWithValue("@UnitPrice", textBox6.Text);
            Comm.Parameters.AddWithValue("@UnitsInStock", textBox7.Text);
            Comm.Parameters.AddWithValue("@UnitsOnOrder", textBox8.Text);
            Comm.Parameters.AddWithValue("@ReorderLevel", textBox9.Text);
            Comm.Parameters.AddWithValue("@Discontinued", textBox10.Text);
            Comm.Parameters.AddWithValue("@ProductID" ,dataGridView1.Rows[i].Cells[0].Value);
            Comm.ExecuteNonQuery();
            connection.Close();
            Showdata("Select* from Products");
        }     
        private void button6_Click(object sender, EventArgs e) // DELETE BUTTON
        {
            foreach(DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                string ProductName = Convert.ToString(drow.Cells[1].Value);
                Delete(ProductName);
                Showdata("Select* from Products");
            }
        }
    }
}
