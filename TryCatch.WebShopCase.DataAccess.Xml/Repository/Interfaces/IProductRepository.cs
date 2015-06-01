using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain;
using TryCatch.WebShopCase.Infraestructure.Repository;

namespace TryCatch.WebShopCase.DataAccess.Xml.Repository.Interfaces
{
    public interface IProductRepository : ICrudRepository<Product,int>
    {
    }
}
