using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PayPal.Api;
using PeriodicalsFinal.Attributes;
using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Payment;
using PeriodicalsFinal.DataAccess.Repository;

namespace PeriodicalsFinal.Controllers
{
    [MyAuthorize()]
    public class PaymentController : Controller
    {
        private readonly SubscribeRepository _subscribeRepository = new SubscribeRepository();
        private ApplicationUserManager _userManager;
        private readonly PaymentBonus paymentBonus = new PaymentBonus();


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [MyCustomException]
        public ActionResult PaymentWithPaypal(float? paymentPrice, string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();

            if(paymentPrice < 1)
            {
                return RedirectToAction("Failure", new { status = "Minimum payment 1$" });
            }
            else
            {
                if (paymentPrice != null)
                {
                    Session.Add("payPrice", paymentPrice);
                }
            }

            //A resource representing a Payer that funds a payment Payment Method as paypal
            //Payer Id will be returned when payment proceeds or click to pay
            string payerId = Request.Params["PayerID"];

            if (string.IsNullOrEmpty(payerId))
            {
                //this section will be executed first because PayerID doesn't exist
                //it is returned by the create function call of the payment class

                // Creating a payment
                // baseURL is the url on which paypal sendsback the data.
                string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                            "/Payment/PaymentWithPayPal?";

                //here we are generating guid for storing the paymentID received in session
                //which will be used in the payment execution

                var guid = Convert.ToString((new Random()).Next(100000));

                //CreatePayment function gives us the payment approval url
                //on which payer is redirected for paypal account payment

                float.TryParse(paymentPrice.ToString(), out float payPrice);

                var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, payPrice);

                //get links returned from paypal in response to Create function call

                var links = createdPayment.links.GetEnumerator();

                string paypalRedirectUrl = null;

                while (links.MoveNext())
                {
                    Links lnk = links.Current;

                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        //saving the payapalredirect URL to which user will be redirected for payment
                        paypalRedirectUrl = lnk.href;
                    }
                }

                // saving the paymentID in the key guid
                Session.Add(guid, createdPayment.id);

                return Redirect(paypalRedirectUrl);
            }
            else
            {
                // This function exectues after receving all parameters for the payment

                var guid = Request.Params["guid"];

                var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                //If executed payment failed then we will show payment failure message to user
                if (executedPayment.state.ToLower() != "approved")
                {
                    return RedirectToAction("Failure", new { status = executedPayment.failure_reason });
                }
            }

            // Adding money to the user account
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());

            float.TryParse(Session["payPrice"].ToString(), out float replenishmentPrice);

            // Checks for bonuses
            float bonusIfBirthday = paymentBonus.PayBonusIfBirhday(User.Identity.Name, replenishmentPrice);
            float bonusIfLargePayment = paymentBonus.IsLargePayment(replenishmentPrice);

            float bonus = bonusIfBirthday + bonusIfLargePayment;

            user.Account += replenishmentPrice + bonus;
            UserManager.Update(user);

            //on successful payment, show success page to user.
            return RedirectToAction("Index", "Manage");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl,float paymentPrice)
        {

            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };
            string paymentName = $"Personal account replenishment for {User.Identity.Name} on Cinnabon";

            //Adding Item Details like name, currency, price etc
            itemList.items.Add(new Item()
            {
                name = paymentName,
                currency = "USD",
                price = paymentPrice.ToString(),
                quantity = "1",
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            //var details = new Details()
            //{
            //    tax = "0",
            //    shipping = "0",
            //    subtotal = "0"
            //};

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = paymentPrice.ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                //details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = paymentName,
                invoice_number = Guid.NewGuid().ToString(), //Generate an Invoice No
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }


        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failure(string status)
        {
            ViewBag.Message = status;

            return View();
        }


        public ActionResult SubscibeConfirm(Guid editionId, string successReturn, float? paymentPrice)
        {
            ViewBag.EditionId = editionId;
            ViewBag.UrlReturn = successReturn;
            ViewBag.Price = paymentPrice;

            return View();
        }


        public ActionResult Subscribe(Guid editionId, string successReturn, float? paymentPrice)
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            bool isUserSubscribed = _subscribeRepository.IsUserSubscribed(editionId, user.UserName);

            if (!isUserSubscribed)
            {
                if (user.Account >= paymentPrice)
                {
                    SubscriptionModel subscription = new SubscriptionModel() { Id = user.Id, EditionId = editionId };

                    _subscribeRepository.Create(subscription);
                    _subscribeRepository.Save();


                    user.Account -= paymentPrice;
                    UserManager.Update(user);

                    return Redirect(successReturn);
                }

                return RedirectToAction("Failure", new { status = "insufficient funds in the account" });

            }

            return RedirectToAction("Failure", new { status = "You are already subscribed to this edition" });

        }

        public ActionResult PaymentConfirm()
        {
            return View();
        }


    }
}