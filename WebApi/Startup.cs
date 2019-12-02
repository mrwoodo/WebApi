using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        private readonly IHostingEnvironment _Environment;
        private readonly ILogger _Logger;
        private Settings _settings;

        public Startup(IConfiguration configuration,
            IHostingEnvironment environment,
            ILogger<Startup> logger)
        {
            _Configuration = configuration;
            _Environment = environment;
            _Logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var settingsSection = _Configuration.GetSection(nameof(Settings));
            _settings = settingsSection.Get<Settings>();

            services.AddSingleton<ISettings>(_settings);
            services.ConfigureCors();
            services.ConfigureIdentityServer(_settings);
            services.ConfigureBearerTokenAuthentication(_settings);
            services.AddMvcCoreWithCustomModelBinder()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
               .AddAuthorization()
               .AddJsonFormatters()
               .AddDataAnnotations()
               .AddApiExplorer();

            if (_Environment.IsDevelopment())
            {
                services.ConfigureSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                    {
                        Type = "oauth2",
                        Flow = "application",
                        TokenUrl = "/connect/token",
                        Scopes = new Dictionary<string, string>
                        {
                            {
                                _settings.APIClientScopes[0],
                                _settings.APIClientScopes[0]
                            }
                        }
                    });

                    options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            "oauth2", new[]
                            {
                                _settings.APIClientScopes[0],
                                _settings.APIClientScopes[0]
                            }
                        }
                    });
                });

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Sample API", Version = "v1" });
                });
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler(_Logger);
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseMvc();

            if (_Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.OAuthClientId(_settings.APIClientIDs[0]);
                    options.OAuthClientSecret(_settings.APIClientSecrets[0]);
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API");
                });
            }
        }
    }
}