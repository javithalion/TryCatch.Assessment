using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.WebShopCase.Domain.Base
{
    public class TrackedEntity<T> : Entity<T>
    {
        /// <summary>
        /// Gets or sets when this entyty was created
        /// </summary>
        public DateTime CreationDate { get; set; }
    }
}
