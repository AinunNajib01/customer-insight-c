using System;
using System.Collections.Generic;
using AI.ADP.DomainObject;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using ExcelLibrary.Office.Excel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DateTime dateTo = DateTime.Now;
                TimeSpan difftime = dateTo - dateTo.AddMinutes(2);
                DateTime dateFrom = DateTime.Now - difftime;
                List<Lead> leadlist = new List<Lead>();
                var connection = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

                #region Select Query
                string SelectQuery =  @"
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
        WHERE isNull(SendEmailStatus,0) = 0";
                #endregion

                using (SqlConnection myConnection = new SqlConnection(connection))
                {
                    string oString = SelectQuery;
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
                {
                    generateExcel(leadlist);
                    //generateExcelOleDB(leadlist);
                    updateSentEmailStatus(leadlist);
                }


            }
            catch (Exception ex)
            {
            }
        }

        private static void updateSentEmailStatus(List<Lead> leadlist)
        {
            var connection = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            
            using (SqlConnection myConnection = new SqlConnection(connection))
            {
                myConnection.Open();
                foreach (Lead lead in leadlist)
                {
                    string oString = @" UPDATE [dbo].[LMS_Leads]
                                    SET SendEmailStatus = 1
                                    WHERE SourceSystemNo = '{0}'
                                ";
                    oString = String.Format(oString, lead.SourceSystemNo);
                    SqlCommand oCmd = new SqlCommand(oString, myConnection);
                    
                    oCmd.ExecuteNonQuery();
                   
                }
                myConnection.Close();
            }

        }

        private static void generateExcel(List<Lead> leadlist)
        {
            ExcelOLEDB xlLead = new ExcelOLEDB("C:\\Leads\\leaddata_"+DateTime.Now.ToString("yyyyMMdd_hhmm")+".xls");
            Workbook xlWorkBook = new Workbook();
            Worksheet xlWorkSheet = new Worksheet("Sheet1");
            xlLead.Conn.Close();

            xlWorkSheet.Cells[0, 0] = new Cell("LeadNo");
            xlWorkSheet.Cells[0, 1] = new Cell("Title");
            xlWorkSheet.Cells[0, 2] = new Cell("LeadName");
            xlWorkSheet.Cells[0, 3] = new Cell("Email");
            xlWorkSheet.Cells[0, 4] = new Cell("TelephoneNo");
            xlWorkSheet.Cells[0, 5] = new Cell("Variant");
            xlWorkSheet.Cells[0, 6] = new Cell("VariantType");
            xlWorkSheet.Cells[0, 7] = new Cell("SalesSourceCategory");
            xlWorkSheet.Cells[0, 8] = new Cell("SalesSource");
            xlWorkSheet.Cells[0, 9] =new Cell( "Address");
            xlWorkSheet.Cells[0, 10] =new Cell( "City");
            xlWorkSheet.Cells[0, 11] =new Cell( "BusinessArea");
            xlWorkSheet.Cells[0, 12] =new Cell( "Status");
            xlWorkSheet.Cells[0, 13] =new Cell( "LeadType");
            xlWorkSheet.Cells[0, 14] =new Cell( "PreferredDateToCall");
            xlWorkSheet.Cells[0, 15] =new Cell( "CustomerNo");
            xlWorkSheet.Cells[0, 16] =new Cell( "Salesman No");
            xlWorkSheet.Cells[0, 17] =new Cell( "DropReason");
            xlWorkSheet.Cells[0, 18] =new Cell( "DropReasonDescription");
            xlWorkSheet.Cells[0, 19] =new Cell( "SourceSystem");
            xlWorkSheet.Cells[0, 20] = new Cell("OrderNo");

            int i = 1;
            foreach (Lead lead in leadlist)
            {
                //LeadNo";
                xlWorkSheet.Cells[i, 0] = new Cell(""); //ngga ada data dari CRM
                //title
                xlWorkSheet.Cells[i, 1] = new Cell(""); //ngga ada data dari CRM
                //leadname
                xlWorkSheet.Cells[i, 2] = new Cell(lead.Name1);
                //email
                xlWorkSheet.Cells[i, 3] = new Cell(lead.Email1);
                //telephoneno
                xlWorkSheet.Cells[i, 4] = new Cell(lead.Phone1);
                //variant
                xlWorkSheet.Cells[i, 5] = new Cell(lead.Notes1);
                //variantype
                xlWorkSheet.Cells[i, 6] = new Cell("");
                //salesroucecategory
                xlWorkSheet.Cells[i, 7] = new Cell(""); //ngga ada data dari CRM
                //salessource
                xlWorkSheet.Cells[i, 8] = new Cell(""); //ngga ada data dari CRM
                //address
                xlWorkSheet.Cells[i, 9] = new Cell(lead.Address1);
                //city
                xlWorkSheet.Cells[i, 10] = new Cell("");
                //business area
                xlWorkSheet.Cells[i, 11] = new Cell("T053");//ngga ada data dari CRM
                //status
                xlWorkSheet.Cells[i, 12] = new Cell("1");//ngga ada data dari CRM
                //leadtype
                xlWorkSheet.Cells[i, 13] = new Cell("6");//ngga ada data dari CRM
                //PreferredDateToCall
                xlWorkSheet.Cells[i, 14] = new Cell(lead.PreferredDateToContacted.ToString("g"));
                //CustomerNo
                xlWorkSheet.Cells[i, 15] = new Cell("");//ngga ada data dari CRM
                //Salesman No
                xlWorkSheet.Cells[i, 16] = new Cell("");//ngga ada data dari CRM
                //DropReason
                xlWorkSheet.Cells[i, 17] = new Cell("");//ngga ada data dari CRM
                //DropReasonDescription
                xlWorkSheet.Cells[i, 18] = new Cell("");//ngga ada data dari CRM
                //SourceSystem
                xlWorkSheet.Cells[i, 19] = new Cell("ADP");
                //OrderNo
                xlWorkSheet.Cells[i, 20] = new Cell(lead.SourceSystemNo);
                i++;
            }

            xlWorkBook.Worksheets.Add(xlWorkSheet);
            xlWorkBook.Save(xlLead.FilePath);

            SendMail("niwayan.sita@ai.astra.co.id", "[TEST]SEND LEAD DATA", "Plz Find the attachment", xlLead.FilePath);

        }

    //private static void generateExcelOleDB(List<Lead> leadList)
    //{
    //    //Create the data set and table
    //    DataSet ds = new DataSet("New_DataSet");
    //    DataTable dt = new DataTable("New_DataTable");

    //    //Set the locale for each
    //    ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;
    //    dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

    //    //Open a DB connection (in this example with OleDB)
    //    ExcelOLEDB leadExcel = new ExcelOLEDB("D:\\Leads\\leaddata_"+DateTime.Now.ToString("yyyyMMdd_hhmm")+".xls");
    //    leadExcel.FilePath
    //    Workbook workbook = new Workbook();
    //    Worksheet worksheet = new Worksheet("Sheet1");
    //   OleDbCommand cmd = new OleDbCommand(createExcelTable, leadExcel.Conn);
    //    OleDbDataAdapter adptr = new OleDbDataAdapter();
    //    adptr.SelectCommand = cmd;
    //    adptr.Fill(dt);
    //    foreach (Lead lead in leadList)
    //    {
    //        string sql = @"INSERT INTO [Sheet1] 
    //            (LeadName,
    //             Email,
    //             TelephoneNo,
    //             Variant,
    //             Address,
    //             SalesSourceCategory,
    //             BusinessArea,
    //             Status,
    //             LeadType,
    //             PreferredDateToCall,
    //             SourceSystem,
    //             OrderNo
    //            ) 
    //            VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')";
    //        sql = string.Format(sql, lead.Name1, lead.Email1, lead.Phone1, lead.Notes1, lead.Address1, "5", "T053", "1", "6", lead.PreferredDateToContacted.ToString(), "ADP", lead.SourceSystemNo);
    //        cmd = new OleDbCommand(sql, leadExcel.Conn);
    //        adptr.SelectCommand = cmd;
    //        adptr.Fill(dt);
    //    }

    //    //Add the table to the data set
    //    ds.Tables.Add(dt);
    //    leadExcel.Conn.Close();
    //    //Here's the easy part. Create the Excel worksheet from the data set
    //    ExcelLibrary.DataSetHelper.CreateWorkbook(leadExcel.FilePath, ds);

    //    SendMail("refan.andros@ag-it.com", "[IGNORE]TEST LEAD DATA", "Please find the attachment", leadExcel.FilePath);



    //}
    public static void SendMail(string recipient, string subject, string body, string attachmentFilename)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("existingadp01@gmail.com", "rendycakep");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("existingadp01@gmail.com");

            // setup up the host, increase the timeout to 5 minutes
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;
            smtpClient.Timeout = (60 * 5 * 1000);

            message.From = fromAddress;
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;
            message.To.Add(recipient);

            if (attachmentFilename != null)
                message.Attachments.Add(new Attachment(attachmentFilename));

            smtpClient.Send(message);
        }
    }
}
