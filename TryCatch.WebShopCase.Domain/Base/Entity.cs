using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.WebShopCase.Domain.Base
{
    public class Entity<T>
    {
        /// <summary>
        /// Gets or sets the identifier for this entity
        /// </summary>
        public T Id { get; set; }
    }
}
