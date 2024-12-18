using DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class BookDbContext : IdentityDbContext<User, Role, int>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItems> BasketItems { get; set; }
    }
}
