using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.ADP.UpdateOpportunity.Models
{
  
        /// <summary>
        /// RequestGetOrderStatus
        /// </summary>
        public class RequestUpdateOpportunity
        {
        public string OpportunityId { get; set; }

        public int Status { get; set; }
        public string StatusReason { get; set; }
        public string Description { get; set; }
        public string SalesPersonName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        

    }

     
  
}