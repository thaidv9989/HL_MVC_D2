using ManageOnline.Models;
using ManageOnline.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_D1.Service.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly MProductEntities _dbContext;

        public CategoryService(MProductEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCategory(Category Category)
        {
            _dbContext.Categories.Add(Category);
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var rs = _dbContext.Categories.FirstOrDefault(x =>x.Category_Id == id);
            if (rs != null)
            {
                _dbContext.Categories.Remove(rs);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _dbContext.Categories.FirstOrDefault(x => x.Category_Id == id);
        }

        public void UpdateCategory(Category category)
        {
            var rs = _dbContext.Categories.FirstOrDefault(c => c.Category_Id == category.Category_Id);
            if (rs == null)
            {
                throw new Exception("Not found this product with id: " + category.Category_Id);
            }
            rs.Category_Id = category.Category_Id;
            rs.Category_Name = category.Category_Name;
            _dbContext.Entry(rs);
            _dbContext.SaveChanges();
        }
    }
}
