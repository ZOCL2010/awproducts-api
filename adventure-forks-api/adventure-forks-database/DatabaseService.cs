using System.Linq;

namespace adventure_forks_database
{
    public class DatabaseService
    {
        private readonly AdventureWorks2017Entities _entities;

        public DatabaseService()
        {
            _entities = new AdventureWorks2017Entities();
        }

        public Product GetProduct(int productId)
        {
            var product = _entities.Product.FirstOrDefault(x => x.ProductID == productId);
            return product;
        }

        public int CreateProduct()
        {
            var newProduct = new Product();
            _entities.Product.Add(newProduct);
            _entities.SaveChanges();

            return newProduct.ProductID;
        }

        public void UpdateProduct(int productId, string name)
        {
            var product = _entities.Product.FirstOrDefault(x => x.ProductID == productId);
            if (product != null)
            {
                product.Name = name;
            }

            _entities.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = _entities.Product.FirstOrDefault(x => x.ProductID == productId);
            if (product != null)
            {
                _entities.Product.Remove(product);
                _entities.SaveChanges();
            }
        }
    }
}