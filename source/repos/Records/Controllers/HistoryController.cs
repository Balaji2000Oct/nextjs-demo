using Records.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Records.Controllers
{
    public class HistoryController : Controller
    {
        ActionMonitor a = new ActionMonitor();
        ActionModel m = new ActionModel();
        BookerModel book = new BookerModel();
        BookedUser BTable = new BookedUser();
        UserModel usermod = new UserModel();
        DinnerModel d = new DinnerModel();
        // GET: History
        
        //ValidateForm v = new ValidateForm();

        public ActionResult Book(string n,string name)
        {
            var EM = usermod.GetUserByName(name);
            //string Title="dgfdg";
            a.Name = Convert.ToString(name) +"/"+ Convert.ToString(n);
            a.ActionPerformed = "Booked a Dinner";
            a.During = DateTime.Now;
            m.Add(a);
            m.Save();
            BTable.Name = name;
            BTable.EmailID = EM.EmailId;
            BTable.DinnerName = n;
            BTable.BookedDate = DateTime.Now;
            book.Add(BTable);
            book.Save();

            TempData["BookMsg"] = "<script>alert('You have booked successfully');</script>";
            return RedirectToAction("DisplayUser","Primary");
        }

        public ActionResult Show()
        {
            var data = m.FindAllActions();
            return View(data);
        }

        public ActionResult DisplayBook(string name)
        {
            var data = book.FindBookedDinnersDinners(name);

            if (data.Count() == 0)
            {
                data = null;
            }
            
            return View(data);
        }

        public ActionResult Man(string Name)
        {
            var Id = d.GetUserByDinner(Name);

            return RedirectToAction("Details", new RouteValueDictionary(
                            new { controller ="Primary", id=Id.DinnerID }));
        }

        public ActionResult BookersList()
        {

            var data = book.FindAllUsers();
            return View(data);
        }
        public ActionResult Delete(int id)

        {
            var data = book.GetUser(id);
            book.Delete(data);
            book.Save();
            a.Name = Convert.ToString(data.Name);
            a.ActionPerformed = "Canceled a Dinner";
            a.During = DateTime.Now;
            m.Add(a);
            m.Save();
            TempData["CancelMsg"] = "<script>alert('You had Cancel the booking successfully');</script>";
            return RedirectToAction("DisplayBook", new RouteValueDictionary(
                            new { controller = "History", Name= data.Name }));
        }
    }
}