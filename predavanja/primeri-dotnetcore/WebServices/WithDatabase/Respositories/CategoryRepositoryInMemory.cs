using System.Collections.Generic;

namespace DIinCore
{
    /// <summary>
    /// Repozitorujum za kategorije 
    /// </summary>
    /// <seealso cref="DIinCore.ICategoryRepository" />
    public class CategoryRepositoryInMemory : ICategoryRepository
    {
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            Category category = new Category() { CategoryId = 1, CategoryName = "Category1" };
            categories.Add(category);

            category = new Category() { CategoryId = 2, CategoryName = "Category2" };
            categories.Add(category);

            return categories;
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Category GetCategoryById(int categoryId)
        {
            throw new System.NotImplementedException();
        }
    }
}
