using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace AI.ADP.DomainObject.Models
{
    /// <summary>
    /// Leads Model
    /// </summary>
    public class ResponseGetLead
    {
        /// <summary>
        /// OrderNo
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// TestDriveTime
        /// </summary>
        public DateTime? TestDriveTime;
        /// <summary>
        /// TestDriveStatus
        /// </summary>
        public string TestDriveStatus;
        /// <summary>
        /// SalesmanName
        /// </summary>
        public string SalesmanName;
        /// <summary>
        /// ModelName
        /// </summary>
        public string ModelName;
        /// <summary>
        /// SalesSource
        /// </summary>
        public string SalesSource;
        /// <summary>
        /// DropReason
        /// </summary>
        public string DropReason;
        /// <summary>
        /// SPKCreatedTime
        /// </summary>
        public DateTime? SPKCreatedTime;
        /// <summary>
        /// DPTime
        /// </summary>
        public DateTime? DPTime;
        /// <summary>
        /// DPAmount
        /// </summary>
        public decimal DPAmount;
        /// <summary>
        /// FullPaymentTime
        /// </summary>
        public DateTime? FullPaymentTime;
        /// <summary>
        /// FullPaymentAmount
        /// </summary>
        public decimal FullPaymentAmount;
        /// <summary>
        /// PlanDeliveryDate
        /// </summary>
        public DateTime? PlanDeliveryDate;
        /// <summary>
        /// ActualDeliveryDate
        /// </summary>
        public DateTime? ActualDeliveryDate;
        /// <summary>
        /// STNKFinishedDate
        /// </summary>
        public DateTime? STNKFinishedDate;
        /// <summary>
        /// BPKBFinishedDate
        /// </summary>
        public DateTime? BPKBFinishedDate;
        /// <summary>
        /// SPKCancellationTime
        /// </summary>
        public DateTime? SPKCancellationTime;
        /// <summary>
        /// SalesOrderCancellationTime
        /// </summary>
        public DateTime? SalesOrderCancellationTime;
        /// <summary>
        /// BillingCancellationTime
        /// </summary>
        public DateTime? BillingCancellationTime;
        /// <summary>
        /// BillingReturnTime
        /// </summary>
        public DateTime? BillingReturnTime;
        public int OpportunityID;




    }

    /// <summary>
    /// ResponseCreateLeads
    /// </summary>
    public class ResposnseGetLeads
    {
        /// <summary>
        /// collCreateLeads
        /// </summary>
        public List<ResponseGetLead> collGetLead;
    }

}