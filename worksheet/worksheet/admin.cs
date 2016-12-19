using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace worksheet
{
    class Admin
    {
        ListView namelist;
        public Admin(ListView namelist )
        {
            this.namelist = namelist;
        }

        public void createNameList()
        {
            foreach (ListViewItem lvi in namelist .Items )
            {
                lvi.Remove();
            }
            LoadData database = new LoadData();
            DataTable dt = database.getTable("select * from staffstatus ", "staffstatus");
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    namelist.Items.Add(new ListViewItem(new[] { dr[0].ToString(), dr["staff_name"].ToString(), dr["staff_no"].ToString(), dr["unit"].ToString(), dr["grade"].ToString(), dr["cc"].ToString() }));
                }
                catch(Exception ex)
                {
                }
                }
        }
    }
}
