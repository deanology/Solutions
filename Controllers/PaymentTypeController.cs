using Solution.HelperClasses;
using Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Controllers
{
    public class PaymentTypeController : Controller
    {
        PaymentTypeService pth = new PaymentTypeService();
        // GET: PaymentType
        public ActionResult Index()
        {
            return View(pth.GetAllPaymentTypes());
        }
        //POST: PaymentType/Create
        public ActionResult CreatePaymentType()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentType(PaymentType model)
        {
           if (ModelState.IsValid)
           {
                if (pth.AddPaymentType(model))
                {
                        TempData["Message"] = "Payment Type Added Successfully";
                        ModelState.Clear();
                }
                else
                {
                    TempData["Message"] = "Payment Type cannot be added";
                }
                return View();
           }
               
            else
            {
                return View(model);
            }
        }
        //GET: PaymentType/Detail/1
        public ActionResult Details(int id)
        {
            return View(pth.GetPaymentType(id));
        }
        //GET: PaymentType/Edit/2
        public ActionResult Edit(int id)
        {
            return View(pth.GetPaymentType(id));
        }
        //POST: PaymentType/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentType model)
        {
            if (ModelState.IsValid)
            {
                if (pth.UpdatePaymentType(model))
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
            if (pth.DeletePaymentType(id))
            {
                TempData["Message"] = "Payment Type successfully deleted";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
    }
}