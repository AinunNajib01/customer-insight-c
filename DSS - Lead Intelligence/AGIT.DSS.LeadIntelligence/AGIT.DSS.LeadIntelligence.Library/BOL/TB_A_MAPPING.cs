using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class TB_A_MAPPING : ApplicationObject, IApplicationObject
    {
        public Int32 ID { get; set; }
        public String ExcelColumn { get; set; }
        public String DatabaseField { get; set; }
        public String DateFormat { get; set; }
        public Boolean UniqueKey { get; set; }
        public Boolean Mandatory { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Boolean RowStatus { get; set; }

        public virtual bool Load(IDataReader dr)
        {
            this.IsValid = false;

            this.ID = DBUtil.GetIntField(dr, "ID");
            this.ExcelColumn = DBUtil.GetCharField(dr, "ExcelColumn");
            this.DatabaseField = DBUtil.GetCharField(dr, "DatabaseField");
            this.DateFormat = DBUtil.GetCharField(dr, "DateFormat");
            this.UniqueKey = DBUtil.GetBoolField(dr, "UniqueKey");
            this.Mandatory = DBUtil.GetBoolField(dr, "Mandatory");
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
