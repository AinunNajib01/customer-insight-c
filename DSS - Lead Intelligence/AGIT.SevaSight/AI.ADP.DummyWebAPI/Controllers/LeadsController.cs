using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Ninject;
using AI.ADP.DataAccess;
using AI.ADP.DomainObject;
using AI.ADP.Business;
using System;
using AI.ADP.DomainObject.Models;
using System.Net;
using System.IO;

namespace AI.ADP.DummyWebAPI.Controllers
{
    [RoutePrefix("api/v1/ADP/Leads")]
    public class LeadsController : ApiController
    {
        private static List<RequestCreateOpportunity> LeadsList;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeadsController"/> class.
        /// </summary>
        public LeadsController()
        {
           
        }
        
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [Route("getLead")]
        [ResponseType(typeof(List<RequestCreateOpportunity>))]
        public IHttpActionResult getLead(string orderNo)
        {
            

            Lead lead = new Lead();
            ResponseGetLead response = new ResponseGetLead();
            MessageModel message = new MessageModel();

            if (orderNo == null || orderNo.Trim()=="")
            {
                message.message = "Bad Request";
                message.success = false;
                message.errors = "OrderNo is NULL";
                return Content(HttpStatusCode.BadRequest, message);
            }
            try
            {
                using (IKernel kernel = new StandardKernel())
                {
                    kernel.Bind<ILeadDataAccess>().To<LeadDataAccess>();

                    lead = kernel.Get<LeadBusiness>().FindByOrderNo(orderNo);
                    if (lead == null) {
                        message.message = "Data is not Exist";
                        message.success = false;
                        return Content(HttpStatusCode.BadRequest, message); ;
                    }

                    
                    response.OrderNo = lead.OrderNo == null ? String.Empty : lead.OrderNo;
                    response.TestDriveTime = lead.TestDriveTime == null ? DateTime.Parse("1900-01-01") : lead.TestDriveTime;
                    response.TestDriveStatus = lead.TestDriveStatus == null ? String.Empty : lead.TestDriveStatus;
                    response.SalesmanName = lead.SalesmanName == null ? String.Empty : lead.SalesmanName;
                    response.ModelName = lead.ModelName == null ? String.Empty : lead.ModelName;
                    response.SalesSource = lead.SalesSource == null ? String.Empty : lead.SalesSource;
                    response.DropReason = lead.DropReason == null ? String.Empty : lead.DropReason;
                    response.SPKCreatedTime = lead.SPKCreatedTime == null ? DateTime.Now : lead.SPKCreatedTime;
                    response.DPTime = lead.DPTime == null ? DateTime.Parse("1900-01-01") : lead.DPTime;
                    response.DPAmount = lead.DPAmount == null ? decimal.Zero : lead.DPAmount;
                    response.FullPaymentTime = lead.FullPaymentTime == null ? DateTime.Parse("1900-01-01") : lead.FullPaymentTime;
                    response.FullPaymentAmount = lead.FullPaymentAmount == null ? decimal.Zero : lead.FullPaymentAmount;
                    response.PlanDeliveryDate = lead.PlanDeliveryDate == null ? DateTime.Parse("1900-01-01") : lead.PlanDeliveryDate;
                    response.ActualDeliveryDate = lead.ActualDeliveryDate == null ? DateTime.Parse("1900-01-01") : lead.ActualDeliveryDate;
                    response.STNKFinishedDate = lead.STNKFinishedDate == null ? DateTime.Parse("1900-01-01") : lead.STNKFinishedDate;
                    response.BPKBFinishedDate = lead.BPKBFinishedDate == null ? DateTime.Parse("1900-01-01") : lead.BPKBFinishedDate;
                    response.SPKCancellationTime = lead.SPKCancellationTime == null ? DateTime.Parse("1900-01-01") : lead.SPKCreatedTime;
                    response.SalesOrderCancellationTime = lead.SalesOrderCancellationTime == null ? DateTime.Parse("1900-01-01") : lead.SalesOrderCancellationTime;
                    response.BillingCancellationTime = lead.BillingCancellationTime == null ? DateTime.Parse("1900-01-01") : lead.BillingCancellationTime;
                    response.BillingReturnTime = lead.BillingReturnTime == null ? DateTime.Parse("1900-01-01") : lead.BillingReturnTime;

                        
                    
                    return Ok(response);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
                //string myTempFile = Path.Combine(Path.GetTempPath(), "ADPLog.txt");
                //using (StreamWriter sw = new StreamWriter(myTempFile))
                //{
                //    sw.WriteLine(DateTime.Now);
                //    sw.WriteLine(ex.StackTrace);
                //}
            }
            
        }


        /// <summary>
        /// Creates the lead.
        /// </summary>
        /// <param name="lead">The lead.</param>
        /// <returns></returns>
        [Route("CreateOpportunity")]
        [ResponseType(typeof(ResponseCreateOpportunity))]
        public IHttpActionResult CreateOpportunity([FromBody] RequestCreateOpportunity lead)
        {
            
            MessageModel message = new MessageModel();
            if (lead == null)
            {
                message.message = "Bad Request";
                message.success = false;
                message.errors = "Request Object is NULL";

                return Content(HttpStatusCode.BadRequest, message);
            }
            if (lead.Name1 == null && lead.Phone1 == null && lead.OpportunityID == null) {
                message.message = "Required Field is NULL";
                message.success = false;
                message.errors = "OpportunityID , Name1 and Phone1 is Required";
                return Content(HttpStatusCode.BadRequest, message);
            }
            if (lead.OpportunityID == null || lead.OpportunityID.Trim() == "")
            {
                message.message = "Required Field is NULL";
                message.success = false;
                message.errors = "OpportunityID is Required";
                return Content(HttpStatusCode.BadRequest, message);

            }
            if(lead.Name1 == null || lead.Name1.Trim() =="")
            {
                message.message = "Required Field is NULL";
                message.errors = " Name1 is Required";
                message.success = false;
                return Content(HttpStatusCode.BadRequest, message);
            }
            if (lead.Phone1 == null || lead.Phone1.Trim() =="")
            {
                message.message = "Required Field is NULL";
                message.errors = " Phone1 is Required";
                message.success = false;
                return Content(HttpStatusCode.BadRequest, message);
            }
            //save to DB
            try
            {
                using (IKernel kernel = new StandardKernel())
                {
                    kernel.Bind<ILeadDataAccess>().To<LeadDataAccess>();

                    //string myTempFile = Path.Combine(Path.GetTempPath(), "ADPLog.txt");
                    //using (StreamWriter sw = new StreamWriter(myTempFile))
                    //{
                    //    sw.WriteLine(DateTime.Now);
                    //    sw.WriteLine("OpportunityID: " + lead.OpportunityID.ToString());
                    //}


                    var leadDomain = new Lead();
                    leadDomain.Address1 = lead.Address1;
                    leadDomain.Address2 = lead.Address2;
                    leadDomain.Area = lead.Area;
                    leadDomain.BusinessArea = lead.BusinessAreaCode;
                    leadDomain.City = lead.City;
                    leadDomain.Company = lead.CompanyCode;
                    leadDomain.DropReasonCode = lead.DropReasonCode;
                    leadDomain.DropReasonDescription = lead.DropReasonDescription;
                    leadDomain.Email1 = lead.Email1;
                    leadDomain.Email2 = lead.Email2;
                    leadDomain.Kecamatan = lead.Kecamatan;
                    leadDomain.Kelurahan = lead.Kelurahan;
                    leadDomain.LeasingCompany = lead.LeasingCompany;
                    if (lead.LeasingInvoiceDate != null)
                        leadDomain.LeasingInvoiceDate = DateTime.Parse(lead.LeasingInvoiceDate);
                    else
                        leadDomain.LeasingInvoiceDate = DateTime.Parse("1900-01-01");
                    if (lead.LeasingDueDate != null)
                        leadDomain.LeasingDueDate = DateTime.Parse(lead.LeasingDueDate);
                    else
                        leadDomain.LeasingDueDate = DateTime.Parse("1900-01-01");
                    leadDomain.Name1 = lead.Name1;
                    leadDomain.Name2 = lead.Name2;
                    leadDomain.Notes1 = lead.Notes1;
                    leadDomain.Phone1 = lead.Phone1;
                    leadDomain.Phone2 = lead.Phone2;
                    leadDomain.Postal = lead.Postal;
                    leadDomain.PreferredBusinessArea = lead.PreferredBusinessAreaCode;
                    if (lead.PreferredDateToContacted != null)
                        leadDomain.PreferredDateToContacted = DateTime.Parse(lead.PreferredDateToContacted);
                    else
                        leadDomain.PreferredDateToContacted = DateTime.Parse("1900-01-01");

                    leadDomain.Program = lead.Program;
                    leadDomain.ProspectVariant = lead.ProspectVariant;
                    leadDomain.Province = lead.Province;
                    leadDomain.Title = lead.Title;
                    leadDomain.Score = lead.Score;
                    leadDomain.VerificationResultCode = lead.VerificationResultCode;
                    leadDomain.VerifiedCustomerName = lead.VerifiedCustomerName;
                    leadDomain.VerifiedCustomerNo = lead.VerifiedCustomerNo;
                    leadDomain.SourceSystem = lead.SourceSystem;
                    leadDomain.SourceSystemNo = lead.SourceSystemNo;
                    leadDomain.OpportunityID = lead.OpportunityID;
                    //leadDomain.CreatedOn = DateTime.Now;
                    leadDomain.CreatedBy = "System";
                    
                    if (kernel.Get<LeadBusiness>().Create(leadDomain) < 0)
                    {
                        return InternalServerError();
                    }
                    else
                    {
                        ResponseCreateOpportunity response = new ResponseCreateOpportunity();
                        response.success = true;
                        response.message = "Create lead success";
                        return Ok(response);
                    }
                }
                
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
                //string myTempFile = Path.Combine(Path.GetTempPath(), "ADPLog.txt");
                //using (StreamWriter sw = new StreamWriter(myTempFile))
                //{
                //    sw.WriteLine(DateTime.Now);
                //    sw.WriteLine(ex.StackTrace);
                //}
            }
        }
    }
}
