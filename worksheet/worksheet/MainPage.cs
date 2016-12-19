using System;
using System.Data;
using System.Windows.Forms;

namespace worksheet
{
    public struct timepicker
    {
        public DateTimePicker start;
        public DateTimePicker end;
        public DateTimePicker otstart;
        public DateTimePicker otend;
        public DateTimePicker toiltaked;
        public DateTimePicker toiladded;
        public CheckBox isA;
        public CheckBox isN;
        public CheckBox isM;
        public CheckBox isECO;
        public CheckBox isDrive;
        public CheckBox isSPday;
        public CheckBox isStandby;
        public Label otTimes;
    }

    public struct staffstatus
    {
        public string staffnamebox;
        public string staffnobox;
        public string gradebox;
        public string unitbox;
        public string ccbox;
    }

    public partial class MainPage : Form
    {
        int userId;
        timepicker tp;
        initiCalendar calendar;
        LoadData database;
        int selcetCellcommentId;
        int neededDuplicateId;
        double oldbalance;
        Admin admin;
        DataGridViewSelectedCellCollection selectcells;


        int monthToInt(string month)
        {
            switch (month)
            {
                case "January":
                    return 1;
                case "February":
                    return 2;
                case "March":
                    return 3;
                case "April":
                    return 4;
                case "May":
                    return 5;
                case "June":
                    return 6;
                case "July":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "October":
                    return 10;
                case "November":
                    return 11;
                case "December":
                    return 12;
            }
            return DateTime.Now.Month;
        }
        public MainPage()
        {
            InitializeComponent();

            timepicker tp = new timepicker();
            tp.otend = OTenddateTimePicker;
            tp.otstart = OTstartdateTimePicker;
            tp.start = startdateTimePicker;
            tp.end = enddateTimePicker;
            tp.toiladded = toiladdeddateTimePicker;
            tp.toiltaked = toiltakeddateTimePicker;
            tp.isA = isAcheckBox;
            tp.isN = isNcheckBox;
            tp.isM = isMcheckBox;
            tp.isECO = isECOcheckBox;
            tp.isDrive = isDrivecheckBox;
            tp.otTimes = otTimelabel;
            tp.isSPday = isSPdaycheckBox;
            tp.isStandby = isStandbycheckBox;
            this.tp = tp;
            database = new LoadData();
            LoginPage lp = new LoginPage(database);
            lp.ShowDialog();

            if (lp.isclosed())
            {
                this.Close();
            }

            userId = lp.getUserId();
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
            Calendar.Rows[0].Cells[0].Selected = false;
            foreach (DataGridViewColumn dgvc in Calendar.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn dgvc in historyList.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            MonthcomboBox.SelectedItem = MonthcomboBox.Items[DateTime.Now.Month - 1];
            this.MonthcomboBox.SelectedIndexChanged += new System.EventHandler(this.MonthcomboBox_SelectedIndexChanged);
            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                yearcomboBox.Items.Add(i);
            }
            yearcomboBox.SelectedItem = DateTime.Now.Year;
            initiCalendar calendar = new initiCalendar(Calendar);
            calendar.createCalendar(DateTime.Now.Month, DateTime.Now.Year, userId.ToString());
            HistoryList history = new HistoryList(userId, historyList);
            history.createHistory();
            Admin admin = new Admin(namelistView);
            admin.createNameList();
            this.admin = admin;
            this.calendar = calendar;

            this.tabControl1.Controls.Remove(this.tabPage2);
            this.tabControl1.Controls.Remove(this.tabPage3);
            this.tabControl1.Controls.Remove(this.Toilexport);
        }


        private void exitbutton_Click(object sender, EventArgs e)
        {


        }

        private void Calendar_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*read data by tag(id)*/
            if (Calendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag == null)
            {
                /* Description description = new Description(calendar.getDateByCaleandar(e.RowIndex, e.ColumnIndex), userId, "", true, Calendar.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                 description.ShowDialog();
                 selcetCellcommentId = Convert.ToInt32(Calendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag);
                 modifybutton.Enabled = description.isapplyed();*/
            }
            else
            {
                Description description = new Description(calendar.getDateByCaleandar(e.RowIndex, e.ColumnIndex), userId, Calendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag.ToString());
                description.ShowDialog();
                modifybutton.Enabled = description.isapplyed();

            }

        }

