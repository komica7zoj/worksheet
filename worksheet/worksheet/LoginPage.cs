using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace worksheet
{
    public partial class LoginPage : Form
    {
        Boolean isclose = true;
        LoadData database;
        int userId;
        public LoginPage(LoadData database)
        {
            InitializeComponent();
            this.database = database;
        }

        public Boolean isclosed()
        {
            return isclose;
        }

        public int getUserId()
        {
            return userId;
        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt =null;
                if (!PWtextBox .Text.Equals(""))
                {
                     dt = database.getTable("select * from login where staff_no ='" + staffIdtextBox.Text + "' and pw = '" + PWtextBox.Text + "';", "login");
                }
                else
                {
                    dt = database.getTable("select * from login where staff_no ='" + staffIdtextBox.Text + "' and isnull(pw)", "login");
                }
                
                if (!dt.Rows.Count.Equals(0))
                {
                    userId = Convert.ToInt32(dt.Rows[0][0]);
                    isclose = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong!");
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show("Couldn 't  connet the database!");
            }
                
        }

        private void Exitbutton_Click(object sender, EventArgs e)
        {

            this.Close();

        }
    }
}
