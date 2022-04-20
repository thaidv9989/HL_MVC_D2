

using ManageOnline.Models;
using System.Collections.Generic;

namespace ManageOnline.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
        void AddCategory(Category Category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
