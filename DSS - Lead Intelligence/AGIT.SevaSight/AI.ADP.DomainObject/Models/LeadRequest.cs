using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace AI.ADP.DomainObject.Models
{
    /// <summary>
    /// Leads Model
    /// </summary>
    public class LeadRequest
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public string KTP { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public string Jenis9_Now { get; set; }
        public string Series_Now { get; set; }
        public string Jenis9_Next { get; set; }
        public string Series_Next { get; set; }
        public string TanggalBeliSelanjutnya { get; set; }
        public string SalesPerson { get; set; }
        public int statusLead { get; set; }
    }

 
}