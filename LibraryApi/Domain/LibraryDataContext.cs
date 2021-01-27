using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Domain
{
    public class LibraryDataContext : DbContext
    {
        public LibraryDataContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        public IQueryable<Book> GetBooksInInventory()
        {
            return Books.Where(b => b.IsInInventory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                    .Property(b => b.Author)
                    .IsRequired()
                    .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .Property(b => b.Genre)
                .HasMaxLength(100);
        }
    }
}
