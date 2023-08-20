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

        //GET: /api/categories
        [HttpGet]

        public async Task<IActionResult> GetAllCategories()
        {
            var catagories = await _categoryRepository.GetAllAsync();

            //Map Domain model to DTo

            var response = new List<CategoryDto>();

            foreach (var category in catagories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandel = category.UrlHandel
                });
            }

            return Ok(response);
        }


        //GET: /api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute]Guid id)
        {
            var existingCategory = await _categoryRepository.GetById(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandel = existingCategory.UrlHandel
            };

            return Ok(response);
        }

        // PUT : /api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, [FromBody]UpdateCategoryRequestDTO request)
        {
            // Convert DTO to 

            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandel = request.UrlHandel
            };

            category = await _categoryRepository.UpdateAsync(category);

            if (category == null)
            {
                return NotFound();
            }

            // Convert Domain model to DTO
            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandel = category.UrlHandel
            };

            return Ok(response);
        }

        //DELETE: /api/categories/{id}

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCategory([FromRoute]Guid id)
        {
            //Write Code of Delete Action Method 

            var category = await _categoryRepository.DeleteAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            //Conver Domain Model to Dto

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
