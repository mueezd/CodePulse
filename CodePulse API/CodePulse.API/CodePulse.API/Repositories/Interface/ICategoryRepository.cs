using CodePulse.API.Models.DomainModels;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);

    }
}
