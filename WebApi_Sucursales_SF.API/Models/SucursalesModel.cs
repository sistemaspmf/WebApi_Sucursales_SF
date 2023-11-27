using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Sucursales_SF.API.Models
{
    public class SucursalesModel
    {
        public string Sales_Force__c { get; set; }
        public string Region__c { get; set; }
        public string Division__c { get; set; }
        public string DC__c { get; set; }
        public string RTE_ID__c { get; set; }
        public string PEP_ExternalID_Location__c { get; set; }
        public string PEP_ExternalID_District__c { get; set; }
    }
}