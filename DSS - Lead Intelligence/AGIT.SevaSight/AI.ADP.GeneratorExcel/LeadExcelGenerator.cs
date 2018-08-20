using System;
using System.Collections.Generic;
using AI.ADP.DomainObject;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Timers;

namespace AI.ADP.GeneratorExcel
{
    
    public partial class ExcelGenerator : ServiceBase
    {
        private Timer _timer;
        public ExcelGenerator()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(15 * 60 * 1000);  // 15 minutes expressed as milliseconds
            _timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            _timer.AutoReset = true;
            _timer.Start();

        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();

        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                DateTime dateTo = DateTime.Now;
                TimeSpan difftime = dateTo - dateTo.AddMinutes(15);
                DateTime dateFrom = DateTime.Now - difftime;
                List<Lead> leadlist = new List<Lead>(); 
                var connection = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                using (SqlConnection myConnection = new SqlConnection(connection))
                {
                    string oString = @"
                            SELECT
                          [TestDriveTime]
                          ,[SalesmanName]
                          ,[ModelName]
                          ,[SalesSource]
                          ,[DropReason]
                          ,[SPKCreatedTime]
                          ,[DPTime]
                          ,[DPAmount]
                          ,[FullPaymentTime]
                          ,[FullPaymentAmount]
                          ,[PlanDeliveryDate]
                          ,[ActualDeliveryDate]
                          ,[STNKFinishedDate]
                          ,[BPKBFinishedDate]
                          ,[SPKCancellationTime]
                          ,[SalesOrderCancellationTime]
                          ,[BillingCancellationTime]
                          ,[BillingReturnTime]
                          ,[CreatedOn]
                          ,[CreatedBy]
                          ,[ModifiedOn]
                          ,[ModifiedBy]
                          ,[RowStatus]
                          ,[Title]
                          ,[Name1]
                          ,[Name2]
                          ,[Phone1]
                          ,[Phone2]
                          ,[Email1]
                          ,[Email2]
                          ,[Address1]
                          ,[Address2]
                          ,[Area]
                          ,[City]
                          ,[Postal]
                          ,[Kelurahan]
                          ,[Kecamatan]
                          ,[Province]
                          ,[PreferredDateToContacted]
                          ,[PreferredBusinessArea]
                          ,[Notes1]
                          ,[ProspectVariant]
                          ,[Program]
                          ,[Score]
                          ,[LeasingCompany]
                          ,[LeasingInvoiceDate]
                          ,[LeasingDueDate]
                          ,[BusinessArea]
                          ,[Company]
                          ,[VerifiedCustomerSystem]
                          ,[VerifiedCustomerNo]
                          ,[VerifiedCustomerName]
                          ,[DropReasonCode]
                          ,[DropReasonDescription]
                          ,[VerificationResultCode]
                          ,[TestDriveStatus]
                          ,[SourceSystem]
                          ,[SourceSystemNo]
                            FROM[dbo].[LMS_Leads]
                            WHERE CreatedOn BETWEEN '{0}' AND '{1}'";
                    oString = String.Format(oString, dateFrom.ToString(), dateTo.ToString());
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    myConnection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            Lead lead = new Lead();
                            lead.SourceSystemNo = oReader["SourceSystemNo"].ToString();
                            lead.Name1 = oReader["Name1"].ToString();
                            lead.Phone1 = oReader["Phone1"].ToString();
                            lead.Address1 = oReader["Address1"].ToString();
                            lead.PreferredDateToContacted = (DateTime)oReader["PreferredDateToContacted"];
                            lead.Notes1 = oReader["Notes1"].ToString();
                            leadlist.Add(lead);
                        }

                        myConnection.Close();
                    }
                }

                if (leadlist.Count > 0)
                    generateExcel(leadlist);



            }
            catch (Exception ex)
            {
            }
        }


        private static void generateExcel(List<Lead> leadlist)
        {
            Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null) { return; }
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "LeadNo";
            xlWorkSheet.Cells[1, 2] = "Title";
            xlWorkSheet.Cells[1, 3] = "LeadName";
            xlWorkSheet.Cells[1, 4] = "Email";
            xlWorkSheet.Cells[1, 5] = "TelephoneNo";
            xlWorkSheet.Cells[1, 6] = "Variant";
            xlWorkSheet.Cells[1, 7] = "VariantType";
            xlWorkSheet.Cells[1, 8] = "SalesSourceCategory";
            xlWorkSheet.Cells[1, 9] = "SalesSource";
            xlWorkSheet.Cells[1, 10] = "Address";
            xlWorkSheet.Cells[1, 11] = "City";
            xlWorkSheet.Cells[1, 12] = "BusinessArea";
            xlWorkSheet.Cells[1, 13] = "Status";
            xlWorkSheet.Cells[1, 14] = "LeadType";
            xlWorkSheet.Cells[1, 15] = "PreferredDateToCall";
            xlWorkSheet.Cells[1, 16] = "CustomerNo";
            xlWorkSheet.Cells[1, 17] = "Salesman No";
            xlWorkSheet.Cells[1, 18] = "DropReason";
            xlWorkSheet.Cells[1, 19] = "DropReasonDescription";
            xlWorkSheet.Cells[1, 20] = "SourceSystem";
            xlWorkSheet.Cells[1, 21] = "OrderNo";

            int i = 2;
            foreach (Lead lead in leadlist)
            {
                //LeadNo";
                xlWorkSheet.Cells[i, 1] = ""; //ngga ada data dari CRM
                //title
                xlWorkSheet.Cells[i, 2] = ""; //ngga ada data dari CRM
                //leadname
                xlWorkSheet.Cells[i, 3] = lead.Name1;
                //email
                xlWorkSheet.Cells[i, 4] = lead.Email1;
                //telephoneno
                xlWorkSheet.Cells[i, 5] = lead.Phone1;
                //variant
                xlWorkSheet.Cells[i, 6] = lead.Notes1;
                //variantype
                xlWorkSheet.Cells[i, 7] = "";
                //salesroucecategory
                xlWorkSheet.Cells[i, 8] = ""; //ngga ada data dari CRM
                //salessource
                xlWorkSheet.Cells[i, 9] = ""; //ngga ada data dari CRM
                //address
                xlWorkSheet.Cells[i, 10] = lead.Address1;
                //city
                xlWorkSheet.Cells[i, 11] = "";
                //business area
                xlWorkSheet.Cells[i, 12] = "T053";//ngga ada data dari CRM
                //status
                xlWorkSheet.Cells[i, 13] = "1";//ngga ada data dari CRM
                //leadtype
                xlWorkSheet.Cells[i, 14] = "6";//ngga ada data dari CRM
                //PreferredDateToCall
                xlWorkSheet.Cells[i, 15] = lead.PreferredDateToContacted;
                //CustomerNo
                xlWorkSheet.Cells[i, 16] = "";//lead.VerifiedCustomerNo;//ngga ada data dari CRM
                //Salesman No
                xlWorkSheet.Cells[i, 17] = "";//ngga ada data dari CRM
                //DropReason
                xlWorkSheet.Cells[i, 18] = "";//ngga ada data dari CRM
                //DropReasonDescription
                xlWorkSheet.Cells[i, 19] = "";//ngga ada data dari CRM
                //SourceSystem
                xlWorkSheet.Cells[i, 20] = "ADP";
                //OrderNo
                xlWorkSheet.Cells[i, 21] = lead.SourceSystemNo;
                i++;
            }


            xlWorkBook.SaveAs("D:\\Leads\\leadData " + DateTime.Now.ToString("yyyyMMdd_hhmm") + ".xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

        }
    }
}
