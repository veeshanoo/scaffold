using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scaffold.Models
{
    public class Recenzie
    {
        [Key]
        public int IDRecenzie { get; set; }
        [Required]
        public string Titlu { get; set; }
        [Range(1, 5)]
        [Required]
        public int Nota { get; set; }
        [Required]
        public int IDFilm { get; set; }

        [NotMapped]
        public Film FilmData { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> FilmList { get; set; }

    }
}