using AGIT.DSS.LeadIntelligence.Library.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class FilesLogic : ApplicationCollection<TB_FILES, DBHelper>
    {
        public static List<TB_FILES> GetAll()
        {
            DBHelper sp = new DBHelper(String.Format(@"SELECT * FROM {0} WHERE RowStatus = 1", Table));

            var data = GetApplicationCollection(sp);

            return data;
        }

        public static bool UpdateFiles(Int32 id, Int16 status)
        {
            bool WasSaved = false;
         
            DBHelper sp = new DBHelper(String.Format(@"UPDATE {0} SET Status = @Status WHERE ID = @ID", status, id));
            sp.AddParameter("ID", id);
            sp.AddParameter("Status", status);

            if (sp.ExecuteNonQuery() == 0)
                WasSaved = true;
                
            return WasSaved;
        }
    }
}
