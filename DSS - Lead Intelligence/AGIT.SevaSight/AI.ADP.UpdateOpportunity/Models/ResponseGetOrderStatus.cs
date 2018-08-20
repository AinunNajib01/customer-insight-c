using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.ADP.UpdateOpportunity.Models
{
  
        /// <summary>
        /// ResponseGetOrderStatus
        /// </summary>
        /// 

        public class ResponseGetOrderStatuses
    {
        public List<ResponseGetOrderStatus> ResponseOrderStatuses { get; set; }
    }
        public class ResponseGetOrderStatus
    {
        public string OrderNo { get; set; }

        public string TestDriveTime { get; set; }
        public string TestDriveStatus { get; set; }
        public string SalesmanName { get; set; }
        public string ModelName { get; set; }
        public string SalesSource { get; set; }
        public string DropReason { get; set; }
        public string SPKCreatedTime { get; set; }
        public string DPTime { get; set; }
        public string DPAmount { get; set; }
        public string FullPaymentTime { get; set; }
        public string FullPaymentAmount { get; set; }
        public string PlanDeliveryDate { get; set; }
        public string ActualDeliveryDate { get; set; }
        public string STNKFinishedDate { get; set; }
        public string BPKBFinishedDate { get; set; }
        public string SPKCancellationTime { get; set; }
        public string SalesOrderCancellationTime { get; set; }
        public string BillingCancellationTime { get; set; }
        public string BillingReturnTime { get; set; }


    }

     
     
    
}