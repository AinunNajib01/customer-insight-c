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
using Agit.Sevasight.Models;

namespace AI.ADP.DummyWebAPI.Controllers
{
    [RoutePrefix("api/v1/Sevasight")]
    public class SalesOrderController : ApiController
    {
        private static List<RequestCreateOpportunity> LeadsList;

        /// <summary>
        /// Initializes a new instance of the <see cref="LeadsController"/> class.
        /// </summary>
        public SalesOrderController()
        {
           
        }
        
      

        /// <summary>
        /// Creates the lead.
        /// </summary>
        /// <param name="lead">The lead.</param>
        /// <returns></returns>
        [Route("updateSalesOrder")]
        [ResponseType(typeof(SalesOrder))]
        public IHttpActionResult CreateOpportunity([FromBody] LeadRequest salesOrder)
        {
            
            MessageModel message = new MessageModel();
            if (salesOrder == null)
            {
                message.message = "Bad Request";
                message.success = false;
                message.errors = "Request Object is NULL";

                return Content(HttpStatusCode.BadRequest, message);
            }
            if (salesOrder.ID == 0) {
                message.message = "Required Field is NULL";
                message.success = false;
                message.errors = "ID is Required";
                return Content(HttpStatusCode.BadRequest, message);
            }
           
            //save to DB
            try
            {
                using (IKernel kernel = new StandardKernel())
                {
                    kernel.Bind<ISalesOrderDataAccess>().To<SalesOrderDataAccess>();

                 

                    var salesOrderDomain = new SalesOrder();
                    salesOrderDomain.ID = salesOrder.ID;
                    salesOrderDomain.NoKTP = salesOrder.KTP;
                    salesOrderDomain.Nama = salesOrder.Nama;
                    salesOrderDomain.NoTelp = salesOrder.NoTelp;
                    salesOrderDomain.Email = salesOrder.Email;
                    salesOrderDomain.Jenis6 = salesOrder.Jenis9_Now;
                    salesOrderDomain.TipeMotor = salesOrder.Series_Now;
                    salesOrderDomain.Jenis6Selanjutnya = salesOrder.Jenis9_Next;
                    salesOrderDomain.statuslead = salesOrder.statusLead;
                    if(salesOrder.TanggalBeliSelanjutnya !=null)
                    {
                        salesOrderDomain.TanggalBeliSelanjutnya =DateTime.Parse( salesOrder.TanggalBeliSelanjutnya);
                    }
                    salesOrderDomain.SalesPerson = salesOrder.SalesPerson;
                   


                    
                    if (kernel.Get<SalesOrderBusiness>().Update(salesOrderDomain) < 0)
                    {
                        return InternalServerError();
                    }
                    else
                    {
                        ResponseCreateOpportunity response = new ResponseCreateOpportunity();
                        response.success = true;
                        response.message = "Update Sales Order success";
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
