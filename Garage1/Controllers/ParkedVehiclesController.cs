using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage1.DataAccessLayer;
using Garage1.Models;
using Garage1.ViewModel;

namespace Garage1.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: ParkedVehicles
        public ActionResult Index(IndexViewModel model)
        {

            model.Vehicles = db.Vehicles.ToList();

            return View(model);
        }

            // GET: ParkedVehicles/Details/5
            public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.Vehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegNummer,VehicleType,Manufacturer,Model,Color,CheckInTime,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                parkedVehicle.CheckInTime = DateTime.Now;
                db.Vehicles.Add(parkedVehicle);
                var success = db.SaveChanges();
                if (success > 0)
                {
                    var model = new IndexViewModel();
                    model.Feedback = true;
                    model.Success = true;
                    model.Message = "Your " + parkedVehicle.VehicleType.ToString().ToLower() + " (" +parkedVehicle.RegNummer +") is now parked";
                    return RedirectToAction("Index", model);
                }
                else
                {

                    var model = new IndexViewModel();
                    model.Feedback = true;
                    model.Success = false;
                    model.Message = "We werent able to park your vehicle!";
                    return RedirectToAction("Index", model);

                }
                    
            }

            return View("index", parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.Vehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegNummer,VehicleType,Manufacturer,Model,Color,CheckInTime,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkedVehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle parkedVehicle = db.Vehicles.Find(id);
            if (parkedVehicle == null)
            {
                return HttpNotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ParkedVehicle parkedVehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(parkedVehicle);
            if (db.SaveChanges() > 0)
            {
                IndexViewModel model = new IndexViewModel();
                model.Feedback = true;
                model.Success = true;
                model.Message = "Your " + parkedVehicle.VehicleType.ToString().ToLower() + " (" + parkedVehicle.RegNummer + ") has been checked out.";
                return RedirectToAction("index", model);
            }
            else
            {
                IndexViewModel model = new IndexViewModel();
                model.Feedback = true;
                model.Success = true;
                model.Message = "We werent able to check out your " + parkedVehicle.VehicleType.ToString().ToLower() + ", " + parkedVehicle.RegNummer + ".";
                return RedirectToAction("index", model);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
