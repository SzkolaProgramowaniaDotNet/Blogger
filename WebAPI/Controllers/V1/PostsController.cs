using App.Metrics;
using Application.Dto;
using Application.Interfaces;
using Application.Validators;
using Infrastructure.Identity;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Cache;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Metrics;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{

    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger _logger;
        private readonly IMetrics _metrics;


        public PostsController(IPostService postService, IMemoryCache memoryCache, ILogger<PostsController> logger, IMetrics metrics)
        {
            _postService = postService;
            _memoryCache = memoryCache;
            _logger = logger;
            _metrics = metrics;
        }

        [SwaggerOperation(Summary = "Retrieves sort fields")]
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            return Ok(SortingHelper.GetSortFields().Select(x => x.Key));
        }

        [SwaggerOperation(Summary = "Retrieves paged posts")]
        [AllowAnonymous]
        [Cached(600)]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationFilter paginationFilter, [FromQuery] SortingFilter sortingFilter, [FromQuery] string filterBy = "")
        {
            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

            var posts = await _postService.GetAllPostsAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                            validSortingFilter.SortField, validSortingFilter.Ascending,
                                                            filterBy);

            var totalRecords = await _postService.GetAllPostsCountAsync(filterBy);

            return Ok(PaginationHelper.CreatePagedResponse(posts, validPaginationFilter, totalRecords));
        }

        [SwaggerOperation(Summary = "Retrieves all posts")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("[action]")]
        [EnableQuery]
        public IQueryable<PostDto> GetAll()
        {
            var posts = _memoryCache.Get<IQueryable<PostDto>>("posts");
            if (posts == null)
            {
                _logger.LogInformation("Fetching from service.");
                posts = _postService.GetAllPostsAsync();
                _memoryCache.Set("posts", posts, TimeSpan.FromMinutes(1));
            }
            else
            {
                _logger.LogInformation("Fetching from cache");
            }



            return posts;
        }

        [SwaggerOperation(Summary = "Retrieves a specific post by unique id")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(new Response<PostDto>(post));
        }

        [ValidateFilter]
        [SwaggerOperation(Summary = "Create a new post")]
        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatePostDto newPost)
        {
            var post = await _postService.AddNewPostAsync(newPost, User.FindFirstValue(ClaimTypes.NameIdentifier));
            _metrics.Measure.Counter.Increment(MetricsRegistry.CreatedPostsCounter);
            return Created($"api/posts/{post.Id}", new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Update a existing post")]
        [Authorize(Roles = UserRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdatePostDto updatePost)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(updatePost.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!userOwnsPost)
            {
                return BadRequest(new Response(false, "You do not own this post."));
            }

            await _postService.UpdatePostAsync(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete a specific post")]
        [Authorize(Roles = UserRoles.AdminOrUser)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isAdmin = User.IsInRole(UserRoles.Admin);

            if (!isAdmin && !userOwnsPost)
            {
                return BadRequest(new Response(false, "You do not own this post."));
            }

            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
