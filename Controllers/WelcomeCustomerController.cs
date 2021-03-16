using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStoreProject.Models;

namespace BookStoreProject.Controllers
{
    public class WelcomeCustomerController : Controller
    {
        BookStoreEntities2 db = new BookStoreEntities2();
        // GET: WelcomeCustomer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Home()
        {
            string CustomerName = TempData.Peek("CustomerName").ToString();
            string CustomerEmail = TempData.Peek("CustomerEmail").ToString();
            ViewBag.Cname = CustomerName;
            ViewBag.Cemail = CustomerEmail;
            //Book book = new Book();
            //using(BookStoreEntities db=new BookStoreEntities())
            //{
            //    book = db.Books.Where(x => x.BookId == id).FirstOrDefault();
            //}
            return View(db.Books.ToList());
        }
        public ActionResult Add_To_Cart(int BookId)
        {
            try
            {
                Book book = db.Books.FirstOrDefault(b => b.BookId == BookId);
                CartDetails cart = new CartDetails();
                if (book != null)
                {
                    cart.BookId = book.BookId;
                    cart.BookName = book.BookName;
                    cart.BookAuthor = book.BookAuthor;
                    cart.BookPrice = book.BookPrice;
                    return View(cart);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error" + ex.Message;
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult Add_To_Cart(CartDetails cartDetails)
        {
           
                AddtoCart addcart = new AddtoCart();
                addcart.BookId = cartDetails.BookId;
                addcart.Quantity = cartDetails.Quantity;
                addcart.CustomerEmail = TempData.Peek("CustomerEmail").ToString();
                db.AddtoCarts.Add(addcart);
                db.SaveChanges();
                ViewBag.Message = "Added Successfully";
                return View();        
        }
        //public ActionResult View_Cart_Details(string CustomerEmail)
        //{
        //    //var cartDetails = db.AddtoCarts.Where(b => b.CustomerEmail == CustomerEmail);
        //    var Book_Id = from data in db.AddtoCarts
        //                  where data.CustomerEmail== CustomerEmail
        //                  select data.BookId;
        //    //int Id_Book = Convert.ToInt32(Book_Id);
        //    int Id_Book = 1;
        //    CustomerEmail = "shobi@gmail.com";
        //    var tables = new ViewCartDetails
        //    {
        //        Books = db.Books.Where(b => b.BookId == Id_Book),
        //        AddtoCarts=db.AddtoCarts.Where(b => b.CustomerEmail == CustomerEmail)
        //    //Departments = db.Departments.ToList(),
        //    //Incentives = db.Incentives.ToList()
        //};
        //    return View(tables);
        //    //var cartDetails = db.AddtoCarts.Where(b => b.CustomerEmail ==CustomerEmail);
        //    //return View(cartDetails);
        //}
        [Route("Cart Details")]
        public ActionResult View_Cart()
        {
            using (BookStoreEntities2 db = new BookStoreEntities2())
            {
                List<Book> books = db.Books.ToList();
                List<AddtoCart> carts = db.AddtoCarts.ToList();
                var tables = from b in books
                             join c in carts on b.BookId equals c.BookId into table1
                             from c in table1.ToList()
                             select new ViewCartDetails
                             {
                                 Books = b,
                                 AddtoCarts = c
                             };
                return View(tables);
            }
        }
        public ActionResult OrderDetails(int BookPrice)
        {
            try
            {
                string CustomerMailid = TempData.Peek("CustomerEmail").ToString();
                Customer customer = db.Customers.FirstOrDefault(b => b.CustomerEmail == CustomerMailid);
                //AddtoCart cart = db.AddtoCarts.FirstOrDefault(c => c.CustomerEmail == CustomerMailid);
                OrderDetail order = new OrderDetail();
                ViewBag.total = BookPrice;
                if (customer != null)
                {
                    //order.BookId = cart.BookId;
                    order.CustomerName = customer.CustomerName;
                    order.CustomerPhoneno = customer.CustomerPhoneno;
                    order.CustomerAddress = customer.CustomerAddress;
                    order.CustomerPincode = customer.CustomerPincode;
                    //order.Price = Convert.ToDouble(BookPrice);
                }
                return View(order);
            }
            catch(Exception e)
            {
                ViewBag.Message = "Error" + e.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult OrderDetails(OrderDetail order, int BookPrice,int BookId)
        {
            string CustomerMailid = TempData.Peek("CustomerEmail").ToString();
            //AddtoCart cart = db.AddtoCarts.FirstOrDefault(c => c.CustomerEmail == CustomerMailid);
            //var details= db.AddtoCarts.Where(c => c.CustomerEmail == CustomerMailid).ToList();
            //string IdofBook;
            //foreach(var item in details)
            //{
            //    IdofBook.Append(item.BookId);
            //}
            order.CustomerEmail= CustomerMailid;
            order.BookId = BookId;
            order.Price= Convert.ToDouble(BookPrice);
            db.OrderDetails.Add(order);
            db.SaveChanges();
            ViewBag.Message = "Order Placed Successfully";
            return View();
        }
        [Route("Order Details")]
        public ActionResult ViewOrderDetails()
        {
            string CustomerEmail= TempData.Peek("CustomerEmail").ToString();
            var data = db.OrderDetails.Where(d => d.CustomerEmail == CustomerEmail);
            return View(data);
        }
        public ActionResult Delete_From_Cart(int BookId)
        {
            if (BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddtoCart cart = db.AddtoCarts.Find(BookId);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }
        [HttpPost, ActionName("Delete_From_Cart")]
        public ActionResult Delete_From_Cart_Confirmed(int BookId)
        {
            AddtoCart cart = db.AddtoCarts.Find(BookId);
            db.AddtoCarts.Remove(cart);
            db.SaveChanges();
            return RedirectToAction("View_Cart");
        }
        public ActionResult AddFeedBack()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFeedBack(FeedBack feedBack, int BookId)
        {
            feedBack.BookId = BookId;
            db.FeedBacks.Add(feedBack);
            db.SaveChanges();
            return View();
        }
        public ActionResult FeedBack(int BookId)
        {
            var feedback = db.FeedBacks.Where(b => b.BookId== BookId);
            return View(feedback);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("HomePage", "HomePage");
        }
    }
}