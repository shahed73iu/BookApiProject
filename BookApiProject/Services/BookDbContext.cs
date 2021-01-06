using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApiProject.Services
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options )
            :base(options)
        {
            Database.Migrate();
        }
        public virtual DbSet<Book> emsBooks { get; set; }
        public virtual DbSet<Author> emsAuthors { get; set; }
        public virtual DbSet<Review> emsReviews { get; set; }
        public virtual DbSet<Reviewer> emsReviewers { get; set; }
        public virtual DbSet<Country> emsCountries { get; set; }
        public virtual DbSet<Category> emsCategories { get; set; }
        public virtual DbSet<BookAuthor> emsBookAuthors { get; set; }
        public virtual DbSet<BookCategory> emsBookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>()
                        .HasKey(bc=> new { bc.BookId , bc.CategoryId});
            modelBuilder.Entity<BookCategory>()
                        .HasOne(b => b.Book)
                        .WithMany(c => c.BookCategories)
                        .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookCategory>()
                        .HasOne(c => c.Category)
                        .WithMany(c => c.BookCategories)
                        .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<BookAuthor>()
                        .HasKey(ba=> new {ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                        .HasOne(b => b.Book)
                        .WithMany(ba => ba.BookAuthors)
                        .HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookAuthor>() 
                        .HasOne(a => a.Author)
                        .WithMany(ba => ba.BookAuthors)
                        .HasForeignKey(a => a.AuthorId);
        }
    }
    
}
