using System.Collections.Generic;

namespace DIinCore
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            Category category = new Category() { CategoryId = 1, CategoryName = "Filmovi" };
            categories.Add(category);

            category = new Category() { CategoryId = 2, CategoryName = "Slike" };
            categories.Add(category);

            category = new Category() { CategoryId = 4, CategoryName = "Ploče" };
            categories.Add(category);

            return categories;
        }
    }
}
