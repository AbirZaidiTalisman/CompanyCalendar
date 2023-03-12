using CompanyCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanyCalendar.Controllers
{
    public class UserController : Controller
    {
        CalendarDBEntities1 db = new CalendarDBEntities1();

        public ActionResult Login()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];
            

            if (getCookie != null)
            {
                int cookieUserID = Convert.ToInt32(getCookie["UserID"]);
                var details = db.Users.Where(u => u.UserID == cookieUserID).FirstOrDefault();

                if (details != null)
                {
                    return RedirectToAction("MyCalendar", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (email != null && password!=null)
            {
                var details = db.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();

                if (details != null)
                {

                    HttpCookie cookie = new HttpCookie("LoggedIn");
                    cookie["userName"] = details.UserName;
                    cookie["email"] = details.Email;
                    cookie["password"] = details.Password;
                    cookie["EmpID"] = details.EmpID;
                    cookie["UserID"] = details.UserID.ToString();
                    cookie.Expires.AddDays(365);
                    this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("MyCalendar", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid Cradentials");
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];


            if (getCookie != null)
            {
                getCookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(getCookie);
                return RedirectToAction("Login", "User");

            }


            return RedirectToAction("Login", "User");
        }
        public ActionResult ChangePassword(string email, string newPass, string repPass)
        {
            HttpCookie getCookie = Request.Cookies["LoggedIn"];

            
            string password = getCookie["password"].ToString();

            if (newPass != null && repPass != null)
            {
                var details = db.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();

                if (details != null)
                {
                    if (newPass == repPass)
                    {
                        details.Password = newPass;
                        db.SaveChanges();
                    }
                }

                getCookie.Values["userName"] = details.UserName;
                getCookie.Values["email"] = details.Email;
                getCookie.Values["EmpID"] = details.EmpID;
                getCookie.Values["UserID"] = details.UserID.ToString();
                getCookie.Values["password"] = newPass;
                getCookie.Expires.AddDays(365);
                this.ControllerContext.HttpContext.Response.Cookies.Add(getCookie);
            }
            return RedirectToAction("MyCalendar", "Home");
        }
    }
}