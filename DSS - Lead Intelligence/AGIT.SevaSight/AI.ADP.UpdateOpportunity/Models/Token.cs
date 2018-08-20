using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AI.ADP.UpdateOpportunity.Models
{
    public partial class Token
    {
        public int Acknowledge { get; set; }

        public string Message { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }
        public string AccessToken { get; set; }


    }


}