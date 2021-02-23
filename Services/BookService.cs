using Library.Data;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly DBContext dbContext;

        public BookService(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Book> GetBooks()
        {
           return dbContext.Books;
        }
    }
}
