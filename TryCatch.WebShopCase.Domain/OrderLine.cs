using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain.Base;

namespace TryCatch.WebShopCase.Domain
{
    public class OrderLine : TrackedEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the product for this order line
        /// </summary>
        public virtual  Product Product { get; set; }

        /// <summary>
        /// Gets or sets the amount of products for this order line
        /// </summary>
        public virtual int Amount { get; set; }

        /// <summary>
        /// Gets or sets the VAT percentage related with the product in the moment of the checkout (in order to avoid calc issues if the product change its VAT)
        /// </summary>
        public virtual  double VatPercentageFromProduct { get; set; }

        /// <summary>
        /// Gets or sets the total price (Sub total + Vat) for this order line
        /// </summary>
        public virtual double Total { get; set; }

        /// <summary>
        /// Gets or sets the sub total price for this order line
        /// </summary>
        public virtual double SubTotal { get; set; }

        /// <summary>
        /// Gets or sets the VAT price for this order line
        /// </summary>
        public virtual double Vat { get; set; }
    }
}
