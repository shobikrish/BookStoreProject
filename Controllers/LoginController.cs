using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreProject.Models;
namespace BookStoreProject.Controllers
{
    public class LoginController : Controller
    {
        BookStoreEntities2 dbcontext = new BookStoreEntities2();
        // GET: Login
        //[Route("Customer Login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            try
            {
                Customer c1 = dbcontext.Customers.FirstOrDefault(ud => ud.CustomerEmail == customer.CustomerEmail && ud.Password == customer.Password);
                if (c1!=null)
                {
                    //Session["Customer_Email"] = c1.CustomerEmail;
                    TempData["CustomerName"] = c1.CustomerName;
                    TempData["CustomerEmail"] = c1.CustomerEmail;
                    return RedirectToAction("Home", "WelcomeCustomer");
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