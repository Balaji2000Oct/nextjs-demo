using Records.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Records.Controllers
{
    public class LoginController : Controller
    {
        ActionMonitor a = new ActionMonitor();
        ActionModel m = new ActionModel();
        // GET: Login
        UserModel u = new UserModel();
        User t = new User();

        public ActionResult Home()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logout");
            }
            return View();
        }
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserValidation user)

        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (user.Password.TrimEnd().Equals(user.rPassword.TrimEnd()))
            {
                t.Id = user.Id;
                t.Name = user.Name;
                t.EmailId = user.Email;
                t.Password = u.Enc(user.Password);
               TempData["a"]= Convert.ToString(user.Name);
                TempData["b"] = Convert.ToString(user.Email);
                u.Add(t);
                u.Save();
                string te = Convert.ToString(TempData["a"]);
                a.Name = te;
                a.ActionPerformed = "Create an Account";
                a.During = DateTime.Now;
                m.Add(a);
                m.Save();
                return RedirectToAction("Register");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public ActionResult Userlist()
        {
            var r = u.FindAllUsers();
            return View(r);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterValidation registerValidation)

        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData["a"]= Convert.ToString(registerValidation.Name);
            string temp =Convert.ToString( TempData["a"]);
            var data = u.GetUserByName(registerValidation.Name);
            var encpass = u.Dec(data.Password.TrimEnd());
            bool userName = data.Name.TrimEnd().Equals(registerValidation.Name);
            bool userEmail = data.EmailId.TrimEnd().Equals(registerValidation.Email);
            bool userPassword = encpass.TrimEnd().Equals(registerValidation.Password);
            if (userName && userEmail && userPassword)
            {
                var ticket = new FormsAuthenticationTicket(registerValidation.Name,true,3000);
                string encrypt = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);
                cookie.Expires = DateTime.Now.AddHours(3000);
                cookie.HttpOnly=true;
                Response.Cookies.Add(cookie);
                a.Name = temp;
                a.ActionPerformed = "Login";
                a.During = DateTime.Now;
                m.Add(a);
                m.Save();
                if (registerValidation.Name.Equals("Admin"))
                    return RedirectToAction("AdminPage");
                else
                    return RedirectToAction("UserPage");
            }
            return View("Error");
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            var data = u.GetUser(id);
            u.Delete(data);
            u.Save();
            a.Name = data.Name;
            a.ActionPerformed = "Account Deletion";
            a.During = DateTime.Now;
            m.Add(a);
            m.Save();
            return RedirectToAction("Userlist");
        }
        [Authorize(Roles="Admin")]
        public ActionResult AdminPage()
        {
            return View();
        }
        [Authorize]
        public ActionResult UserPage()
        {
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            a.Name = User.Identity.Name;
            a.ActionPerformed = "Logout";
            a.During = DateTime.Now;
            m.Add(a);
            m.Save();
            return RedirectToAction("Home");
        }
    }
}