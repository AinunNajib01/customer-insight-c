using Agit.Web.Helper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using AI.ADP.UpdateOpportunity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AI.ADP.DomainObject;
using AI.ADP.DataAccess;
using AI.ADP.Business;
using System.Configuration;

using Ninject;
using System.Configuration;

namespace AGIT.HR.Web.Controllers
{
    public class GetOrderStatusController : Controller
    {
      

       public ActionResult Index()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpanGetOrderStatus = TimeSpan.FromMinutes(Double.Parse(ConfigurationManager.AppSettings["SchedulerTimeGetOrderStatus"]));
            var periodTimeSpanUpdateOpportunity = TimeSpan.FromMinutes(Double.Parse(ConfigurationManager.AppSettings["SchedulerTimeUpdateOpportunity"]));

            var timerGetOrderStatus = new System.Threading.Timer((e) =>
            {
                getOrderStatusScheduler();
            }, null, startTimeSpan, periodTimeSpanGetOrderStatus);

            var timerUpdateOpportunity = new System.Threading.Timer((e) =>
            {
                updateOpportunity();
            }, null, startTimeSpan, periodTimeSpanGetOrderStatus);

            return View();
        }
  

        //update Opportunity to CRM
        public ActionResult updateOpportunity()
        {
            var response = new HttpResponseMessage();
            List<Lead> listLead = new List<Lead>();
            RequestUpdateOpportunity Opportunity = new RequestUpdateOpportunity();
            Uri APIM_Uri = new Uri(ConfigurationManager.AppSettings["APIMUri"]);//new Uri("https://astradtaisapimint.azure-api.net/api/v1/ADP/");

            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ILeadDataAccess>().To<LeadDataAccess>();
                listLead = kernel.Get<LeadBusiness>().FindBySyncStatus();
                if (listLead == null)
                {

                }
                else
                {
                    foreach(Lead lead in listLead)
                    {
                        Opportunity.OpportunityId = lead.OpportunityID;
                        // logic need more validation information
                        if (lead.DropReason != null && lead.DropReason != "") { Opportunity.Status = 3; Opportunity.StatusReason = "LOSE"; }
                        if (lead.SPKCreatedTime.ToString() != "1/1/1900 12:00:00 AM" && lead.SPKCreatedTime.ToString() != "1/1/1753 12:00:00 AM") { Opportunity.Status = 2; Opportunity.StatusReason = "WIN"; }
                        Opportunity.SalesPersonName = lead.SalesmanName;
                        Opportunity.Email = "";
                        Opportunity.PhoneNumber = "";

                        using (WebApiClient client = new WebApiClient())
                        {
                            client.BaseAddress = APIM_Uri;
                            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ab43d7214ae04756b3ff81aeaa75f577");
                            response = client.PostAsJsonAsync("UpdateOpportunity", Opportunity).Result;
                            if (response.IsSuccessStatusCode)
                            {
                                lead.SyncStatus = 1;
                                kernel.Get<LeadBusiness>().Update(lead);
                            }
                            else
                            {
                                lead.SyncStatus = 0;
                                kernel.Get<LeadBusiness>().Update(lead);
                            }

                        }
                    }
                }
                return View();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult getOrderStatusScheduler()
        {
            var response = new HttpResponseMessage();
            var getToken = new Token();
            var result = new ResponseGetOrderStatuses();
            var leadOrderStatus = new ResponseGetOrderStatus();
            var lead = new Lead();
            Uri proxy_Uri = new Uri(ConfigurationManager.AppSettings["ProxyUri"]);//new Uri("https://devproxy.astra.co.id/psswebapi/api/");
            Uri APIM_Uri = new Uri(ConfigurationManager.AppSettings["APIMUri"]);//new Uri("https://astradtaisapimint.azure-api.net/api/v1/ADP/");
            RetrieveOrderStatus request = new RetrieveOrderStatus();

            request.Password = ConfigurationManager.AppSettings["RequestPassword"];//"Astra@123";
            request.CompanyCode = ConfigurationManager.AppSettings["RequestCompanyCode"];//"0002";
            request.Action = "";
            request.HeadOfficeCode = ConfigurationManager.AppSettings["RequestHeadOfficeCode"];// "0001";
            request.UserID = ConfigurationManager.AppSettings["RequestUserID"];// "loyaltyuser";
            request.ApplicationCode = int.Parse(ConfigurationManager.AppSettings["RequestApplicationCode"]);//0;
            request.dateFrom = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
            request.dateTo = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");

            try
            {
                using (WebApiClient clientAuth = new WebApiClient())
                {
                    clientAuth.BaseAddress = proxy_Uri;
                    response = clientAuth.PostAsJsonAsync("Authentication/PostAuthentication", request).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        getToken = response.Content.ReadAsAsync<Token>().Result;
                        request.AccessToken = getToken.AccessToken;
                    }
                }
            }
            catch(Exception ex)
            {
                return View(ex);
            }
            try
            {
                if (request.AccessToken != null)
                {
                    using (WebApiClient clientOrderStatus = new WebApiClient())
                    {
                        clientOrderStatus.BaseAddress = proxy_Uri;
                        response = clientOrderStatus.PostAsJsonAsync("LMSOrderStatus/Retrieve", request).Result;
                        if (response.IsSuccessStatusCode)
                        {

                            ResponseGetOrderStatuses getResponse = response.Content.ReadAsAsync<ResponseGetOrderStatuses>().Result;
                            if (getResponse != null)
                            {
                                foreach(ResponseGetOrderStatus getOrderStatusDomain in getResponse.ResponseOrderStatuses)
                                {
                                    using (IKernel kernel = new StandardKernel())
                                    {
                                        kernel.Bind<ILeadDataAccess>().To<LeadDataAccess>();

                                        lead.SourceSystemNo = getOrderStatusDomain.OrderNo;
                                        lead.TestDriveTime = getOrderStatusDomain.TestDriveTime != null ? DateTime.Parse(getOrderStatusDomain.TestDriveTime) : DateTime.MinValue;
                                        lead.TestDriveStatus = getOrderStatusDomain.TestDriveStatus;
                                        lead.SalesmanName = getOrderStatusDomain.SalesmanName;
                                        lead.ModelName = getOrderStatusDomain.ModelName;
                                        lead.SalesSource = getOrderStatusDomain.SalesSource;
                                        lead.DropReason = getOrderStatusDomain.DropReason;
                                        lead.SPKCreatedTime = getOrderStatusDomain.SPKCreatedTime != null ? DateTime.Parse(getOrderStatusDomain.SPKCreatedTime) : DateTime.MinValue;
                                        lead.DPTime = getOrderStatusDomain.DPTime != null ? DateTime.Parse(getOrderStatusDomain.DPTime) : DateTime.MinValue;
                                        lead.DPAmount = decimal.Parse(getOrderStatusDomain.DPAmount);
                                        lead.FullPaymentAmount = decimal.Parse(getOrderStatusDomain.FullPaymentAmount);
                                        lead.FullPaymentTime = getOrderStatusDomain.FullPaymentTime != null ? DateTime.Parse(getOrderStatusDomain.FullPaymentTime) : DateTime.MinValue;
                                        lead.PlanDeliveryDate = getOrderStatusDomain.PlanDeliveryDate != null ? DateTime.Parse(getOrderStatusDomain.PlanDeliveryDate) : DateTime.MinValue;
                                        lead.ActualDeliveryDate = getOrderStatusDomain.ActualDeliveryDate != null ? DateTime.Parse(getOrderStatusDomain.ActualDeliveryDate) : DateTime.MinValue;
                                        lead.STNKFinishedDate = getOrderStatusDomain.STNKFinishedDate != null ? DateTime.Parse(getOrderStatusDomain.STNKFinishedDate) : DateTime.MinValue;
                                        lead.BPKBFinishedDate = getOrderStatusDomain.BPKBFinishedDate != null ? DateTime.Parse(getOrderStatusDomain.BPKBFinishedDate) : DateTime.MinValue;
                                        lead.SPKCancellationTime = DateTime.Parse(getOrderStatusDomain.SPKCancellationTime);
                                        lead.SalesOrderCancellationTime = getOrderStatusDomain.SalesOrderCancellationTime != null ? DateTime.Parse(getOrderStatusDomain.SalesOrderCancellationTime) : DateTime.MinValue;
                                        lead.BillingCancellationTime = getOrderStatusDomain.BillingCancellationTime != null ? DateTime.Parse(getOrderStatusDomain.BillingCancellationTime) : DateTime.MinValue;
                                        lead.BillingReturnTime = getOrderStatusDomain.BillingReturnTime != null ? DateTime.Parse(getOrderStatusDomain.BillingReturnTime) : DateTime.MinValue;
                                        lead.SyncStatus = 0;

                                        kernel.Get<LeadBusiness>().Update(lead);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
                    
            return View(request);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult getOrderStatus(RetrieveOrderStatus request)
        {
            var response = new HttpResponseMessage();
            var getToken = new Token();
            var result = new ResponseGetOrderStatuses();
            var leadOrderStatus = new ResponseGetOrderStatus();
            var lead = new Lead();
            Uri proxy_Uri = new Uri(ConfigurationManager.AppSettings["ProxyUri"]);//new Uri("https://   .astra.co.id/psswebapi/api/");
            Uri APIM_Uri = new Uri(ConfigurationManager.AppSettings["APIMUri"]);//new Uri("https://astradtaisapimint.azure-api.net/api/v1/ADP/");
           

            request.Password = ConfigurationManager.AppSettings["RequestPassword"];//"Astra@123";
            request.CompanyCode = ConfigurationManager.AppSettings["RequestCompanyCode"];//"0002";
            request.Action = "";
            request.HeadOfficeCode = ConfigurationManager.AppSettings["RequestHeadOfficeCode"];// "0001";
            request.UserID = ConfigurationManager.AppSettings["RequestUserID"];// "loyaltyuser";
            request.ApplicationCode = int.Parse(ConfigurationManager.AppSettings["RequestApplicationCode"]);//0;
            

            try
            {
                using (WebApiClient clientAuth = new WebApiClient())
                {
                    clientAuth.BaseAddress = proxy_Uri;
                    response = clientAuth.PostAsJsonAsync("Authentication/PostAuthentication", request).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        getToken = response.Content.ReadAsAsync<Token>().Result;
                        request.AccessToken = getToken.AccessToken;
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            try
            {
                if (request.AccessToken != null)
                {
                    using (WebApiClient clientOrderStatus = new WebApiClient())
                    {
                        clientOrderStatus.BaseAddress = proxy_Uri;
                        response = clientOrderStatus.PostAsJsonAsync("LMSOrderStatus/Retrieve", request).Result;
                        if (response.IsSuccessStatusCode)
                        {

                            ResponseGetOrderStatuses getResponse = response.Content.ReadAsAsync<ResponseGetOrderStatuses>().Result;
                            if (getResponse != null)
                            {
                                foreach (ResponseGetOrderStatus getOrderStatusDomain in getResponse.ResponseOrderStatuses)
                                {
                                    using (IKernel kernel = new StandardKernel())
                                    {
                                        kernel.Bind<ILeadDataAccess>().To<LeadDataAccess>();

                                        lead.SourceSystemNo = getOrderStatusDomain.OrderNo;
                                        lead.TestDriveTime = getOrderStatusDomain.TestDriveTime != null ? DateTime.Parse(getOrderStatusDomain.TestDriveTime) : DateTime.MinValue;
                                        lead.TestDriveStatus = getOrderStatusDomain.TestDriveStatus;
                                        lead.SalesmanName = getOrderStatusDomain.SalesmanName;
                                        lead.ModelName = getOrderStatusDomain.ModelName;
                                        lead.SalesSource = getOrderStatusDomain.SalesSource;
                                        lead.DropReason = getOrderStatusDomain.DropReason;
                                        lead.SPKCreatedTime = getOrderStatusDomain.SPKCreatedTime != null ? DateTime.Parse(getOrderStatusDomain.SPKCreatedTime) : DateTime.MinValue;
                                        lead.DPTime = getOrderStatusDomain.DPTime != null ? DateTime.Parse(getOrderStatusDomain.DPTime) : DateTime.MinValue;
                                        lead.DPAmount = decimal.Parse(getOrderStatusDomain.DPAmount);
                                        lead.FullPaymentAmount = decimal.Parse(getOrderStatusDomain.FullPaymentAmount);
                                        lead.FullPaymentTime = getOrderStatusDomain.FullPaymentTime != null ? DateTime.Parse(getOrderStatusDomain.FullPaymentTime) : DateTime.MinValue;
                                        lead.PlanDeliveryDate = getOrderStatusDomain.PlanDeliveryDate != null ? DateTime.Parse(getOrderStatusDomain.PlanDeliveryDate) : DateTime.MinValue;
                                        lead.ActualDeliveryDate = getOrderStatusDomain.ActualDeliveryDate != null ? DateTime.Parse(getOrderStatusDomain.ActualDeliveryDate) : DateTime.MinValue;
                                        lead.STNKFinishedDate = getOrderStatusDomain.STNKFinishedDate != null ? DateTime.Parse(getOrderStatusDomain.STNKFinishedDate) : DateTime.MinValue;
                                        lead.BPKBFinishedDate = getOrderStatusDomain.BPKBFinishedDate != null ? DateTime.Parse(getOrderStatusDomain.BPKBFinishedDate) : DateTime.MinValue;
                                        lead.SPKCancellationTime = DateTime.Parse(getOrderStatusDomain.SPKCancellationTime);
                                        lead.SalesOrderCancellationTime = getOrderStatusDomain.SalesOrderCancellationTime != null ? DateTime.Parse(getOrderStatusDomain.SalesOrderCancellationTime) : DateTime.MinValue;
                                        lead.BillingCancellationTime = getOrderStatusDomain.BillingCancellationTime != null ? DateTime.Parse(getOrderStatusDomain.BillingCancellationTime) : DateTime.MinValue;
                                        lead.BillingReturnTime = getOrderStatusDomain.BillingReturnTime != null ? DateTime.Parse(getOrderStatusDomain.BillingReturnTime) : DateTime.MinValue;
                                        lead.SyncStatus = 0;

                                        kernel.Get<LeadBusiness>().Update(lead);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }

            return View(request);
        }


    }

}
