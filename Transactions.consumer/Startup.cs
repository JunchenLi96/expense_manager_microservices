using Shared;
using Shared.Configurations;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Users.events;

namespace Transactions.consumer
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbConfig>(Configuration.GetSection("ConnectionStrings"));
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserConsumer>();
                x.UsingAzureServiceBus((context, config) =>
                {
                    string connectionString = Configuration.GetConnectionString("AzureConnectionString");
                    config.Host(connectionString);
                    config.SubscriptionEndpoint<UserCreated>("test", config =>
                     {
                         config.ConfigureConsumer<UserConsumer>(context);
                     });
                });
            });
            services.AddMassTransitHostedService();

            services.AddSingleton<DbContext>(provider =>
            {
                string connectionString = provider.GetRequiredService<IOptions<DbConfig>>().Value.DefaultConnection;
                return new DbContext(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
