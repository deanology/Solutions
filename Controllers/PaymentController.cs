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
        PaymentTypeService pth = new PaymentTypeService();
        PaymentService pym = new PaymentService();
        
        public ActionResult Index(string searchString)
        {
            return View(pym.GetAllPayments2(searchString));
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
        
    }
}