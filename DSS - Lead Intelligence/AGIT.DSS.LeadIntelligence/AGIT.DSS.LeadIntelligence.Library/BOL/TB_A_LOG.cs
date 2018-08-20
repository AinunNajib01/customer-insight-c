using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class TB_A_LOG : ApplicationObject, IApplicationObject
    {
        public Int64 ID { get; set; }
        public Int32 UserID { get; set; }
        public String Process { get; set; }
        public String Filename { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public String Status { get; set; }
        public String Note { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Boolean RowStatus { get; set; }

        public virtual bool Load(IDataReader dr)
        {
            this.IsValid = false;

            this.ID = DBUtil.GetLongField(dr, "ID");
            this.UserID = DBUtil.GetIntField(dr, "UserID");
            this.Process = DBUtil.GetCharField(dr, "Process");
            this.Filename = DBUtil.GetCharField(dr, "Filename");
            this.Start = DBUtil.GetDateTimeField(dr, "Start");
            this.Finish = DBUtil.GetDateTimeField(dr, "Finish");
            this.Status = DBUtil.GetCharField(dr, "Status");
            this.Note = DBUtil.GetCharField(dr, "Note");
            this.CreatedBy = DBUtil.GetCharField(dr, "CreatedBy");
            this.CreatedDate = DBUtil.GetDateTimeField(dr, "CreatedDate");
            this.LastModifiedBy = DBUtil.GetCharField(dr, "LastModifiedBy");
            this.LastModifiedDate = DBUtil.GetDateTimeField(dr, "LastModifiedDate");
            this.RowStatus = DBUtil.GetBoolField(dr, "RowStatus");

            this.IsValid = true;
            return this.IsValid;
        }
    }
}
