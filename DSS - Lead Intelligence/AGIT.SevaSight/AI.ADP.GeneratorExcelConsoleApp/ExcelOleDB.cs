using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class ExcelOLEDB
    {
        OleDbConnection conn;
        private string filePath = "";

        public string FilePath
        {
            get { return filePath; }
        }

        public ExcelOLEDB(int version, string filePath)
        {
            createConnection(version, filePath);
            this.filePath = filePath;
        }

        private void createConnection(int version, string filePath)
        {
            this.filePath = filePath;
            string connectionString = "Provider=Microsoft.ACE.OLEDB.{0};Data Source={1};Extended Properties=Excel {0} Xml";
            connectionString = String.Format(connectionString, version.ToString() + ".0", filePath);

            conn = new OleDbConnection(connectionString);
            conn.Open();
        }

        public ExcelOLEDB(string filePath)
        {
            try
            {
                createConnection(12, filePath);
                return;
            }
            catch (Exception ex)
            {

            }
            try
            {
                createConnection(14, filePath);
                return;
            }
            catch (Exception ex)
            {

            }
            try
            {
                createConnection(15, filePath);
                return;
            }
            catch (Exception ex)
            {

            }
        }

        public OleDbConnection Conn
        {
            get { return conn; }
        }

        public List<string> GetSheetList()
        {
            List<string> result = new List<string>();
            // get sheets list into combobox

            DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string nameValue = dt.Rows[i].ItemArray[2].ToString();

                result.Add(nameValue);

            }
            return result;
        }


        public List<string> GetColumnList(string sheetName)
        {
            List<string> result = new List<string>();

            // get sheets list into combobox

            DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new String[] { null, null, sheetName, null });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string nameValue = dt.Rows[i].ItemArray[3].ToString();

                result.Add(nameValue);

            }
            return result;
        }
    }
}
