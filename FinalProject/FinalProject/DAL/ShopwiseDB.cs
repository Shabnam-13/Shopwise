using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalProject.DAL
{
    public class ShopwiseDB:DbContext
    {
        public ShopwiseDB() : base("ShopwiseDBCon")
        {

        }

        public DbSet<Setting> Setting { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogTags> BlogTags { get; set; }
        public DbSet<BlogImages> BlogImages { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<ProCategory> ProCategory { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<SaleBanner> SaleBanner { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<SizeOption> SizeOption { get; set; }
        public DbSet<SocialNet> SocialNet { get; set; }
        public DbSet<Subscribe> Subscribe { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<SubOfSubcategory> SubOfSubcategory { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Term> Term { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasOptional(b => b.ParentComment)
                                          .WithMany(b => b.Children)
                                          .HasForeignKey(b => b.ParentCommentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}