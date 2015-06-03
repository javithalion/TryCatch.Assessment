﻿using Newtonsoft.Json;
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
        public string Get(int page = 1, int itemsPerPage = 10, bool allProducts = false)
        {
            try
            {
                var products = _productService.FindAll(); 
                var serializer = new Newtonsoft.Json.JsonSerializer();
                var json = JObject.FromObject(
                    new
                    {
                        count = products.Count(),
                        data = allProducts ? 
                                            JsonConvert.SerializeObject(products) :
                                            JsonConvert.SerializeObject(products.Skip((page - 1) * itemsPerPage).Take(itemsPerPage)) //TODO :: Move pagination to repository 
                    });

                return json.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // GET api/products/5
        public string Get(int id)
        {
            try
            {
                var product = _productService.Get(id);
                return JsonConvert.SerializeObject(product);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // POST api/products
        public string Post([FromBody]string value)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(value);
                var result = _productService.Insert(product);
                return JsonConvert.SerializeObject(product);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // PUT api/products/5
        public string Put([FromBody]string value)
        {
            try
            {
                var product = JsonConvert.DeserializeObject<Product>(value);
                _productService.Update(product);
                return JsonConvert.SerializeObject(product);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // DELETE api/products/5
        public string Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
