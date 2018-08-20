using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AI.ADP.UpdateOpportunity.Models
{
    public partial class MessageModel
    {
        public string message { get; set; }
        public bool success { get; set; }
        public List<ResponseGetOrderStatus> response {get;set;}
        public Dictionary<string, string> errors { get; set; }


    }


}