using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AI.ADP.DomainObject.Models
{
    public partial class MessageModel
    {
        public string message { get; set; }
        public bool success { get; set; }
        public string errors { get; set; }


    }


}
