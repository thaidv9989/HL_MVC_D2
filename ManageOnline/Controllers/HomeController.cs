using ManageOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace ManageOnline.Controllers
{
    
    public class HomeController : Controller
    {
        private MProductEntities _dbContext = new MProductEntities();


        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            bool rs = _dbContext.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password);
            if (rs)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", "Username or password incorrect");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}