using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Services
{
    public class PostServiceTests
    {
        [Fact]
        public async Task add_post_async_should_invoke_add_async_on_post_repository()
        {
            // Arrange
            var postRepositoryMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<PostService>>();

            var postService = new PostService(postRepositoryMock.Object, mapperMock.Object, loggerMock.Object);

            var postDto = new CreatePostDto()
            {
                Title = "Title 1",
                Content = "Content 1"
            };

            mapperMock.Setup(x => x.Map<Post>(postDto)).Returns(new Post() { Title = postDto.Title, Content = postDto.Content });

            // Act
            await postService.AddNewPostAsync(postDto, "1b8e3e30-d943-47c2-bb85-332a1ffc633f");

            // Assert
            postRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task when_invoking_get_post_async_it_should_invoke_get_async_on_post_repository()
        {
            // Arrange
            var postRepositoryMock = new Mock<IPostRepository>();
            var mapperMock = new Mock<IMapper>();
            var loggerMock = new Mock<ILogger<PostService>>();

            var postService = new PostService(postRepositoryMock.Object, mapperMock.Object, loggerMock.Object);

            var post = new Post(1, "Title 1", "Content 1");
            var postDto = new PostDto()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
            };

            mapperMock.Setup(x => x.Map<Post>(postDto)).Returns(post);
            postRepositoryMock.Setup(x => x.GetByIdAsync(post.Id)).ReturnsAsync(post);

            // Act

            var existingPostDto = await postService.GetPostByIdAsync(post.Id);

            // Assert

            postRepositoryMock.Verify(x => x.GetByIdAsync(post.Id), Times.Once);
            postDto.Should().NotBeNull();
            postDto.Title.Should().NotBeNull();
            postDto.Title.Should().BeEquivalentTo(post.Title);
            postDto.Content.Should().NotBeNull();
            postDto.Content.Should().BeEquivalentTo(post.Content);
        }
    }
}
