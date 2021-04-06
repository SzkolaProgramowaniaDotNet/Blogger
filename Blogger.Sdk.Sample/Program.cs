using Blogger.Contracts.Requests;
using Refit;
using System;
using System.Threading.Tasks;

namespace Blogger.Sdk.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;

            var identityApi = RestService.For<IIdentityApi>("https://localhost:44371");
            var bloggerApi = RestService.For<IBloggerApi>("https://localhost:44371", new RefitSettings 
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            //var register = await identityApi.RegisterAsync(new RegisterModel()
            //{
            //    Email = "sdkaccount@gmail.com",
            //    Username = "sdkaccount",
            //    Password = "Pa$$w0rd123!"
            //});

            var login = await identityApi.LoginAsync(new LoginModel()
            {
                Username = "sdkaccount",
                Password = "Pa$$w0rd123!"
            });

            cachedToken = login.Content.Token;

            var createdPost = await bloggerApi.CreateAsync(new CreatePostDto 
            { 
                Title = "Post Sdk",
                Content = "Treść Sdk"
            });

            var retrievedPost = await bloggerApi.GetAsync(createdPost.Content.Data.Id);

            await bloggerApi.UpdateAsync(new UpdatePostDto 
            {
                Id = createdPost.Content.Data.Id,
                Content = "Nowa treść Sdk"
            });

            await bloggerApi.DeleteAsync(createdPost.Content.Data.Id);
        }
    }
}
