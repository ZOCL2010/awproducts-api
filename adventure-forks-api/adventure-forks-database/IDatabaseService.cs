using System;
using System.Collections.Generic;

namespace adventure_forks_database
{
    public interface IDatabaseService : IDisposable
    {
        ProductDto GetProduct(int productId);

        List<ProductDto> GetAllProducts();

        ProductDto CreateProduct(string name, string productNumber, decimal standardCost, decimal listPrice, string size, decimal weight);

        bool UpdateProduct(ProductDto product);

        bool DeleteProduct(int productId);
    }
}