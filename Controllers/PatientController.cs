using Solution.HelperClasses;
using Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solution.Controllers
{
    public class PatientController : Controller
    {
        PatientService pth = new PatientService();
        // GET: Patient
        public ActionResult Index(string searchString)
        {
            return View(pth.GetAllPatients(searchString));
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Patient model)
        {
            if (ModelState.IsValid)
            {
                if (pth.AddPatient(model))
                {
                    ViewBag.Message = "Patient Added Successfully";
                    ModelState.Clear();
                }
                return View();
            }

            else
            {
                return View(model);
            }
        }
        //GET: Patient/Detail/1
        public ActionResult Details(int id)
        {
            
            return View(pth.GetPatient(id));
        }
        //GET: Patient/Edit/2
        public ActionResult Edit(int id)
        {
            return View(pth.GetPatient(id));
        }
        //POST: Patient/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Patient model)
        {
            if (ModelState.IsValid)
            {
                if (pth.UpdatePatient(model))
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
            if (pth.DeletePatient(id))
            {
                ViewBag.Message = "Patiient Successfully Deleted";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        //action to search a patient
    }
}