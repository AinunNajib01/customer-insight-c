using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.ADP.DomainObject.Models
{
    /// <summary>
    /// Leads Model
    /// </summary>
    public class RequestCreateOpportunity
    {

        /// <summary>
        /// The opportunityID
        /// </summary>
        public string OpportunityID;
        /// <summary>
        /// Title
        /// </summary>
        public string Title;
        /// <summary>
        /// Name1
        /// </summary>
        public string Name1;
        /// <summary>
        /// Name2
        /// </summary>
        public string Name2;
        /// <summary>
        /// Phone1
        /// </summary>
        public string Phone1;
        /// <summary>
        /// Phone2
        /// </summary>
        public string Phone2;
        /// <summary>
        /// Email1
        /// </summary>
        public string Email1;
        /// <summary>
        /// Email2
        /// </summary>
        public string Email2;
        /// <summary>
        /// Address1
        /// </summary>
        public string Address1;
        /// <summary>
        /// Address2
        /// </summary>
        public string Address2;
        /// <summary>
        /// AreaCode
        /// </summary>
        public string Area;
        /// <summary>
        /// CityCode
        /// </summary>
        public string City;
        /// <summary>
        /// PostalCode
        /// </summary>
        public string Postal;
        /// <summary>
        /// KelurahanCode
        /// </summary>
        public string Kelurahan;
        /// <summary>
        /// KecamatanCode
        /// </summary>
        public string Kecamatan;
        /// <summary>
        /// ProvinceCode
        /// </summary>
        public string Province;
        /// <summary>
        /// PrefferedDateToContacted
        /// </summary>
        public string PreferredDateToContacted;
        /// <summary>
        /// PrefferedBusinessAreaCode
        /// </summary>
        public string PreferredBusinessAreaCode;
        /// <summary>
        /// Notes1
        /// </summary>
        public string Notes1;
        /// <summary>
        /// ProspectVariant
        /// </summary>
        public string ProspectVariant;
        /// <summary>
        /// Program
        /// </summary>
        public string Program;
        /// <summary>
        /// Score
        /// </summary>
        public string Score;
        /// <summary>
        /// LeasingCompany
        /// </summary>
        public string LeasingCompany;
        /// <summary>
        /// LeasingInvoiceDate
        /// </summary>
        public string LeasingInvoiceDate;
        /// <summary>
        /// LeasingDueDate
        /// </summary>
        public string LeasingDueDate;
        /// <summary>
        /// BusinessAreaCode
        /// </summary>
        public string BusinessAreaCode;
        /// <summary>
        /// CompanyCode
        /// </summary>
        public string CompanyCode;
        /// <summary>
        /// VerifiedCustomerSystem
        /// </summary>
        public string VerifiedCustomerSystem;
        /// <summary>
        /// VerifiedCustomerNo
        /// </summary>
        public string VerifiedCustomerNo;
        /// <summary>
        /// VerifiedCustomerName
        /// </summary>
        public string VerifiedCustomerName;
        /// <summary>
        /// DropReasonCode
        /// </summary>
        public string DropReasonCode;
        /// <summary>
        /// DropReasonDescription
        /// </summary>
        public string DropReasonDescription;
        /// <summary>
        /// VerificationResultCode
        /// </summary>
        public string VerificationResultCode;
        /// <summary>
        /// The source system
        /// </summary>
        public string SourceSystem;
        /// <summary>
        /// The source system number
        /// </summary>
        public string SourceSystemNo;
    }

    /// <summary>
    /// CreateLeadsRequest
    /// </summary>
    public class RequestCreateLeads
    {
        /// <summary>
        /// collCreateLeads
        /// </summary>
        public List<RequestCreateOpportunity> collCreateLeads;
    }

}