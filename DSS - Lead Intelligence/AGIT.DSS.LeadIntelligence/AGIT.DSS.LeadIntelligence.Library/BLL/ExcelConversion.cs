using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class ExcelConversion
    {
        public static string UploadSalesOrder()
        {
            #region Hardcode

            string dealerID = "3";
            //string customerID = "";
            string loginUser = "DSSCI_2";
            int userID = 3;

            //string file = ConfigurationManager.AppSettings["filePath"];
            //string fileName = Path.GetFileName(file);

            string _return = string.Empty;

            var files = FilesLogic.GetAll();
            //var file = getFiles.Where(i => i.Status = 0).Select(a => a.DatabaseField).ToList();
            foreach (var item in files.Where(x => x.Status == 0))
            {
                string file = item.Filepath;
                string fileName = Path.GetFileName(file);
                Int32 id = item.ID;
                #endregion


                TB_A_LOG log = new TB_A_LOG();
                log.UserID = userID;
                log.Process = "Upload Data Sales Order";
                log.Filename = fileName;
                log.Start = DateTime.Now;
                log.CreatedBy = loginUser;
                log.CreatedDate = DateTime.UtcNow;
                log.RowStatus = true;
                LogLogic.Insert(ref log);

                int rowInReading = 0;
                string columnInReading = "0";

                try
                {
                    #region Get mapping data

                    var mappingData = MappingLogic.GetAll();

                    var mappingKey = mappingData.Where(i => i.UniqueKey == true).Select(a => a.DatabaseField).ToList();

                    #endregion

                    #region Read Excel file

                    string filepath = file;

                    var workbook = new XLWorkbook(filepath);
                    var worksheet = workbook.Worksheet(1);

                    int lastRow = worksheet.LastRowUsed().RowNumber();

                    List<Dictionary<string, object>> listData = new List<Dictionary<string, object>>();

                    for (int row = 2; row <= lastRow; row++)
                    {
                        rowInReading = row;

                        //if(row == 2262)
                        //{

                        //}

                        Dictionary<string, object> dicData = new Dictionary<string, object>();

                        foreach (var map in mappingData)
                        {
                            columnInReading = map.DatabaseField;

                            var dataExcel = worksheet.Cell(row, CommonHelper.ColumnExcelToNumber(map.ExcelColumn)).Value;

                            if (dataExcel.GetType().Name == "String") if (dataExcel.ToString().Length > 0)
                                {
                                    dataExcel = dataExcel.ToString().Trim();
                                    if (dataExcel.ToString().Substring(0, 1) == "'")
                                        dataExcel = dataExcel.ToString().Remove(0, 1);
                                    dataExcel = dataExcel.ToString().Replace("'", "''");
                                }

                            if (!string.IsNullOrEmpty(map.DateFormat)) dataExcel = CommonHelper.ConvertToDateTime(map.DateFormat, dataExcel.ToString());

                            if (dataExcel.ToString().Length == 1 && dataExcel.ToString() == "N") dataExcel = string.Empty;

                            if (map.Mandatory == true && String.IsNullOrEmpty(dataExcel.ToString()))
                            {
                                log.Status = "Error";
                                log.Note = String.Format("Error Message: Column '{0}' on row {1} must be filled.", columnInReading, rowInReading);

                                goto JumpHere;
                            }

                            dicData.Add(map.DatabaseField, dataExcel);
                        }

                        listData.Add(dicData);
                    }

                    #endregion

                    #region Insert into database

                    SalesOrderLogic.Insert(listData, dealerID, loginUser, log.ID, log.RowStatus, mappingKey);

                    #endregion

                    log.Status = "Success";
                    log.Note = string.Empty;
                    log.RowStatus = true;
                }

                catch (Exception ex)

                {
                    log.Status = "Error";
                    log.Note = String.Format("Error Message: {0}, Row in reading: {1}, Column in reading: {2}", ex.Message, rowInReading, columnInReading);
                    log.RowStatus = false;
                }

                JumpHere: { }

                log.Finish = DateTime.Now;
                LogLogic.UpdateLog(log);
                Int16 status = 1;
                FilesLogic.UpdateFiles(id, status);
                return _return;
            }
            return _return;
        }
    }
}
