using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Models;

namespace Garage1.Controllers
{
    public class ParkingDetailsController : Controller
    {
        private GarageDbContext db = new GarageDbContext();

        // GET: ParkingDetails
        public ActionResult Index()
        {
            var parkingDetails = db.ParkingDetails.Include(p => p.Member).Include(p => p.Vehicle);
            return View(parkingDetails.ToList());
        }

        // GET: ParkingDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingDetails parkingDetails = db.ParkingDetails.Find(id);
            if (parkingDetails == null)
            {
                return HttpNotFound();
            }
            return View(parkingDetails);
        }

        // GET: ParkingDetails/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "License");
            return View();
        }

        // POST: ParkingDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,VehicleId,ParkingSpaceId,CheckInTime")] ParkingDetails parkingDetails)
        {
            if (ModelState.IsValid)
            {
                db.ParkingDetails.Add(parkingDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", parkingDetails.MemberId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "License", parkingDetails.VehicleId);
            return View(parkingDetails);
        }

        // GET: ParkingDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingDetails parkingDetails = db.ParkingDetails.Find(id);
            if (parkingDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", parkingDetails.MemberId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "License", parkingDetails.VehicleId);
            return View(parkingDetails);
        }

        // POST: ParkingDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,VehicleId,ParkingSpaceId,CheckInTime")] ParkingDetails parkingDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", parkingDetails.MemberId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "License", parkingDetails.VehicleId);
            return View(parkingDetails);
        }

        // GET: ParkingDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingDetails parkingDetails = db.ParkingDetails.Find(id);
            if (parkingDetails == null)
            {
                return HttpNotFound();
            }
            return View(parkingDetails);
        }

        // POST: ParkingDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkingDetails parkingDetails = db.ParkingDetails.Find(id);
            db.ParkingDetails.Remove(parkingDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
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
