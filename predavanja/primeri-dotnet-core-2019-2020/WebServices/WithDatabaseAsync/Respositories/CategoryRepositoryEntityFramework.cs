using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public Task<List<Category>> GetCategoriesAsync()
        {
            return _kontekst.Category.ToListAsync();
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The category identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _kontekst.Category.Where(
                x => x.CategoryId == id).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task DeleteCategoryAsync(int id)
        {
            Category kat = await _kontekst.Category.Where(
                x => x.CategoryId == id).SingleOrDefaultAsync();
            _kontekst.Category.Remove(kat);
            await _kontekst.SaveChangesAsync();
        }


        /// <summary>
        /// Inserts the category.
        /// </summary>
        /// <param name="kategorija">The kategorija.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task InsertCategoryAsync(Category kategorija)
        {
            _kontekst.Category.Add(kategorija);
            await _kontekst.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="kategorija">The kategorija.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task UpdateCategoryAsync(int id, Category kategorija)
        {
            Category kat = await _kontekst.Category.Where(
                x => x.CategoryId == id).SingleOrDefaultAsync();
            if (kat == null)
                return;
            kat.CategoryId = kategorija.CategoryId;
            kat.CategoryName = kategorija.CategoryName;
            _kontekst.Category.Update(kat);
            await _kontekst.SaveChangesAsync();
        }

    }
}
