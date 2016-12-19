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
    public partial class AddUserForm : Form  
    {
        Admin admin;
        ListView namelist;
        string staffId;

        public AddUserForm(string mod , ListView namelist , string staffId = null, staffstatus ss = new staffstatus())
        {
            InitializeComponent();
            applybutton.Text = mod;

            this.namelist = namelist;
            this.staffId = staffId;
            if (mod.Equals ("Moditfly"))
            {
                staffnametextBox.Text = ss.staffnamebox;
                staffnumbertextBox.Text = ss.staffnobox;
                gradecomboBox.Text = ss.gradebox;
                unitcomboBox.Text = ss.unitbox;
                CCcomboBox.Text = ss.ccbox;
            }
            
        }

        private void applybutton_Click(object sender, EventArgs e)
        {
            LoadData database = new LoadData();
            admin = new Admin(namelist);
            switch (applybutton.Text)
            {
                case "Add":
                    database.runsqlcmd("insert into login(staff_no) values('" + staffnumbertextBox.Text + "');");

                    database.runsqlcmd("insert into staffstatus (staff_name, userId, staff_no, grade, unit, cc ) values('" +
                        staffnametextBox.Text + "'," +
                         database.getTable("select LAST(userId)  from login", "login").Rows[0][0] + ",'" +
                         staffnumbertextBox.Text + "','" +
                         gradecomboBox.Text +"','"+
                         unitcomboBox.Text +"','"+
                         CCcomboBox.Text +
                    "');");
                    admin.createNameList();
                    this.Close();
                    break;
                case "Moditfly":
                    database.runsqlcmd("update staffstatus set staff_name = '" + staffnametextBox.Text +
                        "', staff_no = '" + staffnumbertextBox.Text + "', " +
                        "grade = '" + gradecomboBox.Text + "'," +
                        "unit = '" + unitcomboBox.Text + "'," +
                        "cc = '" + CCcomboBox.Text + "' " +
                        "where staffId = " + staffId
                        );
                    admin.createNameList();
                    this.Close();
                    break;
            }
        }
    }
}
