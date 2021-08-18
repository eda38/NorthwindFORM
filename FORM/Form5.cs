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
using System.Data.OleDb;
using ClosedXML.Excel;

namespace FORM
{
    public partial class Form5 : Form
    {
        SqlConnection connection = new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");
        SqlDataAdapter adapt;
        DataTable dt;
        Form1 frm1 = new Form1();
        public Form5()
        {
            InitializeComponent();
            this.IsMdiContainer = true;// Set the IsMdiContainer property to true.
        }        
        public void Showdata(string data) // SHOW 
        {
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1
            SqlDataAdapter da = new SqlDataAdapter(data, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            f5dgv.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e) // SHOW BUTTON
        {   // SHOWS OrderInfo TABLE
            Showdata("Select* from OrderInfo");
        }
        private void button2_Click(object sender, EventArgs e) //SEARCH BUTTON
        {   // SEARCH ACCORDING TO PRODUCTID
            SqlConnection conn = frm1.Connect();// USING THE FUNCTION IN FORM1
            adapt = new SqlDataAdapter("select * from OrderInfo where ProductID like '%" + textBox1.Text + "%'", conn);
            dt = new DataTable();
            adapt.Fill(dt);
            f5dgv.DataSource = dt;
            conn.Close();
        }
    }
}
