using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class AdminLoginController : Controller
    {
        BookStoreEntities2 dbcontext = new BookStoreEntities2();
        // GET: AdminLogin
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(LoginDetail loginDetail)
        {
            try
            {
                LoginDetail c1 = dbcontext.LoginDetails.FirstOrDefault(ud => ud.UserEmail== loginDetail.UserEmail && ud.Password == loginDetail.Password);
                if(c1!=null)
                {
                    TempData["AdminEmail"] = c1.UserEmail;
                    if (c1.Role == "Admin")
                    {
                        return RedirectToAction("ViewAllBooks", "WelcomeAdmin");
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid Credentials!Try Again";
                }
                      
            }
            catch (System.InvalidOperationException)
            {
                ViewBag.Message = "Invalid Credentials!Try Again";
            }
            return View();
        }
    }
}