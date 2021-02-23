using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data
{
    public class DBContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }

        public DBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
