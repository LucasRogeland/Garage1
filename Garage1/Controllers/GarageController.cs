using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using DataAccessLayer;

namespace Garage1.Controllers
{
    public class GarageController : Controller
    {
        // GET: Garage
        public ActionResult Index()
        {
            DbCalls dbc = new DbCalls();
            var garage = dbc.GetGarage();
            return View(garage);
        }

        public ActionResult Park()
        {
            var dbc = new DbCalls();

            var garage = dbc.GetGarage();

            var model = new ViewModel.Garage.ParkViewModel();
            
            if (garage.NrOfParkedVehicles >= garage.Capacity)
            {
                model.Feedback = true;
                model.CssClass = "danger";
                model.Message = "Garage is Full!";
                return View(model);
            }

            GarageDbContext db = new GarageDbContext();

            model.VehicleTypes = new SelectList(db.VehicleTypes, "Id", "Name");
            model.Garages = new SelectList(db.Garages, "Id", "Id");
            model.ParkingSpots = new SelectList(db.ParkingSpaces, "Id", "Id");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Park(ViewModel.Garage.ParkViewModel model) {
            var dbc = new DbCalls();

            var details = model.ParkingDetails;
            details.CheckInTime = DateTime.Now;
            var modelNew = new ViewModel.Garage.ParkedViewModel();
            var garage = dbc.GetGarage();
            if (garage.NrOfParkedVehicles >= garage.Capacity) {
                modelNew.Feedback = true;
                modelNew.CssClass = "fail";
                modelNew.Message = "The Garage is Full!";
                return View(modelNew);
            }

            if (ModelState.IsValid) {

                var arr = model.MemberName.Split(' ');
                var firstName = arr[0];
                var lastName = arr[1];
                var member = dbc.GetMember(firstName, lastName);
                details.MemberId = member.Id;
                garage.NrOfParkedVehicles++;
                dbc.Update(garage);
                dbc.AddParkingDetails(details);

            }

            
            modelNew.Feedback = true;
            modelNew.CssClass = "success";
            modelNew.Message = "Your car was parked";

            return RedirectToAction("Parked", modelNew);
        }

        public ActionResult Parked(ViewModel.Garage.ParkedViewModel model)
        {
            var dbc = new DbCalls();

            if(model.ParkingDetails == null)
                model.ParkingDetails = dbc.GetParkingDetails();

            return View(model);
        }

        public ActionResult Details(int id) {
            var dbc = new DbCalls();
            var model = dbc.GetParkingDetails(id);
            
            return View(model);
        }

        public ActionResult Search(string License, string vType) {
            var dbc = new DbCalls();

            var vehicles = dbc.GetParkingDetails(License);

            return PartialView("ParkedListPartial", vehicles);
        }

        public ActionResult SearchAll()
        {
            var dbc = new DbCalls();

            var vehicles = dbc.GetParkingDetails();

            return PartialView("ParkedListPartial", vehicles);
        }
    }
}