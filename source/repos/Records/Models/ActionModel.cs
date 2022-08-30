using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Records.Models
{
    public class ActionModel
    {
        private ActionForUsersDataContext db = new ActionForUsersDataContext();
        public IQueryable<ActionMonitor> FindAllActions()
        {
            return db.ActionMonitors;
        }

       

       
        //
        // Insert/Delete Methods

        public void Add(ActionMonitor actionMonitor)
        {
            db.ActionMonitors.InsertOnSubmit(actionMonitor);
        }

        public void Delete(ActionMonitor actionMonitor)
        {

            db.ActionMonitors.DeleteOnSubmit(actionMonitor);
        }

        //
        // Persistence 

        public void Save()
        {
            db.SubmitChanges();
        }
    }
}