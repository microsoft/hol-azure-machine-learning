using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace powerbi_sample_app.Controllers
{
    public class AircraftController : Controller
    {
        // GET: Aircraft
        public ActionResult Index()
        {
            return View();
        }

        // GET: Aircraft/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Aircraft/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aircraft/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aircraft/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Aircraft/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Aircraft/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Aircraft/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
