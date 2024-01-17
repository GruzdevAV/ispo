using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalParam);
    }
    public class DefaultDiscountHelper : IDiscountHelper
    {
        //public decimal DiscountSize { get; set; }
        private decimal _discountSize;

        public DefaultDiscountHelper(decimal discountParam)
        {
            _discountSize = discountParam;
        }

        public decimal ApplyDiscount(decimal totalParam)
        {
            return (totalParam - (_discountSize / 100m * totalParam));
        }
    }
}