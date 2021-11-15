using System.Text;
using ExpenseManagerDbContext;
using ExpenseManagerUsers.Configurations;
using ExpenseManagerUsers.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagerUsers
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
            services.Configure<Dbconfig>(Configuration.GetSection("ConnectionStrings"));

            services.AddSingleton<DbContext>(provider =>
            {
                string connectionString = provider.GetRequiredService<IOptions<Dbconfig>>().Value.DefaultConnection;
                return new DbContext(connectionString);
            });

            services.AddSingleton<IUserService, UserService>();

            services.Configure<JwtConfig>(Configuration.GetSection("TokenOptions"));

            services.AddSingleton<ITokenService, TokenService>();

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
