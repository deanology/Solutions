using Solution.HelperClasses;
using Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Controllers
{
    public class PaymentController : Controller
    {
        PaymentTypeHandler pth = new PaymentTypeHandler();
        PaymentHandler pym = new PaymentHandler();

        // GET: Payment
        /*public ActionResult Index(string searchString)
        {
            return View(pym.GetAllPayments(searchString));
        }*/
        public ActionResult Index()
        {
            return View(pym.GetAllPayments2());
        }
        public ActionResult PostPayment()
        {
            var model = pth.GetAllPaymentTypes();
            var payment = new Payment
            {
                PaymentTypes = model
            };
            return View(payment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostPayment(Payment model)
        {
            if (ModelState.IsValid)
            {
                if (pym.AddPayment(model))
                {
                    ViewBag.Message = "Payment Added Successfully";
                    ModelState.Clear();
                }
                return RedirectToAction("PostPayment");
            }

            else
            {
                return View(model);
            }
        }
        public ActionResult Details(int id)
        {
            return View(pym.GetPayment(id));
        }
        public ActionResult EditPayment(int id)
        {
            return View(pym.GetPayment(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPayment(PaymentUpdate model)
        {
            if (ModelState.IsValid)
            {
                if (pym.UpdatePayment(model))
                {
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Message = "UnSuccessfully";
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            if (pym.DeletePayment(id))
            {
                ViewBag.Message = "Payment Successfully Deleted";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        //delete a particular payment
    }
}