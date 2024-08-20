using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilkNest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Persistence
{
    public class MilkNestDbContext:IdentityDbContext<ApplicationUser>
    {
       
        public MilkNestDbContext(DbContextOptions options) : base(options)
        {
           
        }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<JobVacancy> JobVacancies { get; set; }
        public virtual DbSet<NewsLocalization> NewsLocalizations { get; set; }
        public virtual DbSet<JobVacancyLocalization> JobVacancyLocalizations { get; set; }
        public virtual DbSet<ProductLocalization> ProductLocalizations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasMany(u => u.Comments)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
         .HasOne(u => u.Image)
         .WithMany(c => c.Users).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>().HasMany(c => c.Replies)
                 .WithOne(p=>p.ParentComment)
                 .HasForeignKey(c => c.ParentCommentId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Comment>().HasOne(c => c.ParentComment)
                .WithMany()
                .HasForeignKey(c => c.ParentCommentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2); 

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId);
            

             modelBuilder.Entity<User>()
                .HasOne(u => u.Image)
                .WithMany(i => i.Users)
                .HasForeignKey(u => u.ImageId);

            modelBuilder.Entity<NewsLocalization>()
          .HasOne(nl => nl.News)
          .WithMany(n => n.Localizations)
          .HasForeignKey(nl => nl.NewsId)
          .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobVacancyLocalization>()
         .HasOne(jvl => jvl.JobVacancy)
         .WithMany(jv => jv.Localizations)
         .HasForeignKey(jvl => jvl.JobVacancyId)
         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductLocalization>()
           .HasOne(pl => pl.Product)
           .WithMany(p => p.Localizations)
           .HasForeignKey(pl => pl.ProductId)
           .OnDelete(DeleteBehavior.Cascade);

        }
    }
  
}
