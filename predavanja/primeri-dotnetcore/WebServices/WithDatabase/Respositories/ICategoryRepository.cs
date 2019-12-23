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
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        Category GetCategoryById(int categoryId);
    }
}
