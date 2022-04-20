using ManageOnline.Models;
using ManageOnline.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_D1.Service.Implement
{
    public class ProductService : IProductService
    {
        private readonly MProductEntities _dbContext;

        public ProductService(MProductEntities dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var rs = _dbContext.Products.FirstOrDefault(x => x.Product_Id == id);
            if(rs != null)
            {
                _dbContext.Products.Remove(rs);
                _dbContext.SaveChanges();
            }

        }

        public Product GetProduct(int id)
        {
            return _dbContext.Products.FirstOrDefault(x => x.Product_Id == id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public void UpdateProduct(Product product)
        {
            var rs = _dbContext.Products.FirstOrDefault(x => x.Product_Id == product.Product_Id);
            if (rs == null)
            {
                throw new Exception("Not found product");
            }
            rs.Product_Name = product.Product_Name;
            rs.Product_Img = product.Product_Img;
            rs.Description = product.Description;
            rs.Category_Id = product.Category_Id;
            rs.Price = product.Price;
            rs.UpdatedDate = DateTime.Now;
            _dbContext.Entry(rs);
            _dbContext.SaveChanges();
        }

        
    }
}
