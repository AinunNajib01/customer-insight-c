using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class TB_FILES : ApplicationObject, IApplicationObject
    {
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public String Filepath { get; set; }
        public Int16 Status { get; set; }
        public String Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual bool Load(IDataReader dr)
        {
            this.IsValid = false;
            
            this.ID = DBUtil.GetIntField(dr, "id");
            this.Title = DBUtil.GetCharField(dr, "title");
            this.Filepath = DBUtil.GetCharField(dr, "filepath");
            this.Status = DBUtil.GetSmallIntField(dr, "status");
            this.CreatedDate = DBUtil.GetDateTimeField(dr, "created_at");
            this.LastModifiedDate = DBUtil.GetDateTimeField(dr, "updated_at");

            this.IsValid = true;
            return this.IsValid;
        }
    }
}
