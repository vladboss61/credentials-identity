using Credentials.Identity.Services;
using IdentityServer4.Models;

namespace Credentials.Identity
{
    internal sealed class Program
    {
        private static IReadOnlyCollection<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("test-1")
                {
                    Scopes = { "test-1" }
                },
                new ApiResource("test-2")
                {
                    Scopes = { "test-2" }
                }
            };
        }

        private static IReadOnlyCollection<ApiScope> GetApiScope()
        {
            return new[]
            {
                new ApiScope("test-1"),
                new ApiScope("test-2")
            };
        }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder
                .Host
                .UseContentRoot(Directory.GetCurrentDirectory());

            var services = builder.Services;
            services.AddControllers();
            services
                .AddIdentityServer(
                    options =>
                    {
                        options.Discovery.ShowResponseModes = false;
                        options.Endpoints.EnableAuthorizeEndpoint = false;
                        options.Endpoints.EnableCheckSessionEndpoint = false;
                        options.Endpoints.EnableEndSessionEndpoint = false;
                        options.Endpoints.EnableTokenRevocationEndpoint = false;
                        options.Endpoints.EnableUserInfoEndpoint = false;
                        options.Endpoints.EnableIntrospectionEndpoint = false;
                    })
                .AddDeveloperSigningCredential()
                .AddClientStore<CredentialsStore>()
                .AddSecretValidator<DefaultSecretValidator>()
                //Adds aud array to token
                //.AddInMemoryApiResources(GetApiResources())
                .AddInMemoryApiScopes(GetApiScope());

            WebApplication app =  builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseIdentityServer();

            app.Run();
        }
    }
}