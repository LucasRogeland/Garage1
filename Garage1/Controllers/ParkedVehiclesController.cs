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

            if (model.Vehicles == null) {
                model.Vehicles = db.Vehicles.ToList();
            }

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

        public ActionResult Search(string searchTerm = null)
        {
            var model = db.Vehicles
                .OrderBy(i => i.Licens)
                .Where(r => string.IsNullOrEmpty(searchTerm) || r.Licens==searchTerm)
                .ToList();

            return View(model);
        }

        public ActionResult SearchAj(SearchViewModel model ) {

            List<ParkedVehicle> vehicles = new List<ParkedVehicle>();

            vehicles = (from vehicle in db.Vehicles
                        where (model.License == null ||  vehicle.Licens.StartsWith(model.License)) &&
                              (model.Manufacturer == null || vehicle.Manufacturer.StartsWith(model.Manufacturer)) && 
                              (model.VModel == null || vehicle.Model.StartsWith(model.VModel)) &&
                              (model.Color == null || vehicle.Color.StartsWith(model.Color)) &&
                              (model.VehicleType == Enums.Vehicles.Undefined || model.VehicleType == vehicle.VehicleType)
                        select vehicle).ToList();
            return PartialView("IndexListPartial", new IndexListPartialViewModel() { Vehicles = vehicles, CssClassDesc = "", Target = "" });
        }

        public ActionResult SearchAjAll() {
            return PartialView("IndexListPartial", new IndexListPartialViewModel() { Vehicles = db.Vehicles, CssClassDesc = "", Target = "" });
        }

        public ActionResult SortAj(SortViewModel model)
        {
            IEnumerable<ParkedVehicle> list;

            if (model.SortBy == "Licens")
            {
                if(!model.desc)
                    list = db.Vehicles.OrderBy(x => x.Licens);
                else
                    list = db.Vehicles.OrderByDescending(x => x.Licens);
            }
            else if (model.SortBy == "VehicleType")
            {
                if (!model.desc)
                    list = db.Vehicles.OrderBy(x => x.VehicleType.ToString());
                else
                    list = db.Vehicles.OrderByDescending(x => x.VehicleType.ToString());
                
            }
            else if (model.SortBy == "Manufacturer")
            {
                if (!model.desc)
                    list = db.Vehicles.OrderBy(x => x.Manufacturer);
                else
                    list = db.Vehicles.OrderByDescending(x => x.Manufacturer);
            }
            else if (model.SortBy == "Model")
            {
                if (!model.desc)
                    list = db.Vehicles.OrderBy(x => x.Model);
                else
                    list = db.Vehicles.OrderByDescending(x => x.Model);
            }
            else if (model.SortBy == "Color")
            {
                if (!model.desc)
                    list = db.Vehicles.OrderBy(x => x.Color);
                else
                    list = db.Vehicles.OrderByDescending(x => x.Color);
            }
            else if (model.SortBy == "CheckInTime")
            {
                if (!model.desc)
                    list = db.Vehicles.OrderBy(x => x.CheckInTime);
                else
                    list = db.Vehicles.OrderByDescending(x => x.CheckInTime);
            }
            else
            {
                list = db.Vehicles;
            }

            var cssClass = "";

            if (!model.desc)
                cssClass = "desc";

            return PartialView("IndexListPartial", new IndexListPartialViewModel() { Vehicles = list, CssClassDesc = cssClass, Target = model.SortBy });
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
        public ActionResult Create([Bind(Include = "Licens,VehicleType,Manufacturer,Model,Color,CheckInTime,NumberOfWheels")] ParkedVehicle parkedVehicle)
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
                    model.Message = "Your " + parkedVehicle.VehicleType.ToString().ToLower() + " (" +parkedVehicle.Licens +") is now parked";
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
        public ActionResult Edit([Bind(Include = "Licens,VehicleType,Manufacturer,Model,Color,CheckInTime,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                DateTime vec = (from v in db.Vehicles
                                    where v.Licens == parkedVehicle.Licens
                                    select v.CheckInTime).First();
                parkedVehicle.CheckInTime = vec;
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
                model.Message = "Your " + parkedVehicle.VehicleType.ToString().ToLower() + " (" + parkedVehicle.Licens + ") has been checked out.";
                return RedirectToAction("index", model);
            }
            else
            {
                IndexViewModel model = new IndexViewModel();
                model.Feedback = true;
                model.Success = true;
                model.Message = "We werent able to check out your " + parkedVehicle.VehicleType.ToString().ToLower() + ", " + parkedVehicle.Licens + ".";
                return RedirectToAction("index", model);
            }
        }

        public ActionResult Receipts(ReceiptsViewModel model) {

            return View();
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
