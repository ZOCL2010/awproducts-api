using System;
using System.Collections.Generic;

namespace adventure_forks_database
{
    public interface IDatabaseService : IDisposable
    {
        Product GetProduct(int productId);

        List<Product> GetAllProducts();

        Product CreateProduct(string name, string productNumber, decimal standardCost, decimal listPrice, string size, decimal weight);

        bool UpdateProduct(
            int productId,
            string name,
            string productNumber,
            decimal standardCost,
            decimal listPrice,
            string size,
            decimal weight);

        bool DeleteProduct(int productId);
    }
}