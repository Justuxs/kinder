using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using kinder_app.Models;

namespace kinder_app.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<kinder_app.Models.Item> Item { get; set; }
        public DbSet<kinder_app.Models.FurnitureItem> FurnitureItem { get; set; }
        public DbSet<kinder_app.Models.ElectronicsItem> ElectronicsItem { get; set; }
        public DbSet<kinder_app.Models.ClothingItem> ClothingItem { get; set; }

        //public DbSet<kinder_app.Models.Photo> Photo { get; set; }

        public DbSet<kinder_app.Models.LikedItems> LikedItems { get; set; }

        public DbSet<kinder_app.Models.ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Name)
                .HasMaxLength(250);

            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Surname)
                .HasMaxLength(250);
            modelBuilder.Entity<ApplicationUser>()
                .Property(e => e.Karma_points);

        }
    }
}