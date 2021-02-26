using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly DBContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public BookService(DBContext dbContext,
                            UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Book> AddBook(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book; 
        }

        public void Booking(Reservation reservation)
        {
           // var book = dbContext.Reservations.FirstOrDefault(book => book.IdBook == reservation.IdBook);
            var book = dbContext.Reservations.FirstOrDefault(item => item.IdBook == reservation.IdBook && item.IdUser == reservation.IdUser);

            if (book == null)
            {
                dbContext.Reservations.Add(reservation);
                dbContext.SaveChanges();
            }
        }

        public void EditBook(int id, Book book)
        {
            var bookToChange = dbContext.Books.FirstOrDefault(m => m.IdBook == id);
            bookToChange.Title = book.Title;
            bookToChange.Author = book.Author;
            bookToChange.ReleaseDate = book.ReleaseDate;
            bookToChange.Description = book.Description;
            dbContext.SaveChanges();
            //await dbContext.SaveChangesAsync();
        }

        public Book GetBookById(int id)
        {
            var book = dbContext.Books.FirstOrDefault(m => m.IdBook == id);
            return book;
        }

        public IEnumerable<Book> GetBookByID(int id)
        {
            var book = dbContext.Books.Where(m => m.IdBook == id);
            return book;
            //throw new NotImplementedException();
        }

        public IQueryable<Book> GetBooks()
        {
           return dbContext.Books;
        }

        public IEnumerable<Reservation> GetReservationById(int id)
        {
            var reservations = dbContext.Reservations.Where(m => m.IdBook == id);
            return reservations;
        }
        
        public async Task<IEnumerable<IdentityUser>> GetUserByBookId(int id)
        {
            List<IdentityUser> users = new List<IdentityUser>();

            var reservations = dbContext.Reservations.Where(m => m.IdBook == id);
            foreach (var item in reservations)
            {
                var userId = item.IdUser;
                var  user = await userManager.FindByIdAsync(userId);
                users.Add(user);
            }
            return users;
            //var user = userManager.GetUserId(Where(m => m.))
            // throw new NotImplementedException();
        }

        //public List<Reservation> GetReservations()
        //{
        //    return dbContext.Reservations.ToList();
        //}
    }
}
