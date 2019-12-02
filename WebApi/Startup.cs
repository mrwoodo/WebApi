using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Extensions;
using WebApi.Models;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;
        private readonly IHostingEnvironment _Environment;
        private readonly ILogger _Logger;

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
            var settings = settingsSection.Get<Settings>();

            services.AddSingleton<ISettings>(settings);
            services.ConfigureCors();
            services.ConfigureIdentityServer(settings);
            services.ConfigureBearerTokenAuthentication(settings);
            services.AddMvcCoreWithCustomModelBinder()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddAuthorization()
                .AddJsonFormatters()
                .AddDataAnnotations();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler(_Logger);
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}