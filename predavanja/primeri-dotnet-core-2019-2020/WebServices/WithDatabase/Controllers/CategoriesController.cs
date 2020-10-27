using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace DIinCore.Controllers
{
    /// <summary>
    /// Kontroler veb servisa koji vrace kategorije 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        /// <summary>
        /// Vraće kategorije.
        /// Metod radi na sinhroni način.
        /// </summary>
        /// <returns>
        /// OK kod ako je sve OK.
        /// </returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = _categoryRepository.GetCategories();
            return Ok(categories);
        }

        /// <summary>
        /// Daje detalje za prosledjeni identifikato kategorije.
        /// Metod radi na sinhroni način.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.ResetContent)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            Category kat = _categoryRepository.GetCategoryById(id.GetValueOrDefault());
            if (kat == null)
                return new StatusCodeResult((int)HttpStatusCode.ResetContent);
            return Ok(kat);
        }

        /// <summary>
        /// Ubacuje zadatu kategoriju među kategorije. 
        /// Metod radi na sinhroni način.
        /// </summary>
        /// <example>
        /// POST: api/categories
        /// </example>
        /// <param name="kat">Kategorija koja se ubacuje.</param>
        /// <returns>Elemenat za povratni poziv tipa <see cref="T:Category"/>
        /// , koji predstavlja novododati kategoriju.</returns>
        /// <response code="201">Putanja prema novododatoj kategoriji.</response>
        /// <response code="400">Pokušaj dodavanja kategorije nije uspeo.</response>
        /// <response code="500">Greška u procesiranju na serverskoj strani.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Category), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> PostAsync([FromBody]Category kat)
        {
            if (kat == null)
            {
                return BadRequest("Id od nepostojeće kategorije!");
            }
            Category t = new Category();
            t.CategoryName = kat.CategoryName;
            t.CategoryId = 0;
            _categoryRepository.InsertCategory(t);
            kat.CategoryId = t.CategoryId;
            return Created("api/[controller]/" + kat.CategoryId, kat);
        }

        /// <summary>
        /// Postavlja podatke za zadatu kategoriju. 
        /// Metod radi na sinhroni način.
        /// </summary>
        /// <example>
        /// PUT: api/categories/5
        /// </example>
        /// <param name="id">Identifikator kategorije koja se postavlja.
        /// Mora da se podudari sa poljem id kod parametra entitet.</param>
        /// <param name="entitet">Novi podaci.
        /// Vrednost polja id u okviru ovog objekta mora da se poklopi sa vrednošću parametra id.</param>
        /// <returns>Elemenat za povratni poziv.</returns>
        /// <response code="204">Postavljanje podataka je uspešno realizovano.</response>
        /// <response code="400">Pokušaj promene podataka za grad nije uspeo.</response>
        /// <response code="500">Greška u procesiranju na serverskoj strani.</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(Category), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(void), 500)]
        public async Task<IActionResult> Put(int id, [FromBody]Category entitet)
        {
            if (id != entitet.CategoryId)
            {
                return BadRequest("Pokušaj promene podataka za kategoriju koji ne postoji (pogrešan Id).");
            }
            Category ent = _categoryRepository.GetCategoryById(id);
            if (ent == null)
            {
                return BadRequest("Id od nepostojeće kategorije!");
            }
            _categoryRepository.UpdateCategory(id, entitet);
            return NoContent();
        }

        /// <summary>
        /// Briše zadatu kategoriju. Kategorija koja će biti izbrisana se određuje preko identifikatora. 
        /// Metod radi na sinhroni način.
        /// </summary>
        /// <example>
        /// DELETE: api/categories/5
        /// </example>
        /// <param name="id">Identifikator kategorije koja se briše.</param>
        /// <returns>Elemenat za povratni poziv.</returns>
        /// <response code="200">Brisanje je uspešno realizovano.</response>
        /// <response code="400">Pokušaj brisanja nije uspeo.</response>
        /// <response code="500">Greška u procesiranju na serverskoj strani.</response>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category ent = _categoryRepository.GetCategoryById(id); ;
            if (ent == null)
            {
                return BadRequest("Pokušaj brisanja grada koja ne postoji (pogrešan Id).");
            }
            _categoryRepository.DeleteCategory(id);
            return Ok();
        }

    }
}