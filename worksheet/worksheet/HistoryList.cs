using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
namespace worksheet
{
    class HistoryList
    {
        LoadData database = new LoadData();
        int userId;
        DataGridView historyList;
        public HistoryList(int userId, DataGridView historyList)
        {
            this.userId = userId;
            this.historyList = historyList;
        }

        public void createHistory()
        {
            DataTable dt = database.getTable("select * from comment where userId = " + userId + " and( len( toiltaked )>9 or  len( toiladded )>9) order by dateoflast  asc", "comment");
            double toil = 0.0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {

                    if (Convert.ToDateTime(dr["toiltaked"]).ToString("HH:mm:ss") != "00:00:00")
                    {
                        toil -= Convert.ToDouble(Convert.ToDateTime(dr["toiltaked"]).Hour + "." + Convert.ToDateTime(dr["toiltaked"]).Minute * 10 / 60);
                        historyList.Rows.Add(Convert.ToDateTime(dr["toiltaked"]).ToShortDateString(), "", Convert.ToDateTime(dr["toiltaked"]).Hour + "." + Convert.ToDateTime(dr["toiltaked"]).Minute * 10 / 60, toil.ToString("F1"));
                        historyList.Rows[historyList.Rows.Count - 1].Cells[2].Style.ForeColor = Color.Red;
                    }
                    else if (Convert.ToDateTime(dr["toiladded"]).ToString("HH:mm:ss") != "00:00:00")
                    {
                        toil += Convert.ToDouble(Convert.ToDateTime(dr["toiladded"]).Hour + "." + Convert.ToDateTime(dr["toiladded"]).Minute * 10 / 60);
                        historyList.Rows.Add("", Convert.ToDateTime(dr["toiladded"]).ToShortDateString(), Convert.ToDateTime(dr["toiladded"]).Hour + "." + Convert.ToDateTime(dr["toiladded"]).Minute * 10 / 60, toil.ToString("F1"));
                        historyList.Rows[historyList.Rows.Count - 1].Cells[2].Style.ForeColor = Color.Green;
                    }


                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
