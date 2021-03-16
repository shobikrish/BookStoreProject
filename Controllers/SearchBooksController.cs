using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreProject.Models;

namespace BookStoreProject.Controllers
{

    public class SearchBooksController : Controller
    {
        BookStoreEntities2 db = new BookStoreEntities2();
        // GET: SearchBooks
        public ActionResult Search()
        {
            return View(db.Books.ToList());
        }
        public JsonResult GetSearchData(string SearchBy, string SearchValue)
        {
            List<Book> books = new List<Book>();
            if (SearchBy == "BookId")
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    var bid = Int32.Parse(SearchValue);
                    books = db.Books.Where(b => b.BookId == bid || SearchValue == null).ToList();

                }
                catch (FormatException)
                {
                    ViewBag.error = "Entered Value is not Category of the Book";
                }
                return Json(books, JsonRequestBehavior.AllowGet);
            }
            if (SearchBy == "BookName")
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    books = db.Books.Where(b => b.BookName.Contains(SearchValue) || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    ViewBag.error = "Entered Value is not Book Name";
                }
                return Json(books, JsonRequestBehavior.AllowGet);
            }
            if (SearchBy == "BookAuthor")
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    //string NameOftheAuthor = SearchValue;
                    books = db.Books.Where(b => b.BookAuthor.Contains(SearchValue) || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    ViewBag.error = "Entered Value is not Author of the Book";
                }
                return Json(books, JsonRequestBehavior.AllowGet);
            }
            if (SearchBy == "BookCategory")
            {
                try
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    //string NameoftheCategory = SearchValue;
                    books = db.Books.Where(b => b.BookCategory.Contains(SearchValue) || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    ViewBag.error = "Entered Value is not Category of the Book";
                }
                return Json(books, JsonRequestBehavior.AllowGet);
            }
            //if (SearchBy == "BookLanguage")
            else
            {
                db.Configuration.ProxyCreationEnabled = false;
                //string NameoftheLanguage = SearchValue;
                books = db.Books.Where(b => b.BookName.Contains(SearchValue) || SearchValue == null).ToList();
                return Json(books, JsonRequestBehavior.AllowGet);
            }
        }
    }
}