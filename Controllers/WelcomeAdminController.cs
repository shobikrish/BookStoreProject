using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreProject.Models;

namespace BookStoreProject.Controllers
{
    public class WelcomeAdminController : Controller
    {
        BookStoreEntities2 db = new BookStoreEntities2();
        // GET: WelcomeAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminHome()
        {
            string CustomerName = TempData.Peek("CustomerName").ToString();
            string CustomerEmail = TempData.Peek("CustomerEmail").ToString();
            ViewBag.Cname = CustomerName;
            ViewBag.Cemail = CustomerEmail;
            return View();
        }
        public ActionResult ViewAllBooks()
        {
            //string CustomerName = TempData.Peek("CustomerName").ToString();
            //string CustomerEmail = TempData.Peek("CustomerEmail").ToString();
            //ViewBag.Cname = CustomerName;
            //ViewBag.Cemail = CustomerEmail;
            string AdminEmail = TempData.Peek("AdminEmail").ToString();
            ViewBag.Aemail = AdminEmail;
            return View(db.Books.ToList());
        }
        public ActionResult ViewCustomerDetails()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult LowStockBooks()
        {
            //Book book = new Book();
            //using(BookStoreEntities db=new BookStoreEntities())
            //{

            //}
            var BookStock = db.Books.Where(b => b.BookQuantity < 5);
            return View(BookStock);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("HomePage", "HomePage");
        }
    }
}