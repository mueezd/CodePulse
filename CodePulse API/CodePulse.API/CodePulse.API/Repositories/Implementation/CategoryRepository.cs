using Azure.Core;
using CodePulse.API.Data;
using CodePulse.API.Models.DomainModels;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _dbContext.Catagories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null)
            {
                return null;
            }

            _dbContext.Catagories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();   
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Catagories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid Id)
        {
            return await _dbContext.Catagories.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existingCategory = await _dbContext.Catagories.FirstOrDefaultAsync(x => x.Id == category.Id);

            if (existingCategory != null)
            {
                _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
                return category;
            }
            return null;
        }
    
    }
}
