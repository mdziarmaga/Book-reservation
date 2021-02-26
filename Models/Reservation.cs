using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Reservation
    {
        [Key]
        public int IdReservation { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime ReservationDate { get; set; }
        [ForeignKey("IdentityUser")]

        public string IdUser { get; set; }
        [ForeignKey("Book")]
        public int IdBook { get; set; }
    }
}
