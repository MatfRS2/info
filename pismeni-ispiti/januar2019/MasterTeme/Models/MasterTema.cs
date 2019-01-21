using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MasterTeme.Models{

    public class MasterTema
    {
        public int MasterTemaId { get; set; }

        public string Naziv { get; set; }

        public Student Student { get; set; }

        public Nastavnik Mentor { get; set; }

        public IEnumerable<KomisijaElement> Komisija { get; set; }

        public DateTime DatumNNV { get; set; }
    }

    public class KomisijaElement
    {
        public int KomisijaElementId { get; set; }
        public Nastavnik Nastavnik { get; set; }
    }
}