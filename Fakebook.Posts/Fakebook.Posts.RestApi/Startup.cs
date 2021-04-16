using Azure.Storage.Blobs;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.DataAccess.Repositories;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Fakebook.Posts.RestApi
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
            // setup Postgres database
            var connectionString = Configuration["ConnectionString:default"];
            services.AddDbContext<FakebookPostsContext>(options => options.UseNpgsql(connectionString));
            // setup Azure Blobs Service
            services.AddTransient<IBlobService, BlobService>(sp =>
                new BlobService(new BlobServiceClient(Configuration["BlobStorage:ConnectionString"]))
            );

            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IFollowsRepository, FollowsRepository>();
            services.AddSingleton<ITimeService, TimeService>();
            services.AddScoped<ICheckSpamService, CheckSpamService>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fakebook.Posts.RestApi", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://fakebook.revaturelabs.com/")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://revature-p3.okta.com/oauth2/default";
                    options.Audience = "api://default";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fakebook.Posts.RestApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
