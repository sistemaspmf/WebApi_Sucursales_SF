using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApi_Sucursales_SF.SalesForce
{
    public class SalesforceConnection
    {
        private static AuthSalesforce _authSalesforce;

        internal void loggin()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://login.salesforce.com/services/oauth2/token");
                var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new KeyValuePair<string, string>("grant_type", "password"));
                collection.Add(new KeyValuePair<string, string>("client_id", this.GetConfig("client_id")));
                collection.Add(new KeyValuePair<string, string>("client_secret", this.GetConfig("client_secret")));
                collection.Add(new KeyValuePair<string, string>("username", this.GetConfig("username")));
                collection.Add(new KeyValuePair<string, string>("password", string.Concat(this.GetConfig("password"), this.GetConfig("token"))));
                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadAsStringAsync().Result;
                _authSalesforce = JsonConvert.DeserializeObject<AuthSalesforce>(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal string Query(string query, bool isUrl = false)
        {
            string apiUrl = string.Empty;
            if (isUrl)
                apiUrl = _authSalesforce.instance_url + query;
            else
                apiUrl = _authSalesforce.instance_url + "/services/data/v53.0/queryAll/?q=" + HttpUtility.UrlEncode(query);


            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(60);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authSalesforce.access_token}");
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        return responseBody;
                    }
                    else
                    {
                        throw new Exception($"Request failed with status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;
                }
            }

        }

        private string GetConfig(string key) => ConfigurationManager.AppSettings[key].ToString();
    }
}
