using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain;

namespace TryCatch.WebShopCase.Services.Interfaces
{
    public interface IOrderService : ICrudService<Order, Guid> 
    {
        Order BuildOrderFromJson(string value);
    }
}
