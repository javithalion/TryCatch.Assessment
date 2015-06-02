using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Interfaces;
using TryCatch.WebShopCase.Domain;
using TryCatch.WebShopCase.Services.Interfaces;

namespace TryCatch.WebShopCase.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<Product> FindAll()
        {
            return _productRepository.FindAll().ToList();
        }

        public IList<Product> FindAll(Expression<Func<Product, bool>> query)
        {
            return _productRepository.FindAll(query).ToList();
        }

        public Product Get(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format("Provided id {0} was not valid. Id should be geater than zero.", id));

            return _productRepository.Get(id);
        }

        public Product Insert(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Provided product to insert was null. Please provide a valid value");

            return _productRepository.Insert(entity);
        }

        public void Update(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Provided product to update was null. Please provide a valid value");

            _productRepository.Update(entity);
        }

        public void Delete(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Provided product to delete was null. Please provide a valid value");

            _productRepository.Delete(entity);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException(string.Format("Provided id {0} was not valid. Id should be geater than zero.", id));

            _productRepository.Delete(id);
        }
    }
}
