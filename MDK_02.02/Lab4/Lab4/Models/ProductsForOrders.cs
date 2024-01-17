using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class ProductsForOrders
    {
        [Key]
        public int JustSomeId { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public ProductsForOrders (CartLine line, int orderID)
        {
            OrderID = orderID;
            ProductID = line.Product.ProductID;
            Quantity = line.Quantity;
        }
    }
}