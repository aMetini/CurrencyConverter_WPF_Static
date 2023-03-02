using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace CurrencyConverter_WPF
{
    public class CurrencyConverter
    {
        Dictionary<string, string> currencySymbols;

        public Dictionary<string, string> GetCurrencySymbols()
        {
            if (currencySymbols == null)
            {
                currencySymbols = new Dictionary<string, string>();
                string responseContent = getResponseString($"exchangerates_data/symbols");

                Dictionary<string, object> responseData = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                if ((bool)responseData["success"])
                {
                    var tempSymbols = (JObject)responseData["symbols"];
                    currencySymbols = tempSymbols.ToObject<Dictionary<string, string>>();
                }
            }  
            return currencySymbols;
        }

        private string getResponseString(string correspondingURI)
        {
            // Add reference to API client using base URL
            var client = new RestClient($"https://api.apilayer.com/");
            //client.Timeout = -1;

            // Add request from API using Method.Get
            var request = new RestRequest(correspondingURI, Method.Get);
            request.AddHeader("apikey", "pnwCUeO7LgzUUgU3eX9MbPK12jM0gDpW");

            // Makes call to the API
            RestResponse response = client.Execute(request);
            return response.Content;
        }

        public CurrencyConverter()
        {
        }
    }
}