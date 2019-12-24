using System.Collections.Generic;

namespace DIinCore
{
    /// <summary>
    /// Repozitorijum za kategorije 
    /// </summary>
    /// <seealso cref="DIinCore.ICategoryRepository" />
    public class CategoryRepositoryInMemory : ICategoryRepository
    {


        private List<Category> kategorije = new List<Category>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryInMemory"/> class.
        /// </summary>
        public CategoryRepositoryInMemory()
        {
            kategorije.Add(new Category() { CategoryId = 10, CategoryName = "Košulja" });
            kategorije.Add(new Category() { CategoryId = 11, CategoryName = "Majica" });
            kategorije.Add(new Category() { CategoryId = 10, CategoryName = "Farmerke" });
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public List<Category> GetCategories()
        {
            return kategorije;
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Category GetCategoryById(int id)
        {
            foreach (Category kat in kategorije)
                if (kat.CategoryId == id)
                    return kat;
            return null;
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DeleteCategory(int id)
        {
            int pozicija = 0;
            for (int i = 0; i < kategorije.Count; i++)
                if (kategorije[i].CategoryId == id)
                    pozicija = i;
            kategorije.RemoveAt(pozicija);
        }

        /// <summary>
        /// Inserts the category.
        /// </summary>
        /// <param name="kategorija">The kategorija.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void InsertCategory(Category kategorija)
        {
            kategorije.Add(kategorija);
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="kategorija">The kategorija.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateCategory(int id, Category kategorija)
        {
            int pozicija = 0;
            for (int i = 0; i < kategorije.Count; i++)
                if (kategorije[i].CategoryId == id)
                    pozicija = i;
            kategorije[pozicija] = kategorija;
        }
    }
}
