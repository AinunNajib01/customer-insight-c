using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{   
    public class CustomerLogic
    {
        //public 

        public static bool Insert(List<Dictionary<string, object>> listDicResult, string dealerID, string user, Int64 logID, bool RowStatus, List<string> mappingKey)
        {
            bool wasSaved = false;

            StringBuilder finalQuery = new StringBuilder();

            string queryField = string.Empty;
            string queryValue = string.Empty;

            string queryDelete = string.Empty;

            Dictionary<string, object> dicDelete = null;

            foreach (var dicResult in listDicResult)
            {
                string lastQuery = string.Empty;

                try
                {
                    bool newLine = true;

                    dicDelete = new Dictionary<string, object>();

                    foreach (var result in dicResult)
                    {
                        if (newLine == true)
                        {
                            queryField = String.Format("INSERT INTO TB_M_CUSTOMER ([{0}]", result.Key);
                            if (result.Value.ToString() == "NULL") queryValue = String.Format("VALUES ({0}", result.Value);
                            else queryValue = String.Format("VALUES ('{0}'", result.Value);

                            newLine = false;
                        }
                        else
                        {
                            queryField = String.Format("{0}, [{1}]", queryField, result.Key);
                            if (result.Value.ToString() == "NULL") queryValue = String.Format("{0}, {1}", queryValue, result.Value);
                            else queryValue = String.Format("{0}, '{1}'", queryValue, result.Value);
                        }

                        if (mappingKey.Any(i => i == result.Key))
                            dicDelete.Add(result.Key, result.Value);
                    }

                    StringBuilder whereDelete = new StringBuilder();
                    bool isFirstColumn = true;                    
                    foreach (var data in dicDelete)
                    {
                        if (isFirstColumn) whereDelete.Append(String.Format("[{0}] = '{1}'", data.Key, data.Value));
                        else whereDelete.Append(String.Format(" AND [{0}] = '{1}'", data.Key, data.Value));
                        isFirstColumn = false;
                    }

                    queryDelete = String.Format("DELETE FROM TB_M_CUSTOMER WHERE {0}", whereDelete.ToString());

                    queryField = String.Format("{0}, DealerID, LogID, RowStatus, CreatedBy)", queryField);
                    queryValue = String.Format("{0}, '{1}', '{2}', '{3}', '{4}' )", queryValue, dealerID, logID, RowStatus, user);

                    //finalQuery.Append(String.Format("{0} {1}", queryField, queryValue));

                    string query = String.Format("{0} {1} {2}", queryDelete, queryField, queryValue);
                    lastQuery = query;

                    DBHelper sp = new DBHelper(query);
                    if (sp.ExecuteNonQuery() == 0)
                        wasSaved = true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //DBHelper sp = new DBHelper(finalQuery.ToString());
            //if (sp.ExecuteNonQuery() == 0)
            //    wasSaved = true;

            return wasSaved;
        }
    }
}
