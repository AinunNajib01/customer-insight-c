using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AI.ADP.DomainObject.Models;
using Ninject;
using AI.ADP.DataAccess;
using AI.ADP.DomainObject;
using AI.ADP.Business;

namespace AI.ADP.DummyWebAPI.Controllers
{
    [RoutePrefix("api/v1/ADP/BigDataCustomer")]
    public class BigDataCustomerController : ApiController
    {
        public BigDataCustomerController()
        {

        }

        /// <summary>
        /// Requests the check customer.
        /// </summary>
        /// <returns></returns>
        [Route("CheckCustomer")]
        [ResponseType(typeof(List<RequestCheckCustomers>))]
        [HttpPost]
        public IHttpActionResult getRequestCheckCustomer([FromBody]RequestCheckCustomer customer)
        {
            BigDatas BigData = new BigDatas();
            ResponseCheckCustomer response = new ResponseCheckCustomer();
            MessageModel message = new MessageModel();
            if (customer == null)
            {
                message.message = "Bad Request";
                message.success = false;
                message.errors = "Request Object is NULL";
                return Content(HttpStatusCode.BadRequest, message);
            }
            if (customer.BUID == null && customer.SourceSystem == null)
            {
                message.message = "Bad Request";
                message.success = false;
                message.errors = "Required field is NULL";
                return Content(HttpStatusCode.BadRequest, message);
            }
            if (customer.BUID == null && customer.BUID.Trim() =="")
            {
                message.message = "Required Field is NULL";
                message.success = false;
                message.errors = "BUID is Required";
                return Content(HttpStatusCode.BadRequest, message);
            }
            if (customer.SourceSystem == null || customer.SourceSystem.Trim() =="")
            {
                message.message = "Required Field is NULL";
                message.success = false;
                message.errors = "SourceSystem is Required";
                return Content(HttpStatusCode.BadRequest, message);
            }
            
            try
            {
                using (IKernel kernel = new StandardKernel())
                {
                    kernel.Bind<IBigDataDataAccess>().To<BigDataDataAccess>();
                    BigData = kernel.Get<BigDataBusiness>().checkCustomer(customer);
                    if (BigData == null)
                    {
                        response.AstraID = null;
                        return Ok(response);
                    }
                    else
                    {
                        response.AstraID = BigData.Astra_ID;
                    }
                    return Ok(response);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Requests the get pii data.
        /// </summary>
        /// <param name="astraid">The astraid.</param>
        /// <returns></returns>
        [Route("getPIIData")]
        public IHttpActionResult getRequestGetPIIData(string astraID)
        {
            BigDatas bigData = new BigDatas();
            ResponseGetPIIData response = new ResponseGetPIIData();
            MessageModel message = new MessageModel();

            if (astraID == null || astraID.Trim() =="" )
            {
                message.message = "Bad Request";
                message.success = false;
                return Content(HttpStatusCode.BadRequest, message);
            }
            try
            {
                using (IKernel kernel = new StandardKernel())
                {
                    kernel.Bind<IBigDataDataAccess>().To<BigDataDataAccess>();
                    bigData = kernel.Get<BigDataBusiness>().getPIIData(astraID);
                    if(bigData == null)
                    {
                        message.message = "Data not Exist";
                        message.success = false;
                        return Content(HttpStatusCode.BadRequest, message);
                    }
                    else
                    {
                    response.Name = bigData.Name == null ? String.Empty : bigData.Name;
                    response.Email = bigData.Email == null ? String.Empty : bigData.Email;
                    response.DOB = bigData.DOB == null ? String.Empty : bigData.DOB;
                    response.Birth_place = bigData.Birth_Place == null ? String.Empty : bigData.Birth_Place;
                    response.Gender = bigData.Gender == null ? String.Empty : bigData.Gender;
                    response.Religion = bigData.Religion == null ? String.Empty : bigData.Religion;
                    response.Education = bigData.Education == null ? String.Empty : bigData.Education;
                    response.Employee_type = bigData.Employee_Type == null ? String.Empty : bigData.Employee_Type;
                    response.Marital_status = bigData.Marital_Status == null ? String.Empty : bigData.Marital_Status;
                    response.Address = bigData.Address == null ? String.Empty : bigData.Address;
                    response.City = bigData.City == null ? String.Empty : bigData.City;
                    response.Kecamatan = bigData.Kecamatan == null ? String.Empty : bigData.Kecamatan;
                    response.Kelurahan = bigData.Kelurahan == null ? String.Empty : bigData.Kelurahan;
                    response.Zip_code = bigData.Zip_Code == null ? String.Empty : bigData.Zip_Code;
                    response.Bps_code = bigData.BPS_Code == null ? String.Empty : bigData.BPS_Code;
                    response.ID_Number = bigData.ID_Number == null ? String.Empty : bigData.ID_Number;
                    response.ID_Type = bigData.ID_Type == null ? String.Empty : bigData.ID_Type;
                    response.NPWP = bigData.NPWP == null ? String.Empty : bigData.NPWP;
                    response.Office_Phone = bigData.Office_Phone == null ? String.Empty : bigData.Office_Phone;
                    response.Mobile_Phone = bigData.Mobile_Phone == null ? String.Empty : bigData.Mobile_Phone;
                    response.Home_Phone = bigData.Home_Phone == null ? String.Empty : bigData.Home_Phone;
                    response.Fax_Number = bigData.Fax_Number == null ? String.Empty : bigData.Fax_Number;
                    response.BUID = bigData.BUID == null ? String.Empty : bigData.BUID;
                    }

                    return Ok(response);
                }
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
