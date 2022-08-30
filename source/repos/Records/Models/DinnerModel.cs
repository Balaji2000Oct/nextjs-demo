using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class DinnerModel
    {
      
            private DALDataContext db = new DALDataContext();

            //
            // Query Methods

            public IQueryable<Client> FindAllDinners()
            {
                return db.Clients;
            }

            public IQueryable<Client> FindUpcomingDinners()
            {
                return from dinner in db.Clients
                       where dinner.EventDate > DateTime.Now
                       orderby dinner.EventDate
                       select dinner;
            }

            public Client GetDinner(int id)
            {
                return db.Clients.SingleOrDefault(d => d.DinnerID == id);
            }
        public Client GetUserByDinner(string id)
        {
            return db.Clients.SingleOrDefault(d => d.HostedBy== id);
        }

        //
        // Insert/Delete Methods

        public void Add(Client dinner)
            {
                db.Clients.InsertOnSubmit(dinner);
            }

            public void Delete(Client dinner)
            {
                
                db.Clients.DeleteOnSubmit(dinner);
            }

            //
            // Persistence 

            public void Save()
            {
                db.SubmitChanges();
            }
        }
    
}