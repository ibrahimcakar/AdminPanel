using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Data.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public bool IsActive { get; set; }
    }
}
