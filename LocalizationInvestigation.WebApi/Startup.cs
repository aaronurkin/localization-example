using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LocalizationInvestigation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(this.Configuration);
            services.AddBusinessLogic(this.Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            const string LOCALIZATION_DEFAULT_CULTURE_SECTION = "Localization:DefaultCulture";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseLocalization(this.Configuration);
            app.UseUnlocalizedRequestsRedirection(this.Configuration);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            var defaultCulture = this.Configuration[LOCALIZATION_DEFAULT_CULTURE_SECTION];

            if (string.IsNullOrEmpty(defaultCulture))
            {
                // TODO: Replace with correct exception type
                throw new System.Exception($"{LOCALIZATION_DEFAULT_CULTURE_SECTION} section is missing");
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: $"culture:{defaultCulture}/{{controller=WeatherForecast}}/{{action=Get}}/{{id?}}"
                );
            });
        }
    }
}
