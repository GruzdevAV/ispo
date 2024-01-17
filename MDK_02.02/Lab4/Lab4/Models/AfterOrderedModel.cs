using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4.Models
{
    public class AfterOrderedModel
    {
        public AfterOrderedModel(Cart cart, Order order)
        {
            Cart = cart;
            Order = order;
        }

        public Order Order { get; set; }
        public Cart Cart { get; set; }
    }
}