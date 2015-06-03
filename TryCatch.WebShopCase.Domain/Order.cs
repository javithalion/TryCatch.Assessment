using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain.Base;

namespace TryCatch.WebShopCase.Domain
{
    public class Order : TrackedEntity<Guid>
    {
        /// <summary>
        /// Creates a new instance of the class Order
        /// </summary>
        public Order()
        {
            this.OrderLines = new List<OrderLine>();
        }
        /// <summary>
        /// Gets or sets the order lines for this order
        /// </summary>
        public virtual IList<OrderLine> OrderLines { get; set; }

        // <summary>
        /// Gets or sets the customer for this order
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the check out date for this order
        /// </summary>
        public virtual DateTime CheckoutDate { get; set; }
        
        /// <summary>
        /// Gets or sets the total price (Sub total + Vat) for this order
        /// </summary>
        public virtual double Total { get; set; }

        /// <summary>
        /// Gets or sets the sub total price for this order
        /// </summary>
        public virtual double SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the VAT price for this order
        /// </summary>
        public virtual double Vat { get; set; }
    }
}
