using System;
namespace worksheet
{
public class initiCalendar : MainPage 
{
    public initiCalendar()
	{
        Calendar.Rows.Add(Calendar.Rows);
        Calendar.Rows.Add(Calendar.Rows);
        Calendar.Rows.Add(Calendar.Rows);
        Calendar.Rows.Add(Calendar.Rows);
        Calendar.Rows.Add(Calendar.Rows);
        Calendar.Rows.Add(Calendar.Rows);
        Calendar.Rows[0].Height = 40;
        Calendar.Rows[1].Height = 40;
        Calendar.Rows[2].Height = 40;
        Calendar.Rows[3].Height = 40;
        Calendar.Rows[4].Height = 40;
        Calendar.Rows[5].Height = 40;
        LoginPage lp = new LoginPage();
        lp.ShowDialog();

        if (isclose)
        {
            this.Close();
        }
        int weekOfFirstDayOnMonth = (int)DateTime.Now.AddDays(1 + -DateTime.Now.Day).DayOfWeek;
        int i = 1;
        if (weekOfFirstDayOnMonth != 0)
        {
            for (int j = 0; j < weekOfFirstDayOnMonth; j++)
            {
                Calendar.Rows[0].Cells[j].Style.ForeColor = Color.Gray;

                Calendar.Rows[0].Cells[j].Value = DateTime.Now.AddDays(-DateTime.Now.Day - (weekOfFirstDayOnMonth - j) + 1).Day;

            }
        }

        foreach (DataGridViewRow dgv in Calendar.Rows)
        {
            while (weekOfFirstDayOnMonth < 7)
            {

                if (i == DateTime.Now.Day)
                {
                    dgv.Cells[weekOfFirstDayOnMonth].Style.Font = new Font("Consolas", 16, FontStyle.Bold);
                }
                if (i > DateTime.Now.Day)
                {
                    dgv.Cells[weekOfFirstDayOnMonth].Style.ForeColor = Color.IndianRed;
                }
                if (DateTime.Now.AddDays(i + -DateTime.Now.Day).Month != DateTime.Now.Month)
                {
                    dgv.Cells[weekOfFirstDayOnMonth].Style.ForeColor = Color.Gray;
                }
                dgv.Cells[weekOfFirstDayOnMonth].Value = DateTime.Now.AddDays(i + -DateTime.Now.Day).Day;
                weekOfFirstDayOnMonth++;
                i++;
            }
            weekOfFirstDayOnMonth = 0;
        }
        foreach (DataGridViewColumn dgvc in Calendar.Columns)
        {
            dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
	}
}
}