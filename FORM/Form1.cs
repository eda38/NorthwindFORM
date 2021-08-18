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
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }
        public SqlConnection Connect()
        {
            connection = new SqlConnection("Server=.; Database = Northwind; Integrated Security=True");            
            return connection;
        }
        private void customersToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            Form3 form3 = new Form3(); //This is the form that will open
            form3.MdiParent = this.MdiParent; //We give this form as parent.
            form3.Show(); //form 3 opens.           
        }
        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(); 
            form2.MdiParent = this.MdiParent; 
            form2.Show(); 
        }
        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.MdiParent = this.MdiParent;
            form4.Show();
        }
        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.MdiParent = this.MdiParent;
            form5.Show();
        }
    }
}
