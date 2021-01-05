﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApiProject.Services
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<DbContext> options )
            :base(options)
        {
            Database.Migrate();
        }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author>  Authors { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Reviewer> Reviewers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BookAuthor> BookAuthors { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }

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
