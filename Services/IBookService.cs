using Library.Models;
using Microsoft.AspNetCore.Identity;
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
        //Task EditBook(int id, Book book);
        void EditBook(int id, Book book);
        Book GetBookById(int id);

        IEnumerable<Reservation> GetReservationById(int id);
        IEnumerable<Book> GetBookByID(int id);
        Task<IEnumerable<IdentityUser>> GetUserByBookId(int id);
    }
}
