using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using RBACV2.Application.Common.Interfaces.Abstract;
using RBACV2.Application.Common.Settings;
using System.Net.Http.Headers;

namespace RBACV2.Infrastructure.Services.GraphProviders
{
    public class AzureADUserProvider : IAzureADUserProvider
    {
        private readonly GraphServiceClient _graphServiceClient;

        public AzureADUserProvider(IOptions<AzureAdClientSettings> options)
        {

            var scopes = new string[] { options.Value.GraphUri! };
            var confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(options.Value.ClientId)
                .WithTenantId(options.Value.TenantId)
                .WithClientSecret(options.Value.ClientSecret)
                .Build();

            _graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                var authResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync();
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authResult.AccessToken);
            }));
        }

        public async Task<User> Create(User user)
        {
            return await _graphServiceClient.Users
                .Request()
                .AddAsync(user);
        }

        public async Task Delete(string userOid)
        {
            await _graphServiceClient.Users[userOid]
               .Request()
               .DeleteAsync();

        }

        public async Task<User> FindById(string userOid)
        {
            return await _graphServiceClient.Users[userOid]
               .Request()
               .GetAsync();
        }

        public async Task<bool> UserPrincipalExists(string userPrincipalName)
        {
            var user = await _graphServiceClient.Users
               .Request()
               .Filter($"userPrincipalName eq '{userPrincipalName}'")
               .GetAsync();

            if (user.Any())
            {
                return false;
            }
            return true;
        }

        public async Task<User> FindByName(string userPrincipalName)
        {
            var user = await _graphServiceClient.Users
              .Request()
              .Filter($"userPrincipalName eq '{userPrincipalName}'")
              .GetAsync();

            return user.FirstOrDefault()!;
        }

        public async Task<IList<User>> Get()
        {
            return await _graphServiceClient.Users
                .Request()
                .GetAsync();
        }

        public async Task<User> Update(string userOid, User user)
        {
            return await _graphServiceClient.Users[userOid]
                .Request()
                .UpdateAsync(user);
        }

        public async Task<Stream> GetProfilePhoto(string userOid)
        {
            return await _graphServiceClient.Users[userOid].Photo.Content
                            .Request()
                            .GetAsync();
        }
    }
}
