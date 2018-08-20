using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using AI.ADP.UpdateOpportunity.Models;

namespace Agit.Web.Helper
{
    public class WebApiClient : HttpClient
    {
        public string Uri { get; set; }

        public WebApiClient()
        {
           // this.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUrl"]);
            var test = BaseAddress;
            this.DefaultRequestHeaders.Accept.Clear();
            this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public WebApiClient(string pUrl) : this()
        {
            this.Uri = this.BaseAddress + pUrl;

        }

    
    }
}