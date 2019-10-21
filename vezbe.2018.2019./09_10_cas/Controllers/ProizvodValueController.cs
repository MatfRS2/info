using Microsoft.AspNetCore.Mvc;
using ProdavnicaTehnike.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ProdavnicaTehnike.Controllers{

    [Route("api/proizvodi")]
    public class ProizvodValueController : Controller{
        private DataContext repozitorijum;

        public ProizvodValueController(DataContext repo)
        {
            repozitorijum = repo;
        }

        [HttpGet("{id}")]
        public Proizvod GetProizvod(int id)
        {
            Proizvod proiz =  repozitorijum.Proizvodi
                                    .Include(p => p.Proizvodjac).ThenInclude(p => p.Proizvodi)
                                    .Include(p => p.Ocene)
                                    .FirstOrDefault(p => p.ProizvodID == id);

            if (proiz != null)
            {
                if (proiz.Proizvodjac != null)
                    proiz.Proizvodjac.Proizvodi = proiz.Proizvodjac.Proizvodi.Select(
                        p => new Proizvod {
                            ProizvodID = p.ProizvodID,
                            Ime = p.Ime,
                            Kategorija = p.Kategorija,
                            Opis = p.Opis,
                            Cena = p.Cena
                        });

                if (proiz.Ocene != null)
                {
                    foreach (Ocena o in proiz.Ocene)
                        o.Proizvod = null;
                }
            }

            return proiz;
        }


        [HttpGet]
        public IEnumerable<Proizvod> GetProizvodi(bool related = false, string kategorija = null)
        {
            IQueryable<Proizvod> svi_proizvodi = repozitorijum.Proizvodi;
            
            if (kategorija != null)
                svi_proizvodi = svi_proizvodi.Where(p => p.Kategorija == kategorija);


            if (related)
            {
                svi_proizvodi = svi_proizvodi.Include(p => p.Proizvodjac)
                                             .Include(p => p.Ocene);

                List<Proizvod> podaci = svi_proizvodi.ToList();

                podaci.ForEach(p => 
                {
                    if (p.Proizvodjac != null)
                        p.Proizvodjac.Proizvodi = null;
                    if (p.Ocene != null)
                        p.Ocene.ForEach(o => o.Proizvod = null);
                });

                return podaci;
            }
            else
                return svi_proizvodi;
        }

        
        [HttpGet("kategorije")]
        public IEnumerable<string> GetKategorije()
        {

            var kategorije = repozitorijum.Proizvodi.Select(p => p.Kategorija)
                                                   .Distinct().OrderBy(p => p);

            return kategorije;
        }


        [HttpPost]
        public IActionResult CreateProduct([FromBody] Proizvod pdata) {
            if (ModelState.IsValid) 
            {
                if (pdata.Proizvodjac != null) 
                {
                    Proizvodjac proizvodjac = repozitorijum.Proizvodjaci
                               .Where(p => p.ProizvodjacID == pdata.Proizvodjac.ProizvodjacID).FirstOrDefault();
                    repozitorijum.Attach(proizvodjac);
                }

                repozitorijum.Add(pdata);
                repozitorijum.SaveChanges();

                return Ok(pdata.ProizvodID);
            } 
            else 
            {
                return BadRequest(ModelState);
            }
        }        

    }

}



