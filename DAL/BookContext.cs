using AzBook.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzBook.DAL
{
    public class BookContext: DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasData(
                    new Book
                    {
                        Title = "Book 1",
                        Author = "Author 1",
                        Description = "Description 1",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "Seeder",
                    },
                    new Book
                    {
                        Title = "Book 2",
                        Author = "Author 2",
                        Description = "Description 2",
                        CreatedAt = DateTime.Now,
                        CreatedBy = "Seeder"
                    }
                );

            });

        }
    } 
}
