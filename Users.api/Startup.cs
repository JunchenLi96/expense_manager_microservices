using Shared;
using Shared.Configurations;
using Users.api.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Users.api
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
            services.AddControllers();
            services.Configure<DbConfig>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<DbContext>(provider =>
            {
                string connectionString = provider.GetRequiredService<IOptions<DbConfig>>().Value.DefaultConnection;
                return new DbContext(connectionString);
            });

            services.AddSingleton<IUserService, UserService>();

            services.Configure<JwtConfig>(Configuration.GetSection("TokenOptions"));

            services.AddSingleton<ITokenService, TokenService>();

            services.AddMassTransit(x =>
            {
                x.UsingAzureServiceBus((context, config) =>
                {
                    string connectionString = Configuration.GetConnectionString("AzureConnectionString");
                    config.Host(connectionString);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
