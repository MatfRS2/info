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
        /// Ubacuje zadati grad među gradove. 
        /// Metod radi na asinhroni način.
        /// </summary>
        /// <example>
        /// POST: olimp/korisnici/gradovi
        /// </example>
        /// <param name="ent">Grad koji se ubacuje.</param>
        /// <returns>Elemenat za povratni poziv tipa <see cref="T:OlimpDDD.CRUD.Domen.Grad"/>
        /// , koji predstavlja novododati grad.</returns>
        /// <response code="201">Putanja prema novododatom gradu.</response>
        /// <response code="400">Pokušaj dodavanja grada nije uspeo.</response>
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
    }
}