using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStoreProject.Models;

namespace BookStoreProject.Models
{
    public class ViewCartDetails
    {
        public  Book Books { get; set; }
        public AddtoCart AddtoCarts { get; set; }
    }
}