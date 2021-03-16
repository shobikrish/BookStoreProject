using BookStoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreProject.Controllers
{
    public class HomePageController : Controller
    {
        BookStoreEntities2 db = new BookStoreEntities2();
        // GET: HomePage
        public ActionResult HomePage()
        {
            return View(db.Books.ToList());
        }
    }
}