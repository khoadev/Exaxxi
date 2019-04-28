using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Models
{
    public class ExaxxiDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<ds_Size> ds_Size { get; set; }
        public DbSet<Followings> Followings { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Sizes> Sizes { get; set; }

        //Link DS - Escanor: DESKTOP-9LUKN91\\SQLEXPRESS
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-9LUKN91\SQLEXPRESS;Initial Catalog=Exaxxi;Integrated Security=True");
        }
        public ExaxxiDbContext(DbContextOptions<ExaxxiDbContext> options) : base(options)
        {
        }

    }
}
