using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGIT.DSS.LeadIntelligence.Library
{
    public class CommonHelper
    {
        public static int ColumnExcelToNumber(string column)
        {
            return column.Select((c, i) => ((c - 'A' + 1) * ((int)Math.Pow(26, column.Length - i - 1)))).Sum();
        }

        public static string ConvertToDateTime(string dateFormat, string value)
        {
            string _return = "NULL";

            try { _return = DateTime.ParseExact(value, dateFormat, CultureInfo.InvariantCulture).ToString("yyyyMMdd"); }
            catch { }

            return _return;
        }
    }
}
