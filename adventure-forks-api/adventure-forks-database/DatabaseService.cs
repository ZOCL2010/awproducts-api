using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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

        public List<ProductDto> GetAllProducts()
        {
            try
            {
                var products = _entities.Product.ToList();
                return Mapper.Map<List<Product>, List<ProductDto>>(products);
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to retrieve all products.");
            }

            return new List<ProductDto>();
        }

        public ProductDto GetProduct(int productId)
        {
            try
            {
                var product = _entities.Product.FirstOrDefault(x => x.ProductID == productId);
                return Mapper.Map<Product, ProductDto>(product);
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to retrieve a product.");
            }

            return null;
        }

        public ProductDto CreateProduct(
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
                    SafetyStockLevel = 32,
                    ReorderPoint = 1,
                    StandardCost = standardCost,
                    ListPrice = listPrice,
                    Size = size,
                    SizeUnitMeasureCode = "cm",
                    WeightUnitMeasureCode = "kg",
                    Weight = weight,
                    DaysToManufacture = 3,
                    ProductLine = "M",
                    Class = "H",
                    Style = "W",
                    ProductSubcategoryID = 1,
                    ProductModelID = 1,
                    SellStartDate = DateTime.Today,
                    SellEndDate = new DateTime(2018, 12, 3),
                    DiscontinuedDate = new DateTime(2018, 4, 3),
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                };

                _entities.Product.Add(newProduct);
                _entities.SaveChanges();

                return Mapper.Map<Product, ProductDto>(newProduct);
            }
            catch (Exception e)
            {
                Log.Error(e, "An error occurred while trying to create a product.");
            }

            return null;
        }

        public bool UpdateProduct(ProductDto productDto)
        {
            try
            {
                var product = _entities.Product.FirstOrDefault(x => x.ProductID == productDto.ProductId);
                if (product != null)
                {
                    product.Name = productDto.Name;
                    product.ProductNumber = productDto.ProductNumber;
                    product.StandardCost = productDto.StandardCost;
                    product.ListPrice = productDto.ListPrice;
                    product.Size = productDto.Size;
                    product.Weight = productDto.Weight;
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