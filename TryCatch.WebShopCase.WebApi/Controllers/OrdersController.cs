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
        public HttpResponseMessage Get(int page = 1, int itemsPerPage = 10, bool allOrders = false)
        {
            try
            {
                var orders = _orderService.FindAll();
                var serializer = new Newtonsoft.Json.JsonSerializer();
                var json =
                    new
                    {
                        count = orders.Count(),
                        data = allOrders ?
                                            JsonConvert.SerializeObject(orders) :
                                            JsonConvert.SerializeObject(orders.Skip((page - 1) * itemsPerPage).Take(itemsPerPage)) //TODO :: Move pagination to repository 
                    };

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/orders/5
        public HttpResponseMessage Get(Guid id)
        {
            try
            {
                var order = _orderService.Get(id);

                if (order == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Doesn't exist an order with id {0}", id));
                else
                    return Request.CreateResponse<Order>(HttpStatusCode.OK, order);

            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/orders
        public HttpResponseMessage Post([FromBody]string value)
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

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/orders/5
        public HttpResponseMessage Put([FromBody]string value)
        {
            try
            {
                var order = _orderService.BuildOrderFromJson(value);
                _orderService.Update(order);
                return Request.CreateResponse<Order>(HttpStatusCode.OK, order);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/orders/5
        public HttpResponseMessage Delete(Guid id)
        {
            try
            {
                _orderService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
