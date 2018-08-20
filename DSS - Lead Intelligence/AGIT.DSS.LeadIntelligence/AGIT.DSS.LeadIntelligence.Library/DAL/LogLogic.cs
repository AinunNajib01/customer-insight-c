using AGIT.DSS.LeadIntelligence.Library.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class LogLogic : ApplicationCollection<TB_A_LOG, DBHelper>
    {
        public static bool Insert(ref TB_A_LOG data)
        {
            bool WasSaved = false;

            DBHelper sp = new DBHelper(String.Format(@"INSERT INTO {0} (UserID, Process, Filename, Start, CreatedBy, CreatedDate)
                        VALUES (@UserID, @Process, @Filename, @Start, @CreatedBy, @CreatedDate); SELECT @ID = @@IDENTITY", Table));
            sp.AddParameter("UserID", data.UserID);
            sp.AddParameter("Process", data.Process);
            sp.AddParameter("Filename", data.Filename);
            sp.AddParameter("Start", data.Start);
            sp.AddParameter("CreatedBy", data.CreatedBy);
            sp.AddParameter("CreatedDate", data.CreatedDate);
            sp.AddParameter("ID", data.ID, ParameterDirection.Output);

            if (sp.ExecuteNonQuery() == 0)
                WasSaved = true;

            var id = sp.ParameterValue("ID");
            data.ID = (long)id;

            return WasSaved;
        }

        public static bool UpdateLog(TB_A_LOG data)
        {
            bool WasSaved = false;

            if (data.Status == "Error")
            {
                DBHelper sp = new DBHelper(String.Format(@"UPDATE {0} SET Finish = @Finish, Status = @Status, Note = @Note WHERE ID = @ID", Table));
                sp.AddParameter("ID", data.ID);
                sp.AddParameter("Finish", data.Finish);
                sp.AddParameter("Status", data.Status);
                sp.AddParameter("Note", data.Note);

                if (sp.ExecuteNonQuery() == 0)
                    WasSaved = true;
            }
            else
            {
                DBHelper sp = new DBHelper(String.Format(@"UPDATE {0} SET Finish = @Finish, Status = @Status, Note = @Note, RowStatus = @RowStatus WHERE ID = @ID", Table));
                sp.AddParameter("ID", data.ID);
                sp.AddParameter("Finish", data.Finish);
                sp.AddParameter("Status", data.Status);
                sp.AddParameter("Note", data.Note);
                sp.AddParameter("RowStatus", data.RowStatus);

                if (sp.ExecuteNonQuery() == 0)
                    WasSaved = true;
            }
            
            return WasSaved;
        }
    }
}