        private void MonthcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            calendar.createCalendar(monthToInt(MonthcomboBox.Text), DateTime.Now.Year, userId.ToString());
        }

        private void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            /*check the selcet status, siganl of muilti?*/
            if (Calendar.SelectedCells.Count == 1)
            {
                duplicateThisDayToolStripMenuItem.Enabled = true;
                selcetCellcommentId = Convert.ToInt32(Calendar.SelectedCells[0].Tag);
                isNcheckBox.CheckedChanged -= new System.EventHandler(this.isNcheckBox_CheckedChanged);
                isMcheckBox.CheckedChanged -= new System.EventHandler(this.isMcheckBox_CheckedChanged);
                isAcheckBox.CheckedChanged -= new System.EventHandler(this.isAcheckBox_CheckedChanged);
                if (!(Calendar.SelectedCells[0].Tag == null))
                {
                    DutyStatus status = new DutyStatus(selcetCellcommentId);
                    status.updatestatus(tp, Convert.ToDateTime(calendar.getDateByCaleandar(Calendar.SelectedCells[0].RowIndex, Calendar.SelectedCells[0].ColumnIndex)));
                    modifybutton.Enabled = true;
                    notificationlabel.Text = database.getTable("select * from comment where commentId = " + Calendar.SelectedCells[0].Tag, "comment").Rows[0]["daystatus"].ToString();

                }
                else
                {
                    DutyStatus status = new DutyStatus();
                    status.setByDefult(tp, Convert.ToDateTime(calendar.getDateByCaleandar(Calendar.SelectedCells[0].RowIndex, Calendar.SelectedCells[0].ColumnIndex)));
                    modifybutton.Enabled = false;
                    notificationlabel.Text = "No data";
                }
                isNcheckBox.CheckedChanged += new System.EventHandler(this.isNcheckBox_CheckedChanged);
                isMcheckBox.CheckedChanged += new System.EventHandler(this.isMcheckBox_CheckedChanged);
                isAcheckBox.CheckedChanged += new System.EventHandler(this.isAcheckBox_CheckedChanged);
                otTimelabel.Text = (OTenddateTimePicker.Value - OTstartdateTimePicker.Value).Hours.ToString() + "." + (((OTenddateTimePicker.Value - OTstartdateTimePicker.Value).Minutes) * 10 / 60).ToString();
            }

            if (Calendar.SelectedCells.Count > 1)
            {
                duplicateThisDayToolStripMenuItem.Enabled = false;
            }
            selectcells = Calendar.SelectedCells;
        }

        private void saveStatusbutton_Click(object sender, EventArgs e)
        {
            DutyStatus status = new DutyStatus(selcetCellcommentId);
            status.saveStatus(tp, userId, oldbalance);
            saveStatusbutton.Enabled = false;
            Calendar.Enabled = true;
            modifybutton.Enabled = true;
            startdateTimePicker.Enabled = false;
            enddateTimePicker.Enabled = false;
            OTenddateTimePicker.Enabled = false;
            OTstartdateTimePicker.Enabled = false;
            toiladdeddateTimePicker.Enabled = false;
            toiltakeddateTimePicker.Enabled = false;
            isAcheckBox.Enabled = false;
            isNcheckBox.Enabled = false;
            isMcheckBox.Enabled = false;
            isECOcheckBox.Enabled = false;
            isDrivecheckBox.Enabled = false;
            isSPdaycheckBox.Enabled = false;
            isStandbycheckBox.Enabled = false;
        }

        private void duplicateThisDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            duplicateToSelectDayToolStripMenuItem.Enabled = true;
            duplicateThisDayToolStripMenuItem.Enabled = false;
            neededDuplicateId = selcetCellcommentId;
        }

        private void Calendar_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*create the contact menus*/
            if (e.Button.Equals(MouseButtons.Right) & e.RowIndex != -1)
            {
                if (!Calendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                {
                    Calendar.ClearSelection();
                }
                Calendar.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                selectcells = Calendar.SelectedCells;
                calendarcontextMenuStrip.Show(MousePosition);
            }
        }
        string nulltostring(object O)
        {

            try
            {
                return Convert.ToDateTime(O).ToShortTimeString();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        private void duplicateToSelectDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*duplicate function*/

            duplicateToSelectDayToolStripMenuItem.Enabled = false;
            duplicateThisDayToolStripMenuItem.Enabled = true;
            DataTable dt = database.getTable("select * from comment where commentId = " + neededDuplicateId + ";", "comment");


            int i = selectcells.Count - 1;
            foreach (DataGridViewCell dgvc in selectcells)
            {

                DateTime date = new DateTime();
                date = Convert.ToDateTime(calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex));
                string updatesqlCMD = "update comment set comment = '" + dt.Rows[0][1].ToString() +
                "', dutystart = '" + date.ToShortDateString() + " " + Convert.ToDateTime(dt.Rows[0][4]).ToShortTimeString() +
                "', dutyend = '" + date.ToShortDateString() + " " + Convert.ToDateTime(dt.Rows[0][5]).ToShortTimeString() +
                "', otstart = '" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][6].ToString()) +
                "', otend = '" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][7].ToString()) +
                "', toiltaked = '" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][8].ToString()) +
                "', toiladded = '" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][9].ToString()) +
                "', daystatus = '" + dt.Rows[0][10].ToString() +
                "', isA = " + Convert.ToBoolean(dt.Rows[0][11]) +
                ", isN = " + Convert.ToBoolean(dt.Rows[0][12]) +
                ", isM = " + Convert.ToBoolean(dt.Rows[0][13]) +
                ", isECO = " + Convert.ToBoolean(dt.Rows[0][14]) +
                ", isDrive = " + Convert.ToBoolean(dt.Rows[0][15]) +
                ", isSPday = " + Convert.ToBoolean(dt.Rows[0][16]) +
                " where commentId =";

                if (dgvc.Tag == null)
                {
                    string insertsqlCMD = "insert into comment (comment, dateoflast, userId, dutystart, dutyend, otstart, otend, toiltaked, toiladded, daystatus, isA, isN, isM, isECO, isDrive, isSPday, isStandby) values ('" +
                dt.Rows[0][1].ToString() +
                "', CDate('" + date.ToString() +
                "'), " + dt.Rows[0][3].ToString() +
                ", CDate('" + date.ToShortDateString() + " " + Convert.ToDateTime(dt.Rows[0][4]).ToShortTimeString() +
                "'), CDate('" + date.ToShortDateString() + " " + Convert.ToDateTime(dt.Rows[0][5]).ToShortTimeString() +
                "'), CDate('" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][6]) +
                "'), CDate('" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][7]) +
                "'), CDate('" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][8]) +
                "'), CDate('" + date.ToShortDateString() + " " + nulltostring(dt.Rows[0][9]) +
                "'), '" + (dt.Rows[0][10]).ToString() +
                "', " + Convert.ToBoolean(dt.Rows[0][11]) +
                ", " + Convert.ToBoolean(dt.Rows[0][12]) +
                ", " + Convert.ToBoolean(dt.Rows[0][13]) +
                ", " + Convert.ToBoolean(dt.Rows[0][14]) +
                ", " + Convert.ToBoolean(dt.Rows[0][15]) +
                ", " + Convert.ToBoolean(dt.Rows[0][16]) +
                ", " + Convert.ToBoolean(dt.Rows[0][17]) +
                ");";
                    database.runsqlcmd(insertsqlCMD);
                    database.runsqlcmd("update staffstatus set al_balance = al_balance + 1  where userId =" + userId);
                }
                else
                {
                    /*****************if the origion day status isn't al *******************/

                    if (dt.Rows[0][10].ToString().Equals("Annual Leave") && !database.getTable("select * from comment where commentId = " + dgvc.Tag + ";", "comment").Rows[0]["daystatus"].ToString().Equals("Annual Leave"))
                    {
                        database.runsqlcmd("update staffstatus set al_balance = al_balance + 1 where userId =" + userId);
                    }

                    updatesqlCMD = updatesqlCMD + dgvc.Tag + ";";
                    database.runsqlcmd(updatesqlCMD);
                }
                double oldtoil = 0.00;
                double newtoil = 0.00;
                DateTime toiladded;
                    DateTime toiltaked;
                try
                {
                     toiladded = Convert.ToDateTime(database.getTable("select * from comment where commentId =  " + dgvc.Tag + ";", "comment").Rows[0]["toiladded"]);
                     toiltaked = Convert.ToDateTime(database.getTable("select * from comment where commentId =  " + dgvc.Tag + ";", "comment").Rows[0]["toiltaked"]);
                    oldtoil = Convert.ToDouble((toiladded-toiltaked).Hours.ToString() + "." + (((toiladded-toiltaked).Minutes) * 10 / 60).ToString());
                }
                catch(Exception ex)
                {
                    oldtoil = 0.0;
                }
                toiladded = Convert.ToDateTime(dt.Rows[0]["toiladded"]);
                toiltaked = Convert.ToDateTime(dt.Rows[0]["toiltaked"]);
                newtoil = Convert.ToDouble((toiladded - toiltaked).Hours.ToString() + "." + ((( toiladded- toiltaked).Minutes) * 10 / 60).ToString());
                database.runsqlcmd("Update staffstatus set toil_balance = toil_balance +" + (newtoil - oldtoil).ToString() + " where userId = " + userId);
            }

            calendar.createCalendar(monthToInt(MonthcomboBox.Text), DateTime.Now.Year, userId.ToString());
        }

        private void calendarcontextMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.Calendar.SelectionChanged -= new System.EventHandler(this.Calendar_SelectionChanged);
            Calendar.ClearSelection();
            this.Calendar.SelectionChanged += new System.EventHandler(this.Calendar_SelectionChanged);
        }

        private void dayStatusToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            foreach (DataGridViewCell dgvc in selectcells)
            {
                if (dgvc.Tag != null)
                {
                    database.runsqlcmd("update comment set daystatus = " + "'" + e.ClickedItem.Text + "'" + "where commentId = " + dgvc.Tag + ";");
                }
                else
                {
                    DateTime dutystart = Convert.ToDateTime(calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex)).AddHours(8).AddMinutes(30);
                    DateTime dutyend = Convert.ToDateTime(calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex)).AddHours(18);
                    database.runsqlcmd("insert into comment ( dateoflast, userId, daystatus, dutystart, dutyend , otstart, otend, toiladded, toiltaked) values (CDate('" + calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex) + "') ,'" + userId + "','" + e.ClickedItem.Text + "', CDate('" + dutystart + "'), CDate('" + dutyend + "'),CDate('" + calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex) + "'),CDate('" + calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex) + "'),CDate('" + calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex) + "'),CDate('" + calendar.getDateByCaleandar(dgvc.RowIndex, dgvc.ColumnIndex) + "'));");
                    dgvc.Tag = database.getTable("select LAST(commentId)  from comment", "comment").Rows[0][0];
                }

            }
            try
            {
                calendar.createCalendar(monthToInt(MonthcomboBox.Text), DateTime.Now.Year, userId.ToString());
                Calendar.ClearSelection();
                selectcells[0].Selected = true;
            }
            catch (Exception ex)
            {
                //   MessageBox.Show(ex.Message);
            }
        }

        private void modifybutton_Click(object sender, EventArgs e)
        {

            try
            {
                oldbalance = Convert.ToDouble((toiladdeddateTimePicker.Value - toiltakeddateTimePicker.Value).Hours.ToString() + "." + Math.Abs(((toiladdeddateTimePicker.Value - toiltakeddateTimePicker.Value).Minutes) * 10 / 60).ToString());

            }
            catch (Exception ex)
            {
            }
            modifybutton.Enabled = false;
            Calendar.Enabled = false;
            saveStatusbutton.Enabled = true;
            startdateTimePicker.Enabled = true;
            enddateTimePicker.Enabled = true;
            OTenddateTimePicker.Enabled = true;
            OTstartdateTimePicker.Enabled = true;
            toiladdeddateTimePicker.Enabled = true;
            toiltakeddateTimePicker.Enabled = true;
            isAcheckBox.Enabled = true;
            isNcheckBox.Enabled = true;
            isMcheckBox.Enabled = true;
            isECOcheckBox.Enabled = true;
            isDrivecheckBox.Enabled = true;
            isSPdaycheckBox.Enabled = true;
            isStandbycheckBox.Enabled = true;

        }

        private void createExcelReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelReport er = new ExcelReport();
            er.createReport(userId, calendar);
        }

        private void isAcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isNcheckBox.CheckedChanged -= new System.EventHandler(this.isNcheckBox_CheckedChanged);
            isMcheckBox.CheckedChanged -= new System.EventHandler(this.isMcheckBox_CheckedChanged);
            if (isAcheckBox.Checked)
            {
                isNcheckBox.Checked = false;
                isMcheckBox.Checked = false;
                startdateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 14, 00, 0);
                enddateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 23, 0, 0);
                startdateTimePicker.Enabled = false;
                enddateTimePicker.Enabled = false;
            }
            else
            {
                startdateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 8, 30, 0);
                enddateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 18, 0, 0);
                startdateTimePicker.Enabled = true;
                enddateTimePicker.Enabled = true;
            }

            isNcheckBox.CheckedChanged += new System.EventHandler(this.isNcheckBox_CheckedChanged);
            isMcheckBox.CheckedChanged += new System.EventHandler(this.isMcheckBox_CheckedChanged);
        }

        private void isNcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isMcheckBox.CheckedChanged -= new System.EventHandler(this.isMcheckBox_CheckedChanged);
            isAcheckBox.CheckedChanged -= new System.EventHandler(this.isAcheckBox_CheckedChanged);
            if (isNcheckBox.Checked)
            {
                isMcheckBox.Checked = false;
                isAcheckBox.Checked = false;
                startdateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 23, 00, 0);
                enddateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.AddDays(1).Day, 8, 30, 0);
                startdateTimePicker.Enabled = false;
                enddateTimePicker.Enabled = false;
            }
            else
            {
                startdateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 8, 30, 0);
                enddateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 18, 0, 0);
                startdateTimePicker.Enabled = true;
                enddateTimePicker.Enabled = true;
            }
            isMcheckBox.CheckedChanged += new System.EventHandler(this.isMcheckBox_CheckedChanged);
            isAcheckBox.CheckedChanged += new System.EventHandler(this.isAcheckBox_CheckedChanged);
        }

        private void isMcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isNcheckBox.CheckedChanged -= new System.EventHandler(this.isNcheckBox_CheckedChanged);
            isAcheckBox.CheckedChanged -= new System.EventHandler(this.isAcheckBox_CheckedChanged);
            if (isMcheckBox.Checked)
            {
                isNcheckBox.Checked = false;
                isAcheckBox.Checked = false;
                startdateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 8, 30, 0);
                enddateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 14, 0, 0);
                startdateTimePicker.Enabled = false;
                enddateTimePicker.Enabled = false;
            }
            else
            {
                startdateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 8, 30, 0);
                enddateTimePicker.Value = new DateTime(startdateTimePicker.Value.Year, startdateTimePicker.Value.Month, startdateTimePicker.Value.Day, 18, 0, 0);
                startdateTimePicker.Enabled = true;
                enddateTimePicker.Enabled = true;
            }
            isNcheckBox.CheckedChanged += new System.EventHandler(this.isNcheckBox_CheckedChanged);
            isAcheckBox.CheckedChanged += new System.EventHandler(this.isAcheckBox_CheckedChanged);
        }

        private void historybutton_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.RemoveAt(0);
            this.tabControl1.Controls.Add(this.tabPage2);

        }

        private void calendarbutton_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.RemoveAt(0);
            this.tabControl1.Controls.Add(this.tabPage1);

        }

        private void adminbutton_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.RemoveAt(0);
            this.tabControl1.Controls.Add(this.tabPage3);
        }

        private void namelistView_MouseClick(object sender, MouseEventArgs e)
        {
            /*create the contact menus*/
            if (e.Button.Equals(MouseButtons.Right))
            {
                nameListcontextMenuStrip.Show(MousePosition);

            }
        }

        private void moditflyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            staffstatus ss = new worksheet.staffstatus();
            ss.ccbox = namelistView.SelectedItems[0].SubItems[5].Text;
            ss.staffnamebox = namelistView.SelectedItems[0].SubItems[1].Text;
            ss.staffnobox = namelistView.SelectedItems[0].SubItems[2].Text;
            ss.gradebox = namelistView.SelectedItems[0].SubItems[3].Text;
            ss.unitbox = namelistView.SelectedItems[0].SubItems[4].Text;
            AddUserForm applyuser = new AddUserForm("Moditfly", namelistView, namelistView.SelectedItems[0].SubItems[0].Text, ss);

            applyuser.ShowDialog();

        }

        private void addStaffbutton_Click(object sender, EventArgs e)
        {
            AddUserForm applyuser = new AddUserForm("Add", namelistView);
            applyuser.ShowDialog();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ExportToilbutton_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.RemoveAt(0);
            this.tabControl1.Controls.Add(this.Toilexport);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                namelistView.FindItemWithText(textBox1.Text, true, 0).Selected = true;
            }
            catch(Exception ex)
            {

            }
        }
    }
}
