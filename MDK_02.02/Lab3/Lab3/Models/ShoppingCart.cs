using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class ShoppingCart
    {
        private IValueCalculator _calc;
        public ShoppingCart(IValueCalculator calcParam)
        {
            _calc = calcParam;
        }
        public IEnumerable<Product> Products { get; set; }
        public decimal CalculateProductsTotal()
        {
            return _calc.ValueProducts(Products);
        }
    }
}