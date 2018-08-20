using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AI.ADP.DomainObject.Models
{
  
        /// <summary>
        /// ResponseCheckCustomer
        /// </summary>
        public class ResponseCheckCustomer
        {
        /// <summary>
        /// TheAstraID
        /// </summary>
        public string AstraID;      
        }

        /// <summary>
        /// ResponseCheckCustomers
        /// </summary>
        public class ResponseCheckCustomers
    {
            /// <summary>
            /// The coll request check customers
            /// </summary>
            public List<ResponseCheckCustomer> collResponseCheckCustomers;
        }

   
    
}