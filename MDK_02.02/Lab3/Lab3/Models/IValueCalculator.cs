using System.Collections.Generic;

namespace Lab3.Models
{
    public interface IValueCalculator
    {
        decimal ValueProducts(IEnumerable<Product> products);

    }

}
