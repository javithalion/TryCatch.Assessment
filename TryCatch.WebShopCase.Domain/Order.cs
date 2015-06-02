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
        public IList<OrderLine> OrderLines { get; set; }

        /// <summary>
        /// Gets or sets the check out date for this order
        /// </summary>
        public DateTime CheckoutDate { get; set; }
        
        /// <summary>
        /// Gets or sets the total price (Sub total + Vat) for this order
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the sub total price for this order
        /// </summary>
        public double SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the VAT price for this order
        /// </summary>
        public double Vat { get; set; }
    }
}
