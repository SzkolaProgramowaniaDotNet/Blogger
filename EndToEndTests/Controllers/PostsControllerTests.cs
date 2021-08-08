using Application.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAPI;
using WebAPI.Wrappers;
using Xunit;

namespace EndToEndTests.Controllers
{
    public class PostsControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PostsControllerTests()
        {
            // Arrange
            var projectDir = Helper.GetProjectPath("", typeof(Startup).GetTypeInfo().Assembly);
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(projectDir)
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(projectDir)
                    .AddJsonFile("appsettings.json")
                    .Build()
                )
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task fetching_posts_should_return_not_empty_collection()
        {
            // Act
            var response = await _client.GetAsync(@"api/Posts");
            var content = await response.Content.ReadAsStringAsync();
            var pagedResponse = JsonConvert.DeserializeObject<PagedResponse<IEnumerable<PostDto>>>(content);

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            pagedResponse.Data.Should().NotBeEmpty();
        }
    }
}
