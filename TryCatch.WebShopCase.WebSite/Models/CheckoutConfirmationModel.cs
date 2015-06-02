using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TryCatch.WebShopCase.WebSite.Models
{
    public class CheckoutConfirmationModel
    {
        [Required]
        [Display()]
        public CustomerTitle Title { get; set; }

        public IEnumerable<SelectListItem> TitleOptions
        {
            get
            {
                IEnumerable<CustomerTitle> customerTitles = Enum.GetValues(typeof(CustomerTitle)).Cast<CustomerTitle>();
                return customerTitles.Select(ct => new SelectListItem { Text = ct.ToString(), Value = ((int)ct).ToString() });
            }
            private set { }
        } 

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [Range(0,1000)]
        public string HouseNumber { get; set; }

        [Required]
        [Range(10000, 99999)]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public IList<OrderLine> OrderLines { get; set; }


    }
}