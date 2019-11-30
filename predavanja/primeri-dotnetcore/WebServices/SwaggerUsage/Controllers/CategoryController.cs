using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
  
namespace DIinCore.Controllers
{
    /// <summary>
    /// Kontroler veb servisa koji vrace kategorije 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private ICategoryRepository categoryRepository { get; set; }
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Vraće kategorije.
        /// </summary>
        /// <returns> OK kod ako je sve OK.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = categoryRepository.GetCategories();
            return Ok(categories);
        }
    }
}