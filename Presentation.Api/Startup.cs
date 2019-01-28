using Application.Services.CommentService;
using Data.Repository.Context;
using Data.Repository.Repositories;
using Domain.Core.RepositoryInterfaces;
using Domain.Services.Services;
using Infrastructure.Crosscuting;
using Infrastructure.Crosscuting.Adapters.Automapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Api.Model;
using Swashbuckle.AspNetCore.Swagger;
using System.Threading.Tasks;

namespace Presentation.Api
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
            var connectionInventory = Configuration.GetConnectionString("ChallengeDbConnection");
            services.AddDbContext<ChallengeContext>(options =>
                options.UseSqlServer(connectionInventory));

            services.AddTransient<ICommentActionsRepository, CommentActionsRepository>();
            services.AddTransient<ICommentsRepository, CommentRepository>();
            services.AddTransient<IRelatedCommentsRepository, RelatedCommentsRepository>();
            services.AddTransient<ICommentStateService, CommentStateService>();
            services.AddTransient<ICommentService>
            (
                    s => new CommentService
                    (
                        s.GetService<ICommentStateService>(),
                        s.GetService<ICommentsRepository>(),
                        s.GetService<ICommentActionsRepository>(),
                        s.GetService<IRelatedCommentsRepository>()
                    )
            );
          
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Challenge Service", Version = "v1" });
            });

            services.AddIdentity<User, Role>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(cfg =>
                cfg.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api"))
                            ctx.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;

                        return Task.FromResult(0);
                    }
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Challenge Service V1");
            });

            app.UseMvc();

            app.UseAuthentication();
        }
    }
}
