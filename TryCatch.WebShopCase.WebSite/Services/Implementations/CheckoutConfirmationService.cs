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
        private readonly string _apiBaseUrl = ConfigurationManager.AppSettings["System.Configuration.ApiBaseUrl"];

        public CheckoutConfirmationModel GetEmptyCheckOutConfirmationModel()
        {
            return new CheckoutConfirmationModel();
        }

        public void CheckOutAction(CheckoutConfirmationModel model)
        {
            System.Threading.Thread.Sleep(300);
            return;
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string uri = string.Format("{0}/{1}", _apiBaseUrl, "Orders");
                    var content = new StringContent("{test:1}");
                    var response = httpClient.PostAsync(uri,content);                       
                }

            }catch(Exception ex)
            {
                throw new Exception("There was a problem confirming the check out with system. Please try again.", ex);
            }
        }
    }
}