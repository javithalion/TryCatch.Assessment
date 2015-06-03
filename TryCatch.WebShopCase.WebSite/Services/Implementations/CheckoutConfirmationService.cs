using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TryCatch.WebShopCase.WebSite.Models;
using TryCatch.WebShopCase.WebSite.Services.Interfaces;

namespace TryCatch.WebShopCase.WebSite.Services.Implementations
{
    public class CheckoutConfirmationService : ICheckoutConfirmationService
    {
        private const string OrdersResourceName = "Orders";
        private readonly string _apiBaseUrl = ConfigurationManager.AppSettings["System.Configuration.ApiBaseUrl"];
        private readonly string _apiName = ConfigurationManager.AppSettings["System.Configuration.ApiName"];
        private readonly int _apiVersion = Convert.ToInt32(ConfigurationManager.AppSettings["System.Configuration.ApiVersion"]);

        public CheckoutConfirmationModel GetEmptyCheckOutConfirmationModel()
        {
            return new CheckoutConfirmationModel();
        }

        public Guid CheckOutAction(CheckoutConfirmationModel model)
        {
            try
            {
                var client = new RestClient(_apiBaseUrl);

                var request = new RestRequest(
                    string.Format("{0}/{1}", _apiName, OrdersResourceName),
                    Method.POST
                    );

                request.RequestFormat = DataFormat.Json;
                request.AddBody(JsonConvert.SerializeObject(model));

                var response = client.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                    response.StatusCode != System.Net.HttpStatusCode.Created &&
                    response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    throw new Exception(string.Format("Request to API system failed. Status code = {0} ErrorMessage = {1}", response.StatusCode, response.ErrorMessage));
                }
                else
                {
                    JObject parsedResponse = JObject.Parse(response.Content);
                    return new Guid((string)parsedResponse["OrderId"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem confirming the check out with system. Please try again.", ex);
            }
        }
    }
}