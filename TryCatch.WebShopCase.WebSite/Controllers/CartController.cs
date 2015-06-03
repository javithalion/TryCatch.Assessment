using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryCatch.WebShopCase.WebSite.Models;
using TryCatch.WebShopCase.WebSite.Services.Interfaces;

namespace TryCatch.WebShopCase.WebSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ICheckoutConfirmationService _checkoutConfirmationService;

        public CartController(ICheckoutConfirmationService checkoutConfirmationService)
        {
            _checkoutConfirmationService = checkoutConfirmationService;
        }

        //
        // GET: /Cart/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            CheckoutConfirmationModel model = _checkoutConfirmationService.GetEmptyCheckOutConfirmationModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(CheckoutConfirmationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new HttpException("Provided checkout information was not valid");

                var orderIdentifier = _checkoutConfirmationService.CheckOutAction(model);
                ViewData["Messages.Success"] = string.Format("Congratulations. Order was properly registered in our system with id {0}!", orderIdentifier);
                return View("Confirmation");
            }
            catch
            {
                return View();
            }
        }


    }
}
