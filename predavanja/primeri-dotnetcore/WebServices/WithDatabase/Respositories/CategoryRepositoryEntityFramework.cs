using System;
using System.Collections.Generic;
using System.Linq;

namespace DIinCore
{
    /// <summary>
    /// Repozitorujum za kategorije 
    /// </summary>
    /// <seealso cref="DIinCore.ICategoryRepository" />
    public class CategoryRepositoryEntityFramework : ICategoryRepository
    {
        private KategorijeContext _kontekst;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepositoryEntityFramework"/> class.
        /// </summary>
        /// <param name="kontekst">The kontekst.</param>
        public CategoryRepositoryEntityFramework(KategorijeContext kontekst)
        {
            _kontekst = kontekst ?? throw new ArgumentNullException(nameof(kontekst));
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
        /// <param name="id">The category identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Category GetCategoryById(int id)
        {
            return _kontekst.Category.Where(x => x.CategoryId == id).SingleOrDefault();
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DeleteCategory(int id)
        {
            Category kat = _kontekst.Category.Where(x => x.CategoryId == id).SingleOrDefault();
            _kontekst.Category.Remove(kat);
            _kontekst.SaveChanges();
        }


        /// <summary>
        /// Inserts the category.
        /// </summary>
        /// <param name="kategorija">The kategorija.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void InsertCategory(Category kategorija)
        {
            _kontekst.Category.Add(kategorija);
            _kontekst.SaveChanges();
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="kategorija">The kategorija.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UpdateCategory(int id, Category kategorija)
        {
            Category kat = _kontekst.Category.Where(x => x.CategoryId == id).SingleOrDefault();
            if (kat == null)
                return;
            kat.CategoryId = kategorija.CategoryId;
            kat.CategoryName = kategorija.CategoryName;
            _kontekst.Category.Update(kat);
            _kontekst.SaveChanges();
        }
    }
}
