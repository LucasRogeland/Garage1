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
        private int garageId = 1;

        // GET: ParkedVehicles
        public ActionResult Index(IndexViewModel model)
        {

            if (model.ParkingDetails == null) {
                model.ParkingDetails = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.ToList();
            }

            return View(model);
        }

            // GET: ParkedVehicles/Details/5
            public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingDetails details = db.ParkingDetails.FirstOrDefault(x=>x.Id == id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        public ActionResult Search(string searchTerm = null)
        {
            var model = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.Where( m => m.Vehicle.Licens == searchTerm)
                .OrderBy(i => i.Vehicle.Licens)
                .Where(r => string.IsNullOrEmpty(searchTerm) || r.Vehicle.Licens==searchTerm)
                .ToList();

            return View(model);
        }

        public ActionResult SearchAj(SearchViewModel model ) {

            List<ParkingDetails> parkingDetails = new List<ParkingDetails>();

            parkingDetails = (from details in db.Garages.FirstOrDefault( m => m.Id == garageId).ParkingDetails
                        where (model.License == null ||  details.Vehicle.Licens.StartsWith(model.License)) &&
                              (model.Manufacturer == null || details.Vehicle.Manufacturer.StartsWith(model.Manufacturer)) && 
                              (model.VModel == null || details.Vehicle.Model.StartsWith(model.VModel)) &&
                              (model.Color == null || details.Vehicle.Color.StartsWith(model.Color)) &&
                              (model.VehicleType == Enums.Vehicles.Undefined || model.VehicleType == details.Vehicle.VehicleType)
                        select details).ToList();
            return PartialView("IndexListPartial", new IndexListPartialViewModel() { Details = parkingDetails, CssClassDesc = "", Target = "" });
        }

        public ActionResult SearchAjAll() {
            return PartialView("IndexListPartial", new IndexListPartialViewModel() { Details = db.Garages.FirstOrDefault( m => m.Id == garageId).ParkingDetails, CssClassDesc = "", Target = "" });
        }

        public ActionResult SortAj(SortViewModel model)
        {
            IEnumerable<ParkingDetails> list;

            if (model.SortBy == "Licens")
            {
                if(!model.desc)
                    list = db.Garages.FirstOrDefault( m => m.Id == garageId ).ParkingDetails.OrderBy(x => x.Vehicle.Licens);
                else
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderByDescending(x => x.Vehicle.Licens);
            }
            else if (model.SortBy == "VehicleType")
            {
                if (!model.desc)
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderBy(x => x.Vehicle.VehicleType.ToString());
                else
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderByDescending(x => x.Vehicle.VehicleType.ToString());
                
            }
            else if (model.SortBy == "Manufacturer")
            {
                if (!model.desc)
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderBy(x => x.Vehicle.Manufacturer);
                else
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderByDescending(x => x.Vehicle.Manufacturer);
            }
            else if (model.SortBy == "Model")
            {
                if (!model.desc)
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderBy(x => x.Vehicle.Model);
                else
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderByDescending(x => x.Vehicle.Model);
            }
            else if (model.SortBy == "Color")
            {
                if (!model.desc)
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderBy(x => x.Vehicle.Color);
                else
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderByDescending(x => x.Vehicle.Color);
            }
            else if (model.SortBy == "CheckInTime")
            {
                if (!model.desc)
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderBy(x => x.CheckInTime);
                else
                    list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails.OrderByDescending(x => x.CheckInTime);
            }
            else
            {
                list = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails;
            }

            var cssClass = "";

            if (!model.desc)
                cssClass = "desc";

            return PartialView("IndexListPartial", new IndexListPartialViewModel() { Details = list, CssClassDesc = cssClass, Target = model.SortBy });
        }

        public ActionResult Statistics() {
            var vehicles = db.Garages.FirstOrDefault(m => m.Id == garageId).ParkingDetails;
            int nrOfWheels = 0;
            double totalCost = 0;

            foreach(var v in vehicles)
            {
                nrOfWheels += v.Vehicle.NumberOfWheels;
                totalCost += ((DateTime.Now - v.CheckInTime).TotalSeconds * 60);
            }

            StatisticsViewModel model = new StatisticsViewModel() {
                NrOfParkedVehicles = vehicles.Count(), NrOfParkedCars = vehicles.Where(v => v.Vehicle.VehicleType == Enums.Vehicles.Car).Count(),
                NrOfParkedBuss = vehicles.Where(v => v.Vehicle.VehicleType == Enums.Vehicles.Buss).Count(),
                NrOfParkedMotorcycle = vehicles.Where(v => v.Vehicle.VehicleType == Enums.Vehicles.Motorcycle).Count(),
                NrOfWheels = nrOfWheels,
                TotalCost = totalCost
            };

            return View(model);
        }

        // GET: ParkedVehicles/Create
        public ActionResult Create()
        {
            var garage = db.Garages.FirstOrDefault(m => m.Id == garageId);
            if (garage.ParkingDetails.Count() >= garage.Capacity)
            {
                return RedirectToAction("index", new IndexViewModel { Feedback = true, Success = false, Message = "The Garage Is Full!" });
            }
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkedVehicle vehicle)
        {
            var garage = db.Garages.FirstOrDefault(m => m.Id == garageId);
            if (garage.ParkingDetails.Count() >= garage.Capacity)
            {
                return RedirectToAction("index", new IndexViewModel { Feedback = true, Success = false, Message = "The Garage Is Full!" });
            }
            var details = new ParkingDetails();
            details.CheckInTime = DateTime.Now;
            details.Vehicle = vehicle;
            if (ModelState.IsValid)
            {
              
                garage.ParkingDetails.Add(details);
                var success = db.SaveChanges();
                if (success > 0)
                {
                    var model = new IndexViewModel();
                    model.Feedback = true;
                    model.Success = true;
                    model.Message = "Your " + details.Vehicle.VehicleType.ToString().ToLower() + " (" + details.Vehicle.Licens +") is now parked";
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

            return RedirectToAction("index", new IndexViewModel { Feedback = true, Success = false, Message = "Anti Forgery Token Failed"});
        }

        // GET: ParkedVehicles/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingDetails details = db.Garages.FirstOrDefault( m => m.Id == garageId).ParkingDetails.FirstOrDefault( m => m.Id == id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParkingDetails details)
        {
            if (ModelState.IsValid)
            {

                DateTime vec = db.ParkingDetails.AsNoTracking().FirstOrDefault( m => m.Id == details.Id).CheckInTime;
                details.CheckInTime = vec;
                db.Entry(details.Vehicle).State = EntityState.Modified;
                db.Entry(details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(details);
        }

        // GET: ParkedVehicles/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingDetails details = db.ParkingDetails.Find(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            ParkingDetails details = db.ParkingDetails.Find(id);
            ParkedVehicle vec = details.Vehicle;

            ReceiptViewModel modelReceipt = new ReceiptViewModel();
            modelReceipt.Details = details;

            db.ParkedVehicles.Remove(details.Vehicle);
            db.ParkingDetails.Remove(details);
            var success = db.SaveChanges() > 0;
            modelReceipt.Details.Vehicle = vec;
            if (success)
            {
                
                
                return View("receipt", modelReceipt);
            }
            else
            {
                IndexViewModel model = new IndexViewModel();
                model.Feedback = true;
                model.Success = false;
                model.Message = "We werent able to check out your " + details.Vehicle.VehicleType.ToString().ToLower() + ", " + details.Vehicle.Licens + ".";
                return RedirectToAction("index", model);
            }
        }

        public ActionResult Receipt(ReceiptViewModel model) {
            if (model.Details == null)
                model = null;
            return View(model);
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
