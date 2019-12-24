using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIinCore
{
    /// <summary>
    /// Interfejs koji opisuje repozitorijum za kategorije
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        Task<List<Category>> GetCategoriesAsync();

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Category> GetCategoryByIdAsync(int id);

        /// <summary>
        /// Inserts the category.
        /// </summary>
        /// <param name="kategorija">The kategorija.</param>
        Task InsertCategoryAsync(Category kategorija);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="kategorija">The kategorija.</param>
        Task UpdateCategoryAsync(int id, Category kategorija);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        Task DeleteCategoryAsync(int id);
    }
}
