using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.ADP.UpdateOpportunity.Models
{
  
        /// <summary>
        /// RequestGetOrderStatus
        /// </summary>
        public class RequestOrderStatus
         {
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
         }

        public class RetrieveOrderStatus
        {
        public RequestOrderStatus RequestOrderStatus { get; set; }
        public string Password { get; set; }
        public string CompanyCode { get; set; }
        public string Action { get; set; }
        public string HeadOfficeCode { get; set; }
        public string UserID { get; set; }
        public int ApplicationCode { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string AccessToken { get; set; }
    }

     
  
}