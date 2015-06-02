using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryCatch.WebShopCase.WebSite.Models;

namespace TryCatch.WebShopCase.WebSite.Services.Interfaces
{
    public interface ICheckoutConfirmationService
    {
        CheckoutConfirmationModel GetEmptyCheckOutConfirmationModel();

        void CheckOutAction(CheckoutConfirmationModel model);
    }
}
