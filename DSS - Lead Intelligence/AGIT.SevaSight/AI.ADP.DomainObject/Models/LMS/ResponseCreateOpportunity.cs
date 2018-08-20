using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.ADP.DomainObject.Models
{
    /// <summary>
    /// Leads Model
    /// </summary>
    public class ResponseCreateOpportunity
    {
        /// <summary>
        /// success
        /// </summary>
        public bool success;
        /// <summary>
        /// message
        /// </summary>
        public string message;
        

    }

    /// <summary>
    /// ResponseCreateLeads
    /// </summary>
    public class CreateOpportunitys
    {
        /// <summary>
        /// collCreateLeads
        /// </summary>
        public List<ResponseCreateOpportunity> collCreateLeads;
    }

}