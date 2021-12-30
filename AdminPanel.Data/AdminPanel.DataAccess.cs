using AdminPanel.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;

namespace AdminPanel.Data
{
    public class AdminPanelDb: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=18.158.1.38,1433;Database=AdminPanel;User Id=sa;Password=Ckrent123;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cars> Cars { get; set; }

    }
}
