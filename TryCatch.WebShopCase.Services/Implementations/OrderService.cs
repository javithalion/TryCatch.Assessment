using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Interfaces;
using TryCatch.WebShopCase.Domain;
using TryCatch.WebShopCase.Infraestructure.Repository;
using TryCatch.WebShopCase.Services.Interfaces;

namespace TryCatch.WebShopCase.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ICrudRepository<Order, Guid> _orderRepository;
        private readonly ICrudRepository<OrderLine, Guid> _orderLineRepository;
        private readonly ICrudRepository<Customer, Guid> _customerRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(ICrudRepository<Order, Guid> orderRepository,
                            ICrudRepository<OrderLine, Guid> orderLineRepository,
                            ICrudRepository<Customer, Guid> customerRepository,
                            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public IList<Order> FindAll()
        {
            var orders = _orderRepository.FindAll();
            PopulateProductsForOrders(orders);
            return orders.ToList();
        }

        public IList<Order> FindAll(Expression<Func<Order, bool>> query)
        {
            var orders = _orderRepository.FindAll(query);
            PopulateProductsForOrders(orders);
            return orders.ToList();
        }

        public Order Get(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(string.Format("Provided id {0} was not valid. Id should be different than empty identifier.", id));

            var order = _orderRepository.Get(id);
            if (order != null)
                PopulateProductsForOrder(order);

            return order;
        }

        public Order Insert(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Provided order to insert was null. Please provide a valid value");

            return _orderRepository.Insert(entity);
        }

        public void Update(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Provided order to update was null. Please provide a valid value");

            _orderRepository.Update(entity);
        }

        public void Delete(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Provided order to delete was null. Please provide a valid value");

            _orderRepository.Delete(entity);
        }

        public void Delete(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(string.Format("Provided id {0} was not valid. Id should be different than empty identifier.", id));

            _orderRepository.Delete(id);
        }


        public Order BuildOrderFromJson(string json)
        {
            try
            {
                var result = new Order();
                JObject orderRequest = JObject.Parse(json);

                //customer information
                result.Customer = new Customer()
                {
                    Address = (string)orderRequest["Address"],
                    City = (string)orderRequest["City"],
                    CreationDate = DateTime.Now,
                    Email = (string)orderRequest["Email"],
                    FirstName = (string)orderRequest["FirstName"],
                    HouseNumber = Convert.ToInt32((string)orderRequest["HouseNumber"]),
                    LastName = (string)orderRequest["LastName"],
                    Title = (string)orderRequest["Title"],
                    ZipCode = (string)orderRequest["ZipCode"]
                };

                //order lines information
                result.OrderLines = new List<OrderLine>();
                foreach (var orderLine in orderRequest["OrderLines"])
                {
                    var relatedProduct = _productRepository.Get(Convert.ToInt32((string)orderLine["ProductId"]));
                    if (relatedProduct == null)
                        throw new Exception("At least one of the provided produts doesn't exist. Order cannot be processed.");

                    result.OrderLines.Add(new OrderLine()
                        {
                            Amount = Convert.ToInt32((string)orderLine["Amount"]),
                            ProductId = relatedProduct.Id,
                            CreationDate = DateTime.Now,
                            VatPercentageFromProduct = relatedProduct.VatPercentage
                        });
                }

                //other order fields
                result.CheckoutDate = DateTime.Now;
                result.CreationDate = DateTime.Now;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem creating the order based on the provided information.", ex);
            }
        }

        private void PopulateProductsForOrders(IQueryable<Order> orders)
        {
            foreach (var order in orders)
            {
                PopulateProductsForOrder(order);
            }
        }

        private void PopulateProductsForOrder(Order order)
        {
            foreach (var orderLine in order.OrderLines)
            {
                orderLine.Product = _productRepository.Get(orderLine.ProductId);
            }
        }
    }
}
