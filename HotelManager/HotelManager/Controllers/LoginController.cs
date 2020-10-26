using Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Data;

namespace HotelManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminVerification(Admin admin)
        {
            using (var context = new HmDbContext())
            {
                bool isVerified = context.Admins.Where(a => a.Username.ToLower().Equals(admin.Username.ToLower())).First().Password == admin.Password;
                if (isVerified) return RedirectToAction("AdminSearch", "Admin");//View("~/Views/Admin/AdminSearch.cshtml");
            }
            return View("AdminLogin");
        }

        public ActionResult ManagerLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ManagerVerification(User manager)
        {
            using (var context = new HmDbContext())
            {
               
               
               
                    bool isVerified = context.Users.Where(x => x.Username.ToLower().Equals(manager.Username.ToLower())).First().Password == manager.Password;
                    bool isActive = context.Users.Where(x => x.Username.ToLower().Equals(manager.Username.ToLower())).Where(y => y.Password == manager.Password).First().IsActive;
                    if (isVerified && isActive) return RedirectToAction("ManagerSearchRoom", "Manager");
                
            }
            return View("ManagerLogin");
        }
        public ActionResult UserRole()
        {
            return View();
        }
    }
}