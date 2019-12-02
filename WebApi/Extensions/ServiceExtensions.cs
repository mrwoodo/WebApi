using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.ModelBinders;
using WebApi.Models;

namespace WebApi.Extensions
{
    //https://code-maze.com/aspnetcore-webapi-best-practices/

    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public static void ConfigureIdentityServer(this IServiceCollection services, ISettings settings)
        {
            //https://docs.identityserver.io/en/latest/quickstarts/1_client_credentials.html

            services.AddIdentityServer()
                .AddInMemoryIdentityResources(settings.GetResources())
                .AddInMemoryApiResources(settings.GetApis())
                .AddInMemoryClients(settings.GetClients())
                .AddDeveloperSigningCredential();
        }

        public static void ConfigureBearerTokenAuthentication(this IServiceCollection services, ISettings settings)
        {
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = settings.Authority;
                    options.RequireHttpsMetadata = true;
                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                });
        }

        public static IMvcCoreBuilder AddMvcCoreWithCustomModelBinder(this IServiceCollection services)
        {
            return services.AddMvcCore(options =>
            {
                var binderToFind = options.ModelBinderProviders.FirstOrDefault(x => x.GetType() == typeof(SimpleTypeModelBinderProvider));

                if (binderToFind == null)
                    return;

                var index = options.ModelBinderProviders.IndexOf(binderToFind);

                options.ModelBinderProviders.Insert(index, new TrimmingModelBinderProvider());
            });
        }
    }
}