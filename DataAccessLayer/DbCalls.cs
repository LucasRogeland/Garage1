using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DbCalls
    {
        public Models.Garage GetGarage()
        {
            Models.Garage garage = null;

            using (var db = new GarageDbContext())
            {
                garage = db.Garages.Include(x => x.ParkingSpaces).FirstOrDefault();
            }
            return garage;
        }


        public T Find<T>(int id) where T: class
        {
            T entity = null;
            using (var db = new GarageDbContext())
            {
                entity = db.Set<T>().Find(id);
            }
            return entity;
        }

        public void Update<T>(T entity) where T : class
        {
            using (var db = new GarageDbContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Load<T>(T Entity , string load) where T: class
        {
            using (var db = new GarageDbContext())
            {
                db.Entry(Entity).Reference(load).Load();
            }
            
        }

        public List<Models.Member> GetMembers() {
            List<Models.Member> members = new List<Models.Member>();

            using (var db = new GarageDbContext())
            {
                members = db.Members.ToList();
            }
            return members;
        }

        public List<Models.Member> GetMembers(string Name)
        {
            List<Models.Member> members = new List<Models.Member>();

            using (var db = new GarageDbContext())
            {
                members = db.Members.Where(x => x.FirstName.StartsWith(Name) || x.LastName.StartsWith(Name)).ToList();
            }
            return members;
        }

        public Models.Member GetMember(string firstName, string lastName)
        {
            Models.Member member = null;

            using (var db = new GarageDbContext())
            {
                member = db.Members.Where(x => x.FirstName.StartsWith(firstName) && x.LastName.StartsWith(lastName)).FirstOrDefault();
            }
            return member;
        }

        public void AddMember(Models.Member member) {
            using (var db = new GarageDbContext())
            {
                db.Members.Add(member);
                db.SaveChanges();
            }
        }

        public void AddParkingDetails(Models.ParkingDetails details) {
            using (var db = new GarageDbContext())
            {
                db.ParkingDetails.Add(details);
                db.SaveChanges();
            }
        }

        public List<Models.ParkingDetails> GetParkingDetails()
        {
            List<Models.ParkingDetails> details = new List<Models.ParkingDetails>();

            using (var db = new GarageDbContext())
            {
                details = db.ParkingDetails.Include(x=>x.Member).Include(x => x.Vehicle).Include(x => x.Vehicle.VehicleType).ToList();
            }
            return details;
        }
        
        public List<Models.ParkingDetails> GetParkingDetails(string License)
        {
            List<Models.ParkingDetails> details = new List<Models.ParkingDetails>();

            using (var db = new GarageDbContext())
            {
                details = db.ParkingDetails.Where(x => x.Vehicle.License.StartsWith(License)).Include(x => x.Member).Include(x => x.Vehicle).Include(x => x.Vehicle.VehicleType).ToList();
            }
            return details;
        }

        public Models.ParkingDetails GetParkingDetails(int id)
        {
            Models.ParkingDetails details = null;

            using (var db = new GarageDbContext())
            {
                details = db.ParkingDetails.Include(x => x.Member).Include(x => x.Vehicle).Include(x => x.Vehicle.VehicleType).Where(x => x.Id == id).FirstOrDefault();
            }
            return details;
        }
    }
}
