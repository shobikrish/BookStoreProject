using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStoreProject.Models
{
    public class CartDetails
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public Nullable<double> BookPrice { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}