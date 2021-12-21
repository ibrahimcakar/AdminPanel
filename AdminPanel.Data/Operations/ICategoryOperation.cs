using AdminPanel.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public interface ICategoryOperation
    {
        List<Category> GetAllCategories();
        List<Category> GetCategoriesByParentCategoryId();
        List<Category> GetCategoriesByChildCategoryId();
        Category GetCategoryById(int id);
        Category CreateCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(int id);
    }
}
