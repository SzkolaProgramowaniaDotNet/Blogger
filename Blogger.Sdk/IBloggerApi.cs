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
        Task<ApiResponse<Response<PostDto>>> GetPostAsync(int id);

        [Post("/api/posts")]
        Task<ApiResponse<Response<PostDto>>> CreatePostAsync(CreatePostDto newPost);

        [Put("/api/posts")]
        Task UpdatePostAsync(UpdatePostDto updatePost);

        [Delete("/api/posts/{id}")]
        Task DeletePostAsync(int id);
    }
}
