using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MasterTeme.Models;

namespace MasterTeme.ViewModels{

    public class MasterTemaViewModel
    {

        [Required(ErrorMessage="Molimo unesite naziv teme")]
        public string Naziv { get; set; }

        [Required(ErrorMessage="Molimo unesite indeks")]
        public string Indeks { get; set; }

        [Required(ErrorMessage="Molimo odaberite nastavnika")]
        public int NastavnikId { get; set; }

        public List<int> KomisijaId { get; set; }

        [Required(ErrorMessage="Molimo Vas unesite datum")]
        public DateTime DatumNNV { get; set; }

        public IEnumerable<Nastavnik> Nastavnici { get; set; }
    }

}