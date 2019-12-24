using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
  
namespace DIinCore.Controllers
{
    /// <summary>
    /// Kontroler veb servisa koji vrace kategorije 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Vraće kategorije.
        /// </summary>
        /// <returns>
        /// OK kod ako je sve OK.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = _categoryRepository.GetCategories();
            return Ok(categories);
        }
    }
}