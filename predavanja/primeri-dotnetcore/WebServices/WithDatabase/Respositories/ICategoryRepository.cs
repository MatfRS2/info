using System.Collections.Generic;

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
        List<Category> GetCategories();

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Category GetCategoryById(int id);

        /// <summary>
        /// Inserts the category.
        /// </summary>
        /// <param name="kategorija">The kategorija.</param>
        void InsertCategory(Category kategorija);

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="kategorija">The kategorija.</param>
        void UpdateCategory(int id, Category kategorija);

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCategory(int id);
    }
}
