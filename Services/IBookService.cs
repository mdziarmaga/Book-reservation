using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IBookService
    {
        IQueryable<Book> GetBooks();
        void Booking(Reservation reservation);
        Task<Book> AddBook(Book book);
        Task<Book> EditBook(Book book);
    }
}
