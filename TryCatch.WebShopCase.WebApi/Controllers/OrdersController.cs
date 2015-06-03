using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TryCatch.WebShopCase.Domain;
using TryCatch.WebShopCase.Services.Interfaces;

namespace TryCatch.WebShopCase.WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/orders
        public string Get(int page = 1, int itemsPerPage = 10, bool allOrders = false)
        {
            try
            {
                var orders = _orderService.FindAll();
                var serializer = new Newtonsoft.Json.JsonSerializer();
                var json = JObject.FromObject(
                    new
                    {
                        count = orders.Count(),
                        data = allOrders ?
                                            JsonConvert.SerializeObject(orders) :
                                            JsonConvert.SerializeObject(orders.Skip((page - 1) * itemsPerPage).Take(itemsPerPage)) //TODO :: Move pagination to repository 
                    });

                return json.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET api/orders/5
        public string Get(Guid id)
        {
            try
            {
                var order = _orderService.Get(id);
                return JsonConvert.SerializeObject(order);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // POST api/orders
        public string Post([FromBody]string value)
        {
            try
            {
                var order = _orderService.BuildOrderFromJson(value);
                var result = _orderService.Insert(order);
                
                var json = JObject.FromObject(
                new
                {
                    OrderId = result.Id
                });

                return json.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // PUT api/orders/5
        public string Put([FromBody]string value)
        {
            try
            {
                var order = _orderService.BuildOrderFromJson(value);
                _orderService.Update(order);
                return JsonConvert.SerializeObject(order);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // DELETE api/orders/5
        public string Delete(Guid id)
        {
            try
            {
                _orderService.Delete(id);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
