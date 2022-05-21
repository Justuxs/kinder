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

        public DbSet<kinder_app.Models.Message> Messages { get; set; }

        public DbSet<kinder_app.Models.ChatHub> ChatHubs { get; set; }

        public const int DaysPendingCHat = 7;


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
            
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.ID).ValueGeneratedOnAdd();
                entity.Property(e => e.ChatHub).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ChatHub>(entity =>
            {
                entity.HasKey(e => e.Name);
                entity.Property(e => e.SenderID).ValueGeneratedOnAdd();
                entity.Property(e => e.ReceiverID).ValueGeneratedOnAdd();
                entity.Property(e => e.Date).ValueGeneratedOnAdd();
                entity.Property(e => e.Status).ValueGeneratedOnAdd();
                entity.Property(e => e.Approved1).HasDefaultValue(false);
                entity.Property(e => e.Approved2).HasDefaultValue(false);

            });
        }
        
        public DbSet<kinder_app.Models.Message> Message { get; set; }
    }
}