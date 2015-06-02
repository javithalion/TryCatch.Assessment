using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain.Base;

namespace TryCatch.WebShopCase.Domain
{
    public class Product : Entity<int>
    {
        /// <summary>
        /// Gets or sets the name for this product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for this product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the PictureURL for this product
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the Price without Vat taxes for this product
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the VAT taxes in % for this product
        /// </summary>
        public double VatPercentage { get; set; }

    }
}
