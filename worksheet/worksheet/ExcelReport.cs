using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace worksheet
{
    public class ExcelReport
    {

        Application _Excel = null;

        public ExcelReport()
        {
            bool flag = false;
            foreach (var item in Process.GetProcesses())
            {
                if (item.ProcessName == "EXCEL")
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                this._Excel = new Application();
            }
            else
            {
                object obj = Marshal.GetActiveObject("Excel.Application");//引用已在執行的Excel
                _Excel = obj as Application;
            }

            this._Excel.Visible = true;//設false效能會比較好

        }
        string intToMonth(int i)
        {
            switch (i)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
            }
            return "";
        }
        public void createReport(int userId, initiCalendar calaendar)
        {
            Workbook wb = null;
            Worksheet ws = null;
            Range range = null;
            string path = "//10.100.50.113\\caelan\\Temp\\worksheet\\C83E_From.xls";
            LoadData database = new LoadData();

            wb = _Excel.Workbooks.Open(path);

            ws = (Worksheet)wb.Sheets[1];
            range = ws.get_Range("A1", "AE200");
            System.Data.DataTable staffDT = database.getTable("select * from staffstatus where userId = " + userId, "staffstsus");
            ws.Cells[3][4] = staffDT.Rows[0]["staff_name"];
            ws.Cells[19][4] = staffDT.Rows[0]["grade"];
            ws.Cells[25][4] = staffDT.Rows[0]["unit"];
            ws.Cells[32][4] = staffDT.Rows[0]["cc"];
            ws.Cells[3][5] = staffDT.Rows[0]["staff_no"];
            ws.Cells[4][196] = "S/N  " + staffDT.Rows[0]["staff_name"];
            ws.Cells[19][5] = intToMonth(calaendar.getcurrentMonth()) + calaendar.getcurrentYear();
            int i = 9;
            HashSet<int> numbers;
            if (calaendar.getweekOfFirstDayOnMonth() == 0)
            {
                /*check the day of first, if your first day is sunday, you should start the week on row 35*/
                numbers = new HashSet<int> { 35, 41, 45, 49, 53, 57, 61, 65, 71, 75, 79, 83, 87, 91, 95, 101, 105, 109, 113, 117, 121, 125, 131, 135, 139, 143, 147, 151, 155, 161, 165, 169, 173, 177, 181, 185 };
                i = 35;
            }
            else
            {
                /*orther start time are using this patten*/
                numbers = new HashSet<int> { 11, 15, 19, 23, 27, 31, 35, 41, 45, 49, 53, 57, 61, 65, 71, 75, 79, 83, 87, 91, 95, 101, 105, 109, 113, 117, 121, 125, 131, 135, 139, 143, 147, 151, 155, 161, 165, 169, 173, 177, 181, 185 };
            }

            System.Data.DataTable dt = database.getTable("select * from comment  where userId = " + userId + " order by dateoflast ASC ;", "comment");
            System.Data.DataColumn[] dc = new System.Data.DataColumn[] { dt.Columns[0] };
            int AL = Convert.ToInt32(database.getTable("select * from staffstatus where userId = " + userId, "staffstatus").Rows[0]["al_balance"]);
            try
            {
                foreach (System.Windows.Forms.DataGridViewRow dgvr in calaendar.getCalendar().Rows)
                {
                    foreach (System.Windows.Forms.DataGridViewCell dgvc in dgvr.Cells)
                    {

                        /*check the date is or not now month, and find the day status is it normal, if yes, do some to excel, if not, yet*/
                        if (Convert.ToDateTime(calaendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex)).Month == calaendar.getcurrentMonth())
                        {
                            ws.Cells[2][i] = Convert.ToDateTime(calaendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex)).Day.ToString();
                            if (dgvc.Tag == null)
                            {


                            }
                            else
                            {
                                System.Data.DataRow dr = database.getDateRowByKey(dc, new object[] { dgvc.Tag }, dt);
                                if (dr["daystatus"].Equals("Normal"))
                                {
                                    string[] s = dr[1].ToString().Split(new char[] { });
                                    int j = 0;
                                    foreach (string ss in s)
                                    {
                                        ws.Cells[32][i + j] = ss;
                                        j++;
                                    }
                                    ws.Cells[2][i] = Convert.ToDateTime(calaendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex)).Day.ToString();
                                    ws.Cells[3][i] = Convert.ToDateTime(dr[4]).ToString("HH:mm");
                                    ws.Cells[4][i] = Convert.ToDateTime(dr[5]).ToString("HH:mm");
                                    try
                                    {
                                        if (!((Convert.ToDateTime(dr["otend"]) - Convert.ToDateTime(dr["otstart"])).Hours.ToString() + "." + (((Convert.ToDateTime(dr["otend"]) - Convert.ToDateTime(dr["otstart"])).Minutes) * 10 / 60).ToString()).Equals("0.0"))
                                        {
                                            ws.Cells[6][i] = Convert.ToDateTime(dr["otend"]).ToString("HH:mm");
                                            ws.Cells[5][i] = Convert.ToDateTime(dr["otstart"]).ToString("HH:mm");
                                            if (Convert.ToBoolean(dr[14]))
                                            {
                                                ws.Cells[11][i] = ((Convert.ToDateTime(dr["otend"]) - Convert.ToDateTime(dr["otstart"])).Hours.ToString() + "." + (((Convert.ToDateTime(dr["otend"]) - Convert.ToDateTime(dr["otstart"])).Minutes) * 10 / 60).ToString());
                                            }
                                            else
                                            {
                                                ws.Cells[16][i] = (Convert.ToDateTime(dr["otend"]) - Convert.ToDateTime(dr["otstart"])).Hours.ToString() + "." + (((Convert.ToDateTime(dr["otend"]) - Convert.ToDateTime(dr["otstart"])).Minutes) * 10 / 60).ToString();
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    if (Convert.ToBoolean(dr["isA"])) ws.Cells[19][i] = "1";
                                    if (Convert.ToBoolean(dr["isN"])) ws.Cells[21][i] = "1";
                                    //    if (Convert.ToBoolean(dr["isM"]))
                                    if (Convert.ToBoolean(dr["isECO"])) ws.Cells[10][i] = "1";
                                    if (Convert.ToBoolean(dr["isDrive"])) ws.Cells[25][i] = "1";
                                    if (Convert.ToBoolean(dr["isSPday"])) ws.Cells[23][i] = "1";
                                    if (Convert.ToBoolean(dr["isStandby"])) ws.Cells[27][i] = "1";


                                }

                                else
                                {
                                    if (dr["daystatus"].Equals("Annual Leave"))
                                    {
                                        AL++;
                                        ws.Cells[29][i] = -1;
                                    }

                                    ws.Cells[3][i] = getstatus(database.getDateRowByKey(dc, new object[] { dgvc.Tag }, dt)["daystatus"].ToString());
                                }
                            }


                        }
                        for (i = i + 2; !numbers.Contains(i); i = i + 2) ;
                    }
                }
                ws.Cells[29][10] = AL;
            }
            catch (Exception ex)
            {

            }
            wb.SaveAs("C:\\WS\\test.xls");
            wb.Close();
        }

        string getstatus(string fullname)
        {
            switch (fullname)
            {
                case "Annual Leave":
                    return "AL";
                case "Sick Leave":
                    return "SL";
                case "Pubilc Holiday":
                    return "PH";
                case "Statutory Holiday":
                    return "SH";
                case "Day Off":
                    return "DO";
                case "Rest Day":
                    return "RD";
            }
            return "";
        }
    }
}

