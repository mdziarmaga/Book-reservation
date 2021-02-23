using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        [Key]
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy }")]
        public DateTime ReleaseDate  { get; set; }
        public string Description { get; set; }
    }
}
