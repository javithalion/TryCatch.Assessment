using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.Domain.Base;

namespace TryCatch.WebShopCase.Domain
{
    public class Customer : TrackedEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the title for this customer
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name for this customer
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name for this customer
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address for this customer
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// Gets or sets the email for this customer
        /// </summary>
        public virtual string Email { get; set; }

         /// <summary>
        /// Gets or sets the house number for this customer
        /// </summary>
        public virtual int HouseNumber { get; set; }

         /// <summary>
        /// Gets or sets the zip code for this customer
        /// </summary>
        public virtual string ZipCode { get; set; }

         /// <summary>
        /// Gets or sets the city for this customer
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Gets the desciption for this customer. First name and last name string
        /// </summary>
        /// <returns>The First name plus the las name for this customer</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
