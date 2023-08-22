using CodePulse.API.Models.DomainModels;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        //POST: {apibaseurl}/api/blogpost

        [HttpPost]
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
                IsVisable = request.IsVisable
            };

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
                IsVisable = blogPost.IsVisable
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
                    IsVisable = blogPost.IsVisable
                });

            }

            return Ok(response);
        }
    }
}
