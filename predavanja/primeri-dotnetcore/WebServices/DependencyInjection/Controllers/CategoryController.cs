using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
  
namespace DIinCore.Controllers
{
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> categories = _categoryRepository.GetCategories();
            return Ok(categories);
        }
    }
}