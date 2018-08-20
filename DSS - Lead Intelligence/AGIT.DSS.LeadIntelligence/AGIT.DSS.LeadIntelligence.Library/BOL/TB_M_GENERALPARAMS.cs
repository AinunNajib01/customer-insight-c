using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class TB_M_GENERALPARAMS : ApplicationObject, IApplicationObject
    {
        public Int32 ID { get; set; }
        public String Module { get; set; }
        public String Code { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Boolean RowStatus { get; set; }

        public virtual bool Load(IDataReader dr)
        {
            this.IsValid = false;

            this.ID = DBUtil.GetIntField(dr, "ID");
            this.Module = DBUtil.GetCharField(dr, "Module");
            this.Code = DBUtil.GetCharField(dr, "Code");
            this.Name = DBUtil.GetCharField(dr, "Name");
            this.Description = DBUtil.GetCharField(dr, "Description");
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
