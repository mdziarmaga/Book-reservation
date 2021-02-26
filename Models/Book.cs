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
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy }")]
        [Display(Name = "Data wydania")]
        public DateTime ReleaseDate  { get; set; }
        [Display(Name = "Opis")]
        public string Description { get; set; }
    }
}
