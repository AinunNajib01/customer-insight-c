using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class ApplicationTables
    {
        /// <summary>
        /// Correspondence between the C# classes and database tables.
        /// </summary>
        private static Hashtable _Names = new Hashtable()
        {
            {typeof(TB_R_SALES_ORDER).ToString(), "dbo.TB_R_SALES_ORDER"},
            {typeof(TB_M_GENERALPARAMS).ToString(), "dbo.TB_M_GENERALPARAMS"},
            {typeof(TB_A_MAPPING).ToString(), "dbo.TB_A_MAPPING"},
            {typeof(TB_A_LOG).ToString(), "dbo.TB_A_LOG"},
            {typeof(TB_M_CUSTOMER).ToString(), "dbo.TB_M_CUSTOMER" },
            {typeof(TB_FILES).ToString(), "dbo.TB_FILES" }
        };

        /// <summary>
        /// Gets Hashtable of correspondence between the C# classes and database tables.
        /// </summary>
        protected static Hashtable Names { get { return _Names; } }

        /// <summary>
        /// Gets table name by class type.
        /// </summary>
        public static string TableName(object ObjectType)
        {
            return (string)Names[ObjectType.GetType().ToString()];
        }

        /// <summary>
        /// Gets table name by type name.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string TableByTypeName(string Name)
        {
            return (string)Names[Name];
        }
    }
}
