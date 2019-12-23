using System.Collections.Generic;
using System.Linq;

namespace DIinCore
{
    /// <summary>
    /// Repozitorujum za kategorije 
    /// </summary>
    /// <seealso cref="DIinCore.ICategoryRepository" />
    public class CategoryRepositoryEF : ICategoryRepository
    {
        private KategorijeContext _kontekst;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryEF"/> class.
        /// </summary>
        /// <param name="kontekst">The kontekst.</param>
        public CategoryRepositoryEF(KategorijeContext kontekst)
        {
            _kontekst = kontekst;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            return _kontekst.Category.ToList();
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Category GetCategoryById(int categoryId)
        {
            return _kontekst.Category.Where(x => x.CategoryId == categoryId).SingleOrDefault();
        }
    }
}
