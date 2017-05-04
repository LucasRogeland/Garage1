using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage1.Controllers
{
    public class MembersController : Controller
    {
        // GET: Members
        public ActionResult Index()
        {
            DbCalls dbc = new DbCalls();
            List<Member> members = dbc.GetMembers();
            return View(members);
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member member) {

            if (ModelState.IsValid) {

                var dbc = new DbCalls();
                dbc.AddMember(member);

            }
            return View();
        }

        public ActionResult Search(string Name) {
            var dbc = new DbCalls();
            Name = Name.Trim();
            List<Member> model;

            var arr = Name.Split(' ');

            if (arr.Length > 1)
            {
                var firstName = arr[0];
                var lastName = arr[1];
                model = new List<Member>();
                var member = dbc.GetMember(firstName, lastName);
                if (member != null)
                {
                    model.Add(member);
                }
            }
            else {
                model = dbc.GetMembers(Name);
            }
            
            return PartialView("IndexListPartial", model);
        }

        public ActionResult SearchA(string Name)
        {
            var dbc = new DbCalls();
            List<Member> model = new List<Member>();

            var arr = Name.Split(' ');

            if (arr.Length > 1)
            {
                var firstName = arr[0];
                var lastName = arr[1];
                model = new List<Member>();
                var member = dbc.GetMember(firstName, lastName);
                if (member != null) {
                    model.Add(member);
                }
                
            }
            else
            {
                model = dbc.GetMembers(Name);
            }

            return PartialView("AutoCompleteViewPartial", model);
        }

        public ActionResult SearchAll() {
            var dbc = new DbCalls();
            var model = dbc.GetMembers();
            return PartialView("IndexListPartial", model);
        }

    }
}