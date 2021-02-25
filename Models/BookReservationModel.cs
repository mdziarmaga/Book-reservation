using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookReservationModel
    {
        public IEnumerable<Book> Book { get; set; }
      //  public Task<IEnumerable<IdentityUser>> User { get; set; }
        public IEnumerable<IdentityUser> User { get; set; }
        public IEnumerable<Reservation> Reservation { get; set; }
    }
}
