using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreProject.Models;

namespace BookStoreProject.Controllers
{
    public class RegisterController : Controller
    {
        BookStoreEntities2 dbcontext = new BookStoreEntities2();
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            try
            {
                dbcontext.Customers.Add(customer);
                dbcontext.SaveChanges();
                ViewBag.Message ="Successfully Registered" ;
            }
            catch (Exception e)
            {
                ViewBag.useralreadyexists = "Error" +e.Message;
            }
           
            return View();
        }
    }
}