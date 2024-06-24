using Microsoft.EntityFrameworkCore;
using ReatailStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReatailStore.Domain.StoreDbContext
{
    public class StoreContext : DbContext
    {
        public DbSet<Markup> Markups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ReportItem> ReportItems { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public StoreContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Role adminRole = new Role { Id = 1, Name = "admin" };
            //Role salesmanRole = new Role { Id = 2, Name = "salesman" };
            //User adminUser = new User { Id = 1, Name = "Администратор", Email = "admin@box.com", Password = "admin", RoleId = adminRole.Id };
            //User User = new User { Id = 2, Name = "Продовец", Email = "user@box.com", Password = "user", RoleId = salesmanRole.Id };

            //modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, salesmanRole });
            //modelBuilder.Entity<User>().HasData(new User[] { adminUser, User });
            base.OnModelCreating(modelBuilder);
        }
    }
}
