using GoogleMaps.LocationServices;
using Records.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Records.Controllers
{
    [Authorize]
    public class PrimaryController : Controller
    {
        // GET: Primary
        ActionMonitor a = new ActionMonitor();
        ActionModel k = new ActionModel();
        Client c = new Client();
        DinnerModel m = new DinnerModel();
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ValidateForm v)
        {
            c.Title = v.Title;
            c.HostedBy = v.HostedBy;
            c.Description = v.Description;
            c.EventDate = v.EventDate;
            c.ContactPhone = v.ContactPhone;
            c.Address = v.Address;
            c.Country = v.Country;
            c.Latitude = v.Latitude;
            c.Longtitude = v.Longtitude;
            m.Add(c);
            m.Save();
            a.Name = Convert.ToString("Admin");
            a.ActionPerformed = "Created a Dinner";
            a.During = DateTime.Now;
            k.Add(a);
            k.Save();
            TempData["InsertMsg"] = "<script>alert('Data inserted successfully');</script>";

            return RedirectToAction("Display");
        }
       
        public ActionResult Display()
        {
            var data= m.FindAllDinners();
            return View(data);
        }
        public ActionResult DisplayUser()
        {
            var data = m.FindAllDinners();
            return View(data);
        }
        public ActionResult Edit( int Id)
        {
            var data = (ValidateForm) m.GetDinner(Id);
            //int r = data.DinnerId;
            return View(data);
           
        }
        [HttpPost]
        public ActionResult Edit(ValidateForm v)

        { Client c =m.GetDinner(v.DinnerId);
            c.DinnerID = v.DinnerId;
            c.Title = v.Title;
            c.HostedBy = v.HostedBy;
            c.Description = v.Description;
            c.EventDate = v.EventDate;
            c.ContactPhone = v.ContactPhone;
            c.Address = v.Address;
            c.Country = v.Country;
            
            
          c.Latitude = v.Latitude;
            c.Longtitude = v.Longtitude;
            // m.Add(c);
            m.Save();
            Response.Write("<script>alert('Data Updated successfully');</script>");
            return RedirectToAction("Display");
        }
       
        public ActionResult Details(int id)
        {
            var data =(ValidateForm) m.GetDinner(id);
            return View(data);
        }
        // GET: Primary/Details/5
        public ActionResult Delete(int id)
        {
            Client c = m.GetDinner(id);
            m.Delete(c);
            m.Save();
            TempData["DeleteMsg"] = "<script>alert('Data deleted successfully');</script>";
            return RedirectToAction("Display");
        }
        public ActionResult AllMaps()
        {
            return View();
        }
        public JsonResult GetMapMarker()
        {
            var ListOfAddress =m.FindAllDinners().ToList();
            return Json(ListOfAddress, JsonRequestBehavior.AllowGet);
        }
    public ActionResult temp()
        {
            return View();
        }

    }
}
