using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
namespace worksheet
{
    public class initiCalendar
    {
        DataGridView calendar;
        string[][] date = new string[6][];
        string[][] commentId = new string[6][];
        int currentMonth;
        int currentYear;
        int weekOfFirstDayOnMonth;
        public initiCalendar(DataGridView calendars)
        {
            calendar = calendars;

        }
        public int getcurrentMonth()
        {
            return currentMonth;
        }
        public int getcurrentYear()
        {
            return currentYear;
        }
        public DataGridView  getCalendar()
        {
            return calendar;
        }
        public string getDateByCaleandar(int row, int column)
        {
            return date[row][column];
        }

        public string getcommentId(int row, int column)
        {
            return commentId[row][column];
        }

        public int getweekOfFirstDayOnMonth()
        {
            return weekOfFirstDayOnMonth;
        }
        public void createCalendar(int whitchmonth , int whitchyear, string userId)
        {
            currentMonth = whitchmonth;
            currentYear = whitchyear;
            for (int k = 0; k < 6; k++)
            {
                date[k] = new string[] { "", "", "", "", "", "", "" };
                commentId[k] = new string[] { "", "", "", "", "", "", "" };
            }

            DateTime dt = new DateTime(DateTime.Now.Year, whitchmonth, 1);
            int weekOfFirstDayOnMonth = (int)dt.DayOfWeek;
            int i = 1;
            this.weekOfFirstDayOnMonth = weekOfFirstDayOnMonth;
            /*check the first day is or not sunday, if not, do that*/
            if (weekOfFirstDayOnMonth != 0)
            {
                for (int j = 0; j < weekOfFirstDayOnMonth; j++)
                {
                    calendar.Rows[0].Cells[j].Style.BackColor = Color.White;
                    if (dt.AddDays(-j).Date < DateTime.Now.Date)
                    {
                        calendar.Rows[0].Cells[j].Style.ForeColor = Color.Blue;
                    }
                    if (dt.AddDays(-j).Date > DateTime.Now.Date)
                    {
                        calendar.Rows[0].Cells[j].Style.ForeColor = Color.Gray;
                    }
                    calendar.Rows[0].Cells[j].Value = dt.AddDays(-(weekOfFirstDayOnMonth - j)).Day;
                    calendar.Rows[0].Cells[j].Tag = null;
                    date[0][j] = dt.AddDays(-(weekOfFirstDayOnMonth - j)).Day + "/" + dt.AddDays(-(weekOfFirstDayOnMonth - j)).Month + "/" + dt.AddDays(-(weekOfFirstDayOnMonth - j)).Year;
                }
            }

            foreach (DataGridViewRow dgv in calendar.Rows)
            {
                /*loop the week form sunday to statraday to configure the date and calendar*/
                while (weekOfFirstDayOnMonth < 7)
                {
                    dgv.Cells[weekOfFirstDayOnMonth].Style.Font = new Font("Consolas", 12, FontStyle.Regular);
                    dgv.Cells[weekOfFirstDayOnMonth].Style.ForeColor = Color.Black;
                    dgv.Cells[weekOfFirstDayOnMonth].Style.BackColor = Color.White;
                    if (dt.AddDays(i - 1).Date == DateTime.Now.Date && dt.AddDays(i - 1).Month == whitchmonth)
                    {
                        dgv.Cells[weekOfFirstDayOnMonth].Style.Font = new Font("Consolas", 16, FontStyle.Bold);
                    }
                    if (dt.AddDays(i - 1).Date > DateTime.Now.Date & dt.AddDays(i - 1).Month == DateTime.Now.Month)
                    {
                        dgv.Cells[weekOfFirstDayOnMonth].Style.ForeColor = Color.IndianRed;
                    }
                    if (dt.AddDays(i - 1).Month < DateTime.Now.Month & dt.AddDays(i - 1).Date < DateTime.Now.Date)
                    {
                        dgv.Cells[weekOfFirstDayOnMonth].Style.ForeColor = Color.Blue;
                    }
                    if (dt.AddDays(i - 1).Month > DateTime.Now.Month | dt.AddDays(i - 1).Year > DateTime.Now.Year)
                    {
                        dgv.Cells[weekOfFirstDayOnMonth].Style.ForeColor = Color.Gray;
                    }
                    dgv.Cells[weekOfFirstDayOnMonth].Value = dt.AddDays(i - 1).Day;
                    dgv.Cells[weekOfFirstDayOnMonth].Tag = null;
                    date[dgv.Index][weekOfFirstDayOnMonth] = dt.AddDays(i - 1).Day + "/" + dt.AddDays(i - 1).Month + "/" + dt.AddDays(i - 1).Year;
                    weekOfFirstDayOnMonth++;
                    i++;
                }
                weekOfFirstDayOnMonth = 0;
            }

            weekOfFirstDayOnMonth = (int)dt.DayOfWeek;
            LoadData database = new LoadData();
            foreach (DataRow dr in database.getTable("select * from comment where userId = " + userId, "comment").Rows)
            {
                if (Convert.ToDateTime(dr[2]).Month.Equals(whitchmonth))
                {
                    /*mark the tag to be the id, for key using only*/
                    calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Tag = dr[0];

                    switch (dr["daystatus"].ToString())
                    {
                        case "Normal":
                            calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Style.BackColor = Color.Green;
                            break;

                        case "Annual Leave":
                            calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Style.BackColor = Color.Red;
                            break;

                        case "Sick Leave":
                            calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Style.BackColor = Color.RoyalBlue;
                            break;

                        case "Pubilc Holiday":
                            calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Style.BackColor = Color.YellowGreen;
                            break;
                        case "Day Off":
                            calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Style.BackColor = Color.DimGray;
                            break;
                        case "Rest Day":
                            calendar.Rows[(Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) / 7].Cells[((Convert.ToDateTime(dr[2]).Day + weekOfFirstDayOnMonth - 1) % 7)].Style.BackColor = Color.Maroon;
                            break;
                    }
                }
            }

        }
    }
}