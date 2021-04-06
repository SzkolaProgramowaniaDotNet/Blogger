using Blogger.Contracts.Requests;
using Blogger.Contracts.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Sdk
{
    [Headers("Authorization: Bearer")]
    public interface IBloggerApi
    {
        [Get("/api/posts/{id}")]
        Task<ApiResponse<Response<PostDto>>> GetAsync(int id);

        [Post("/api/posts")]
        Task<ApiResponse<Response<PostDto>>> CreateAsync(CreatePostDto newPost);

        [Put("/api/posts")]
        Task UpdateAsync(UpdatePostDto updatePost);

        [Delete("/api/posts/{id}")]
        Task DeleteAsync(int id);
    }
}
