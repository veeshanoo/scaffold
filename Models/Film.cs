using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scaffold.Models
{
    public class Film
    {
        [Key]
        public int IDFilm { get; set; }
        [Required]
        public string Denumire { get; set; }
        public ICollection<Recenzie> Recenzii { get; set; }
    }
}