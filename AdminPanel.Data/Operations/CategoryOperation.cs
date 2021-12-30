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
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Categories.Add(category);
                AdminPanelDbContext.SaveChanges();
                return category;
            }
        }

        public void DeleteCategory(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                var deletedCategory = GetCategoryById(id);
                AdminPanelDbContext.Categories.Remove(deletedCategory);
                AdminPanelDbContext.SaveChanges();
            }
        }

        public List<Category> GetAllCategories()
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Categories
                       .Include(b => b.ParentCategory)
                       .ToList();
            }
        }

        public Category GetCategoryById(int id)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Categories.Find(id);
            }
        }

        public Category UpdateCategory(Category category)
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                AdminPanelDbContext.Categories.Update(category);
                return category;
            }
        }

        public List<Category> GetCategoriesByParentCategoryId()
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Categories.Include(b => b.ParentCategory).ToList();
            }
        }

        public List<Category> GetCategoriesByChildCategoryId()
        {
            using (var AdminPanelDbContext = new AdminPanelDb())
            {
                return AdminPanelDbContext.Categories.Where(x => x.ParentCategoryId != null).ToList();
            }
        }
    }
}
