using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TryCatch.WebShopCase.DataAccess.Xml.Repository.Interfaces;
using TryCatch.WebShopCase.Domain;
using TryCatch.WebShopCase.Infraestructure.Exceptions;

namespace TryCatch.WebShopCase.DataAccess.Xml.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _xmlFilePath;
        private static readonly Object _lockKey = new Object();

        public ProductRepository(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Provided file path was null or empty"); //TODO :: Move to resource file

            if (!File.Exists(filePath))
                throw new FileNotFoundException(string.Format("File related with provided path '{0}' doesn't exist", filePath)); //TODO :: Move to resource file

            _xmlFilePath = filePath;
        }

        public IQueryable<Product> FindAll()
        {
            try
            {
                lock (_lockKey)
                {
                    var xmlDocument = XDocument.Load(_xmlFilePath);
                    return xmlDocument.Descendants("product")
                        .Select(
                        pr => new Product()
                        {
                            Id = Convert.ToInt32(pr.Element("Id").Value),
                            Name = pr.Element("Name").Value,
                            Description = pr.Element("Description").Value,
                            PictureUrl = pr.Element("Picture").Value,
                            Price = Convert.ToDouble(pr.Element("Price").Value),
                            VatPercentage = Convert.ToDouble(pr.Element("VATPercentage").Value)
                        }
                        ).AsQueryable();
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing FindAll method", ex);
            }
        }

        public IQueryable<Product> FindAll(Expression<Func<Product, bool>> query)
        {
            try
            {
                lock (_lockKey)
                {
                    var xmlDocument = XDocument.Load(_xmlFilePath);
                    return xmlDocument.Descendants("product")
                        .Select(
                        pr => new Product()
                        {
                            Id = Convert.ToInt32(pr.Element("Id").Value),
                            Name = pr.Element("Name").Value,
                            Description = pr.Element("Description").Value,
                            PictureUrl = pr.Element("Picture").Value,
                            Price = Convert.ToDouble(pr.Element("Price").Value),
                            VatPercentage = Convert.ToDouble(pr.Element("VATPercentage").Value)
                        }
                        ).AsQueryable().Where(query);
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing FindAll method", ex);
            }
        }

        public Product Get(int id)
        {
            try
            {
                lock (_lockKey)
                {
                    Product result = null;

                    var xmlDocument = XDocument.Load(_xmlFilePath);
                    var xmlProduct = xmlDocument.Descendants("product")
                        .Where(pr => pr.Element("Id").Value.Equals(id.ToString()))
                        .FirstOrDefault();

                    if (xmlProduct != null)
                    {
                        result = new Product()
                        {
                            Id = Convert.ToInt32(xmlProduct.Element("Id").Value),
                            Name = xmlProduct.Element("Name").Value,
                            Description = xmlProduct.Element("Description").Value,
                            PictureUrl = xmlProduct.Element("Picture").Value,
                            Price = Convert.ToDouble(xmlProduct.Element("Price").Value),
                            VatPercentage = Convert.ToDouble(xmlProduct.Element("VATPercentage").Value)
                        };
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Get method", ex);
            }

        }

        public Product Insert(Product entity)
        {
            try
            {
                lock (_lockKey)
                {
                    int id = 1;
                    var xmlDocument = XDocument.Load(_xmlFilePath);
                    var newXmlProduct = new XElement("Products");
                    var element = xmlDocument.Descendants("product").Last();

                    if (element.HasAttributes)
                        id = Convert.ToInt32(element.Element("Id").Value) + 1;

                    newXmlProduct.Element("Id").Value = id.ToString();
                    newXmlProduct.Element("Name").Value = entity.Name;
                    newXmlProduct.Element("Description").Value = entity.Description;
                    newXmlProduct.Element("Picture").Value = entity.PictureUrl;
                    newXmlProduct.Element("Price").Value = entity.Price.ToString();
                    newXmlProduct.Element("VATPercentage").Value = entity.VatPercentage.ToString();

                    element.AddAfterSelf(newXmlProduct);
                    xmlDocument.Save(_xmlFilePath);

                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Insert method", ex);
            }
        }

        public void Update(Product entity)
        {
            try
            {
                lock (_lockKey)
                {
                    var xmlDocument = XDocument.Load(_xmlFilePath);
                    var element = xmlDocument.Descendants("product")
                        .Where(pr => pr.Element("Id").Value.Equals(entity.Id.ToString()))
                        .FirstOrDefault();

                    if (element == null)
                        throw new KeyNotFoundException((string.Format("Doesn't exist an entity with id = {0}", entity.Id)));

                    element.Element("Name").Value = entity.Name;
                    element.Element("Description").Value = entity.Description;
                    element.Element("Picture").Value = entity.PictureUrl;
                    element.Element("Price").Value = entity.Price.ToString();
                    element.Element("VATPercentage").Value = entity.VatPercentage.ToString();

                    xmlDocument.Save(_xmlFilePath);
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Update method", ex);
            }
        }

        public void Delete(Product entity)
        {
            Delete(entity.Id);
        }

        public void Delete(int id)
        {
            try
            {
                lock (_lockKey)
                {
                    var xmlDocument = XDocument.Load(_xmlFilePath);
                    var element = xmlDocument.Descendants("product")
                        .Where(pr => pr.Element("Id").Value.Equals(id.ToString()))
                        .FirstOrDefault();

                    if (element == null)
                        throw new KeyNotFoundException((string.Format("Doesn't exist an entity with id = {0}", id)));

                    element.Remove();
                    xmlDocument.Save(_xmlFilePath);
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException("There was a problem executing Delete method", ex);
            }
        }
    }
}
