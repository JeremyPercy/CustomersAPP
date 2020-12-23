using System;
using System.Net;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SupportecCustomer.API.Data;
using SupportecCustomer.API.Helpers;

namespace SupportecCustomer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<Seed>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //     services.AddAuthentication(options =>
            // {
            //     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //     options.DefaultChallengeScheme = "ExactOnline";
            // })
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                })
                .AddCookie("Cookies")
                .AddOAuth("ExactOnline", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.ClientId = Configuration.GetSection("ExactConnectionStrings:ClientId").Value;
                    options.ClientSecret = Configuration.GetSection("ExactConnectionStrings:Secret").Value;
                    options.CallbackPath = new PathString("/api/exact/oauth/");
                    options.AuthorizationEndpoint = "https://start.exactonline.nl/api/oauth2/auth";
                    options.TokenEndpoint = "https://start.exactonline.nl/api/oauth2/token";
                    options.UserInformationEndpoint = "https://start.exactonline.nl/api/v1/current/Me";
                    options.SaveTokens = true;

                    // options.Events = new OAuthEvents
                    // {
                    //     OnCreatingTicket = context =>
                    //     {
                    //         context.Identity.AddClaim(new Claim("urn:token:exactonline", context.AccessToken));
                    //         context.Identity.AddClaim(new Claim("urn:refreshtoken:exactonline", context.RefreshToken));
                    //         return Task.FromResult(true);
                    //     }
                    // };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                               {
                                   builder.Run(async context =>
                                   {
                                       context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                       var error = context.Features.Get<IExceptionHandlerFeature>();
                                       if (error != null)
                                       {
                                           context.Response.AddApplicationError(error.Error.Message);
                                           await context.Response.WriteAsync(error.Error.Message);
                                       }
                                   });
                               });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DataContext>();
                dbContext.Database.EnsureCreated();
            }
        }
    }
}
