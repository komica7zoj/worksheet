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
    public partial class Description : Form
    {
        string commentId;
        string userId;
        bool isnewcomment;
        LoadData database;
        string datetime;
        DataGridViewCell dgvc;
        bool isapply = false;
        public Description(string datetime, int userId, string commentId = "", bool isnewcomment = false, DataGridViewCell dgvc = null)
        {
            InitializeComponent();
            DataTable dt;
            LoadData database = new LoadData();
            this.database = database;
            this.datetime = Convert.ToDateTime(datetime).ToString();
            dt = database.getTable("select * from comment where userId = " + userId.ToString() + ";", "comment");
            DataColumn[] dc = new DataColumn[] { dt.Columns[0] };
            if (!isnewcomment)
            {
                commentBox.Text = database.getDateRowByKey(dc, new object[] { commentId }, dt)[1].ToString();
                this.commentId = commentId;
            }
            this.isnewcomment = isnewcomment;
            this.userId = userId.ToString();
            this.dgvc = dgvc;
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            string ss ="";
            foreach (string s in commentBox.Text.Split(new[] { "\n" }, StringSplitOptions.None))
            {

                if (s.Length > 50)
                {
                    int i;
                    for (i = 50; i < s.Length; i = i + 50)
                    {
                        ss += s.Substring(i - 50, 50) + "\n";
                    }
                    ss += s.Substring(i - 50, s.Length - (i - 50));
                }
                else
                {
                    ss += s+"\n";
                }
            }
            commentBox.Text = ss;

            if (isnewcomment)
            {
                database.runsqlcmd("insert into comment (comment, dateoflast, userId) values ('" + ss + "','" + datetime + "','" + userId + "') ;");
                dgvc.Tag = database.getTable("select * from comment where userId = " + userId.ToString() + " and dateoflast =CDate('" + datetime + "');", "comment").Rows[0][0];
            }
            else
            {
                database.runsqlcmd("update comment set comment ='" + ss + "'" + " where commentId =" + commentId + ";");
            }
            isapply = true;
            this.Close();
        }

        public bool isapplyed()
        {
            return isapply;
        }
    }
}
