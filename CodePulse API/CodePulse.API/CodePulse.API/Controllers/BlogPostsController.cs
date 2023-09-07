using CodePulse.API.Models.DomainModels;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
        }
        //POST: {apibaseurl}/api/blogpost

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateBlogPost([FromBody]CreateBlogPostRequestDto request)
        {
            // Convert Dto to Model

            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandel = request.UrlHandel,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisable = request.IsVisable,
                Categories = new List<Category>()
            };

            //

            foreach (var categoruGuid in request.Categories)
            {
                var existingCatagory = await _categoryRepository.GetById(categoruGuid);
                if (existingCatagory is not null)
                {
                    blogPost.Categories.Add(existingCatagory);
                }

            }

            blogPost = await _blogPostRepository.CreateAsync(blogPost);
            //Convert Domain model to DTO

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandel = blogPost.UrlHandel,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisable = blogPost.IsVisable,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandel = x.UrlHandel
                }).ToList()
            };

            return Ok(response);
        }

        // GET: 
        [HttpGet]

        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPosts = await _blogPostRepository.GetAllAsync();

            // Convert Domain Model to DTO

            var response = new List<BlogPostDto>();

            foreach (var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Title = blogPost.Title,
                    ShortDescription = blogPost.ShortDescription,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandel = blogPost.UrlHandel,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    IsVisable = blogPost.IsVisable,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandel = x.UrlHandel
                    }).ToList()
                });

            }

            return Ok(response);
        }

        //GET:

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute]Guid id)
        {
            // Get the blog post from repository
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            if (blogPost is null)
            {
                return NotFound();
            }

            //Convert to domain mo0del to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandel = blogPost.UrlHandel,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisable = blogPost.IsVisable,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandel = x.UrlHandel
                }).ToList()
            };

            return Ok(response);

        }

        //GET: By UrlHandel
        [HttpGet]
        [Route("{urlHandel}")]
        public async Task<IActionResult> GetBlogPostByUrlHandel([FromRoute] string urlHandel)
        {
            //get blog popst detail from repository

            var blogPost = await _blogPostRepository.GetByUrlHandelAsync(urlHandel);

            //Convert To DTO

            if (blogPost is null)
            {
                return NotFound();
            }

            //Convert to domain mo0del to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandel = blogPost.UrlHandel,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisable = blogPost.IsVisable,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandel = x.UrlHandel
                }).ToList()
            };

            return Ok(response);

        }


        // PUT: {apibaseurl}/api/blogpost/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateLopgPostById([FromRoute] Guid id, [FromBody] UpdateBlogPostRequestDto request)
        {
            //Convert Dto to domain model

            var blogPost = new BlogPost
            {
                Id = id,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandel = request.UrlHandel,
                PublishedDate = request.PublishedDate,
                Author = request.Author,
                IsVisable = request.IsVisable,
                Categories = new List<Category>()
            };

            // Foreach Loop  

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await _categoryRepository.GetById(categoryGuid);

                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            } 
            
            //Call repository to update blogpost domain model 


            var updatedBlogPost = await _blogPostRepository.UpdateAsync(blogPost);

            if (updatedBlogPost == null)
            {
                return NotFound();
            }


            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandel = blogPost.UrlHandel,
                PublishedDate = blogPost.PublishedDate,
                Author = blogPost.Author,
                IsVisable = blogPost.IsVisable,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandel = x.UrlHandel
                }).ToList()
            };


            return Ok(response);

        }


        //DELETE: This for Delete Method

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            var deletedBlogPost = await _blogPostRepository.DeleteAsync(id);

            if (deletedBlogPost == null)
            {
                return NotFound();
            }

            //Convert Domain Model TO DTO
            var response = new BlogPostDto
            {
                Id = deletedBlogPost.Id,
                Title = deletedBlogPost.Title,
                ShortDescription = deletedBlogPost.ShortDescription,
                Content = deletedBlogPost.Content,
                FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                UrlHandel = deletedBlogPost.UrlHandel,
                PublishedDate = deletedBlogPost.PublishedDate,
                Author = deletedBlogPost.Author,
                IsVisable = deletedBlogPost.IsVisable
               
            };

            return Ok(response);
        }

    }
}
