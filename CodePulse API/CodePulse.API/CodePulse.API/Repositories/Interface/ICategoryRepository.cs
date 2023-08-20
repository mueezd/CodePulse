﻿using CodePulse.API.Models.DomainModels;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetById(Guid Id);
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);



    }
}
