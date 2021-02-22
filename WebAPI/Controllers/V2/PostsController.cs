using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V2
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("2.0")]
    //[Route("api/{v:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        //[SwaggerOperation(Summary = "Retrieves all posts")]
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var posts = _postService.GetAllPosts();
        //    return Ok(
        //        new 
        //        {
        //            Posts = posts,
        //            Count = posts.Count()
        //        });
        //}

        //[SwaggerOperation(Summary = "Retrieves a specific post by unique id")]
        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var post = _postService.GetPostById(id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(post);
        //}

        //[SwaggerOperation(Summary = "Retrieves posts by title")]
        //[HttpGet("{title}")]
        //public IActionResult Get(string  title)
        //{
        //    return Ok();
        //}

        //[SwaggerOperation(Summary = "Create a new post")]
        //[HttpPost]
        //public IActionResult Create(CreatePostDto newPost)
        //{
        //    var post = _postService.AddNewPost(newPost);
        //    return Created($"api/posts/{post.Id}", post);
        //}

        //[SwaggerOperation(Summary = "Update a existing post")]
        //[HttpPut]
        //public IActionResult Update (UpdatePostDto updatePost)
        //{
        //    _postService.UpdatePost(updatePost);
        //    return NoContent();
        //}

        //[SwaggerOperation(Summary = "Delete a specific post")]
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    _postService.DeletePost(id);
        //    return NoContent();
        //}
    }
}
