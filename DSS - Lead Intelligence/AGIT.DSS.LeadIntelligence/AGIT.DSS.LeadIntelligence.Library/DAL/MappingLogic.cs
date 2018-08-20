using AGIT.DSS.LeadIntelligence.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class MappingLogic : ApplicationCollection<TB_A_MAPPING, DBHelper>
    {
        public static List<TB_A_MAPPING> GetAll()
        {
            DBHelper sp = new DBHelper(String.Format(@"SELECT * FROM {0} WHERE RowStatus = 1", Table));

            var data = GetApplicationCollection(sp);

            return data;
        }
    }
}
