using Azure.Core;
using CodePulse.API.Data;
using CodePulse.API.Models.DomainModels;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // 
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCatagoryRequestDto request)
        {
            //Map DEO to Domain Model
            var category = new Category
            {
                Name = request.Name,
                UrlHandel = request.UrlHandel
            };


            await _categoryRepository.CreateAsync(category);


            // Domain model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandel = category.UrlHandel

            };

            return Ok(response);
        }

    }
}
