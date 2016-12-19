using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace worksheet
{
    public class DutyStatus
    {
        int commentId;
        public DutyStatus(int commentId = 0)
        {

            this.commentId = commentId;
        }

        public void updatestatus(timepicker tp, DateTime date)
        {
            try
            {
                LoadData database = new LoadData();
                DataTable dt = database.getTable("select * from comment where commentId =" + commentId.ToString() + ";", "comment");
                tp.start.Value = Convert.ToDateTime(dt.Rows[0][4]);
                tp.end.Value = Convert.ToDateTime(dt.Rows[0][5]);
                tp.otend.Value = Convert.ToDateTime(dt.Rows[0][7]);
                tp.otstart.Value = Convert.ToDateTime(dt.Rows[0][6]);
                tp.toiladded.Value = Convert.ToDateTime(dt.Rows[0][9]);
                tp.toiltaked.Value = Convert.ToDateTime(dt.Rows[0][8]);
                tp.isA.Checked = Convert.ToBoolean(dt.Rows[0][11]);
                tp.isN.Checked = Convert.ToBoolean(dt.Rows[0][12]);
                tp.isM.Checked = Convert.ToBoolean(dt.Rows[0][13]);
                tp.isECO.Checked = Convert.ToBoolean(dt.Rows[0][14]);
                tp.isDrive.Checked = Convert.ToBoolean(dt.Rows[0][15]);
                tp.isSPday .Checked = Convert.ToBoolean(dt.Rows[0][16]);
                tp.isStandby .Checked  = Convert.ToBoolean(dt.Rows[0][17]);

            }
            catch (Exception ex)
            {
                tp.start.Value = date.AddHours(8.5);
                tp.end.Value = date.AddHours(18);
                tp.otend.Value = date;
                tp.otstart.Value = date;
                tp.toiladded.Value = date;
                tp.toiltaked.Value = date;
                tp.isA.Checked = false;
                tp.isN.Checked = false;
                tp.isM.Checked = false;
                tp.isECO.Checked = false;
                tp.isDrive.Checked = false;
                tp.isSPday.Checked = false;
                tp.isStandby.Checked = false;
            }
        }

        public void setByDefult(timepicker tp, DateTime date)
        {
            tp.start.Value = date.AddHours(8.5);
            tp.end.Value = date.AddHours(18);
            tp.otend.Value = date;
            tp.otstart.Value = date;
            tp.toiladded.Value = date;
            tp.toiltaked.Value = date;
            tp.isA.Checked = false;
            tp.isN.Checked = false;
            tp.isM.Checked = false;
            tp.isECO.Checked = false;
            tp.isDrive.Checked = false;
            tp.isSPday.Checked = false;
            tp.isStandby.Checked = false;
        }

        public void saveStatus(timepicker tp, int userId, double oldbalance)
        {
            LoadData database = new LoadData();
            database.runsqlcmd("update comment set " +
                "dutystart = CDate('" + tp.start.Value.ToString() +
                "') , dutyend = CDate('" + tp.end.Value.ToString() +
                "') , otstart = CDate('" + tp.otstart.Value.ToString() +
                "') , otend = CDate('" + tp.otend.Value.ToString() +
                "') , toiltaked = CDate('" + tp.toiltaked.Value.ToString() +
                "') , toiladded = CDate('" + tp.toiladded.Value.ToString() +
                "') , isA = " + tp.isA.Checked +
                " , isN = " + tp.isN.Checked +
                " , isM = " + tp.isM.Checked +
                " , isECO = " + tp.isECO.Checked +
                " , isDrive = " + tp.isDrive.Checked +
                " , isSPday = " + tp.isSPday.Checked +
                " , isStandby = " + tp.isStandby.Checked +
                " where commentId = " + commentId.ToString());
            database.runsqlcmd("update staffstatus set toil_balance = toil_balance +" + (Convert.ToDouble(((tp.toiladded.Value - tp.toiltaked.Value).Hours + "." + Math.Abs(((tp.toiladded.Value - tp.toiltaked.Value).Minutes) * 10 / 60)).ToString()) - oldbalance) + " where userId =" + userId);
        }
    }
}
