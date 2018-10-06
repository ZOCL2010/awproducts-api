using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace adventure_forks_database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly AdventureWorks2017Entities _entities;

        public DatabaseService()
        {
            _entities = new AdventureWorks2017Entities();
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                return _entities.Product.ToList();
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to retrieve all products.");
            }

            return new List<Product>();
        }

        public Product GetProduct(int productId)
        {
            try
            {
                return _entities.Product.FirstOrDefault(x => x.ProductID == productId);
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to retrieve a product.");
            }

            return null;
        }

        public Product CreateProduct(
            string name, 
            string productNumber, 
            decimal standardCost, 
            decimal listPrice,
            string size, 
            decimal weight)
        {
            try
            {
                var newProductId = _entities.Product.Count();
                var newProduct = new Product
                {
                    ProductID = newProductId,
                    Name = name,
                    ProductNumber = productNumber,
                    MakeFlag = true,
                    FinishedGoodsFlag = true,
                    Color = "Blue",
                    SafetyStockLevel = 0,
                    ReorderPoint = 1,
                    StandardCost = standardCost,
                    ListPrice = listPrice,
                    Size = size,
                    SizeUnitMeasureCode = "cm",
                    WeightUnitMeasureCode = "kg",
                    Weight = weight,
                    DaysToManufacture = 3,
                    ProductLine = "my product line",
                    Class = "A",
                    Style = "New style",
                    ProductSubcategoryID = 1,
                    ProductModelID = 1,
                    SellStartDate = DateTime.Today,
                    SellEndDate = DateTime.MaxValue,
                    DiscontinuedDate = DateTime.MinValue,
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };

                _entities.Product.Add(newProduct);
                _entities.SaveChanges();

                return newProduct;
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to create a product.");
            }

            return null;
        }

        public bool UpdateProduct(
            int productId, 
            string name,
            string productNumber,
            decimal standardCost,
            decimal listPrice,
            string size,
            decimal weight)
        {
            try
            {
                var product = _entities.Product.FirstOrDefault(x => x.ProductID == productId);
                if (product != null)
                {
                    product.Name = name;
                    product.ProductNumber = productNumber;
                    product.StandardCost = standardCost;
                    product.ListPrice = listPrice;
                    product.Size = size;
                    product.Weight = weight;
                    product.ModifiedDate = DateTime.Now;
                    _entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to update a product.");
            }

            return false;
        }

        public bool DeleteProduct(int productId)
        {
            try
            {
                var product = _entities.Product.FirstOrDefault(x => x.ProductID == productId);
                if (product != null)
                {
                    _entities.Product.Remove(product);
                    _entities.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to delete a product.");
            }

            return false;
        }

        public void Dispose()
        {
            _entities?.Dispose();
        }
    }
}