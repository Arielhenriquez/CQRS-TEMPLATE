using Microsoft.AspNetCore.Mvc.Testing;
using RBACV2.API.Settings;
using RBACV2.Application.UsersEntity.Dtos;
using RBACV2.Domain.BaseResponse;
using RBACV2.Test.UserTest.IntegrationTests.Helpers;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RBACV2.Test.UserTest.IntegrationTests.EndpointTests
{
    public class UsersIntegrationTest : IClassFixture<WebApplicationFactory<AppSetup>>
    {
        private readonly WebApplicationFactory<AppSetup> _factory;

        public UsersIntegrationTest(WebApplicationFactory<AppSetup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Returns_Successful_Response()
        {
            var client = _factory.CreateClient();
            var endpoint = "api/users?PageNumber=1&PageSize=5";

            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions();
            options.Converters.Add(new BaseResponseConverter<UserResponseDto>());

            var responseObject = JsonSerializer.Deserialize<BaseResponse<UserResponseDto>>(responseContent, options);

            Assert.NotNull(responseObject);
            Assert.NotNull(responseObject.Data);
        }
    }

}
