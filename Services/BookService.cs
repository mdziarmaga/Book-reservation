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

        public Book AddBook(Book book)
        {
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
            return book; 
        }

        public void Booking(Reservation reservation)
        {
            var book = dbContext.Reservations.FirstOrDefault(book => book.IdBook == reservation.IdBook);

            if (book == null)
            {
                dbContext.Reservations.Add(reservation);
                dbContext.SaveChanges();
            }
        }

        public IQueryable<Book> GetBooks()
        {
           return dbContext.Books;
        }
    }
}
