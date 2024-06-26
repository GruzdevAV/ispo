﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab4.Models
{
    public class Cart
    {
        private List<CartLine> _lineCollection = new List<CartLine>();
        public void AddItem(Product product, int quantity)
        {
            CartLine line = _lineCollection
            .Where(p => p.Product.ProductID == product.ProductID)
            .FirstOrDefault();
            if (line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }
        public void RemoveAllLines()
        {
            _lineCollection.RemoveAll(x=>true);
        }
        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }
        public void Clear()
        {
            _lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return _lineCollection; }
        }
    }
}