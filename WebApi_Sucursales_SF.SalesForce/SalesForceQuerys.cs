using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Sucursales_SF.SalesForce
{
    public class SalesForceQuerys<T>
    {
        private SalesforceConnection _sfConn;

        public SalesForceQuerys()
        {
            _sfConn = new SalesforceConnection();
            _sfConn.loggin();
        }

        public T GetSucursal(string nip)
        {
            string query =
                @"SELECT Sales_Force__c,
                           Region__c,
                           Division__c,
                           DC__c,
                           RTE_ID__c,
                           PEP_ExternalID_Location__c,
                           PEP_ExternalID_District__c
                    FROM AccountToRoute__c
                    WHERE Active__c = TRUE
                      AND CUST_ID__r.CUST_ID__c='"+nip+@"'
                      AND (Sales_Force__c='S2'
                           OR Sales_Force__c = 'SABRITAS S. DE R.L. DE C.V.'
                           OR Sales_Force__c='GAMESA S DE RL DE CV')";

            var result = JsonConvert.DeserializeObject<QueryResult<T>>(_sfConn.Query(query));

           

            return result.records;
        }
    }
}
