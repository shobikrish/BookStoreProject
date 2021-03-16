using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStoreProject.Models;

namespace BookStoreProject.Controllers
{
    public class AddBooksController : Controller
    {
        // GET: AddBooks
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Book book)
        {
            try
            {
                string filename = Path.GetFileNameWithoutExtension(book.ImageFile.FileName);
                string extension = Path.GetExtension(book.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                book.BookImage = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                book.ImageFile.SaveAs(filename);
                using (BookStoreEntities2 db = new BookStoreEntities2())
                {
                    db.Books.Add(book);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = "Books added Successfully";
            }
            catch(Exception e)
            {
                ViewBag.err = "Error" + e;
            }
                return View();
        }

        //public ActionResult Join()
        //{
        //    using (BookStoreEntities2 db=new BookStoreEntities2())
        //    {
        //        List<Book> books = db.Books.ToList();
        //        List<AddtoCart> carts = db.AddtoCarts.ToList();
        //        var tables = from b in books
        //                     join c in carts on b.BookId equals c.BookId into table1
        //                     from c in table1.ToList()
        //                     select new ViewCartDetails
        //                     {
        //                         Books = b,
        //                         AddtoCarts = c
        //                     };
        //        return View(tables);
        //    }
        //}
    }
}