using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace AI.ADP.DomainObject.Models
{
    /// <summary>
    /// Leads Model
    /// </summary>
    public class LeadResponse
    {
        public int ID { get; set; }
        public string Nama { get; set; }
        public string NoKTP { get; set; }
        public string NoTelp { get; set; }
        public string Email { get; set; }
        public DateTime TanggalCetak { get; set; }
        public string TipeMotor { get; set; }
        public string Jenis6 { get; set; }
        public string Jenis6Selanjutnya { get; set; }
        public DateTime TanggalBeliSelanjutnya { get; set; }
        public string SalesPerson { get; set; }
    }

 
}