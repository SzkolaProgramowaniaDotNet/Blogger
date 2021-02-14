using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostService
    {
        IEnumerable<PostDto> GetAllPosts();
        PostDto GetPostById(int id);
        PostDto AddNewPost(CreatePostDto newPost);
        void UpdatePost(UpdatePostDto updatePost);
        void DeletePost(int id);
    }
}
