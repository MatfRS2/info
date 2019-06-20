using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MasterTeme.Models{

    public class Student{

        [BindNever]
        public int StudentId { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite prezime")]
        public string Prezime { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite indeks")]
        public string Indeks { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite smer")]
        public string Smer { get; set; }
    }
}