using System;
using System.Data.OleDb;
using System.Data;
namespace worksheet
{
    public class LoadData
    {
         string cs = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                 "Data Source=" + @"//10.100.50.113\caelan\Temp\worksheet\worksheet.accdb" + ";" +
                      "Jet OLEDB:Database Password=Qwe123456789";

        /*  string cs = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                 "Data Source=" + "C:\\Users\\SAMSUNG\\Documents\\worksheet.accdb" + ";" +
                      "Jet OLEDB:Database Password=Qwe123456789"; */
        OleDbConnection cnn;
        OleDbCommand cmd;
        DataSet ds = new DataSet();
        public LoadData()
        {
            cnn = new OleDbConnection(cs);
            cmd = new OleDbCommand();
        }

        public DataRow getDateRowByKey(DataColumn[] keycolumn, object[] key, DataTable dt)
        {
            dt.PrimaryKey = keycolumn;

            return dt.Rows.Find(key);
        }

        public void runsqlcmd(string sqlcmd)
        {
            cnn.Open();
            cmd.CommandText = sqlcmd;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            cnn.Close();

        }
        public DataTable getTable(string selectCMD, string tablename)
        {
            try
            {
                cnn = new OleDbConnection(cs);
                OleDbDataAdapter da = new OleDbDataAdapter(selectCMD, cnn);
                ds = new DataSet();
                da.Fill(ds, tablename);
                return ds.Tables[tablename];
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
