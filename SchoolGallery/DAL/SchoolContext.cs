using Microsoft.EntityFrameworkCore;
using SchoolGallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolGallery.DAL
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options):base(options)
        {

        }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<ContentModel> Content { get; set; }
        public DbSet<VideoModel> Video { get; set; }
        public DbSet<AccountModel> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().HasAlternateKey(i => i.AccountID);

        }
    }
}
