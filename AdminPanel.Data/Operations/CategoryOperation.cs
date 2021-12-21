using AdminPanel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPanel.Data.Operations
{
    public class CategoryOperation
    {
        public Category CreateCategory(Category category)
        {
            using (var categoryDbContext = new AdminPanelDb())
            {

                categoryDbContext.Categories.Add(category);
                categoryDbContext.SaveChanges();
                return category;
            }
        }

        public void DeleteCategory(int id)
        {
            using (var categoryDbContext = new AdminPanelDb())
            {

                var deletedCategory = GetCategoryById(id);
                categoryDbContext.Categories.Remove(deletedCategory);
                categoryDbContext.SaveChanges();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var categoryDbContext = new AdminPanelDb())
            {
                return categoryDbContext.Categories
                       .Include(b => b.ParentCategory)
                       .ToList();
            }
        }

        public Category GetCategoryById(int id)
        {
            using (var categoryDbContext = new AdminPanelDb())
            {
                return categoryDbContext.Categories.Find(id);
            }
        }

        public Category UpdateCategory(Category category)
        {
            using (var categoryDbContext = new AdminPanelDb())
            {
                categoryDbContext.Categories.Update(category);
                return category;
            }
        }

        public List<Category> GetCategoriesByParentCategoryId()
        {
            using (var categoryDbContext = new AdminPanelDb())
            {
                return categoryDbContext.Categories.Include(b => b.ParentCategory).ToList();
            }
        }

        public List<Category> GetCategoriesByChildCategoryId()
        {
            using (var categoryDbContext = new AdminPanelDb())
            {
                return categoryDbContext.Categories.Where(x => x.ParentCategoryId != null).ToList();
            }
        }
    }
}
