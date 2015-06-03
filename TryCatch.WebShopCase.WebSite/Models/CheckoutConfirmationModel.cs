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
        [Display(Name = "Display_Customer_Title", ResourceType = typeof(WebSite.Resources.Resources))]
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
        [Display(Name = "Display_Customer_FirstName", ResourceType = typeof(WebSite.Resources.Resources))]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Display_Customer_LastName", ResourceType = typeof(WebSite.Resources.Resources))]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Display_Customer_Email", ResourceType = typeof(WebSite.Resources.Resources))]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Display_Customer_Address", ResourceType = typeof(WebSite.Resources.Resources))]
        public string Address { get; set; }

        [Required]
        [Range(0,1000)]
        [Display(Name = "Display_Customer_HouseNumber", ResourceType = typeof(WebSite.Resources.Resources))]
        public string HouseNumber { get; set; }

        [Required]
        [Range(10000, 99999)]
        [Display(Name = "Display_Customer_ZipCode", ResourceType = typeof(WebSite.Resources.Resources))]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Display_Customer_City", ResourceType = typeof(WebSite.Resources.Resources))]
        public string City { get; set; }

        public IList<OrderLine> OrderLines { get; set; }


    }
}