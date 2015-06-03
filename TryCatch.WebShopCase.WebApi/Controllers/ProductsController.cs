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
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/products
        public HttpResponseMessage Get(int page = 1, int itemsPerPage = 10, bool allProducts = false)
        {
            try
            {
                var products = _productService.FindAll();
                var serializer = new Newtonsoft.Json.JsonSerializer();
                var json = 
                    new
                    {
                        count = products.Count(),
                        data = allProducts ?
                                            JsonConvert.SerializeObject(products) :
                                            JsonConvert.SerializeObject(products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage)) //TODO :: Move pagination to repository 
                    };

                return Request.CreateResponse(HttpStatusCode.OK, json);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // GET api/products/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var product = _productService.Get(id);
                if (product == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, string.Format("Doesn't exist a product with id {0}", id));
                else
                    return Request.CreateResponse<Product>(HttpStatusCode.OK, product);                
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/products
        public HttpResponseMessage Post([FromBody]string value)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(value);
                var result = _productService.Insert(product);
                return Request.CreateResponse<Product>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.Created, ex.Message);
            }
        }

        // PUT api/products/5
        public HttpResponseMessage Put([FromBody]string value)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(value);
                _productService.Update(product);
                return Request.CreateResponse<Product>(HttpStatusCode.OK, product);
            }
            catch (Exception ex)
            {
                //TODO :: Log  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/products/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _productService.Delete(id);
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
