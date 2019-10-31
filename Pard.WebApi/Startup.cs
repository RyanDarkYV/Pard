using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pard.API.Models.Identity;
using Pard.Application.Auth;
using Pard.Application.Interfaces;
using Pard.Application.Services;
using Pard.Application.ViewModels.Validations;
using Pard.Domain.Entities.Identity;
using Pard.Persistence.Contexts;
using Pard.Persistence.Repositories.Records;
using Pard.WebApi.Extensions;
using Serilog;
using System;
using System.Net;
using System.Text;

namespace Pard.WebApi
{
    public class Startup
    {
        private const string SecretKey = "whfsdhewefoewodwIEUDHdjwrYgGtHf;kjOiJ13123YRvSDiqweODDJFjdJNVZrqRUpQW2DfjaYrV";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            //var loggerConfig = new LoggerConfiguration()
            //    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200") ){
            //        AutoRegisterTemplate = true,
            //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
            //    });
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerExpressIdentity")));
            services.AddDbContext<RecordsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerExpressRecords")));

#pragma warning disable 618
            services.AddAutoMapper();
#pragma warning restore 618
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddTransient<IRecordsRepository, SqlServerRecordsRepository>();
            services.AddTransient<ILocationsRepository, SqlServerLocationsRepository>();
            services.AddTransient<IRecordsService, RecordsService>();
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<IArchiveService, ArchiveService>();
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RecordViewModelValidator>());

            
            var jwtSettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(opt =>
            {
                opt.Issuer = jwtSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                opt.Audience = jwtSettingsOptions[nameof(JwtIssuerOptions.Audience)];
                opt.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettingsOptions[nameof(JwtIssuerOptions.Issuer)],
                ValidateAudience = true,
                ValidAudience = jwtSettingsOptions[nameof(JwtIssuerOptions.Audience)],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.ClaimsIssuer = jwtSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                cfg.TokenValidationParameters = tokenValidationParameters;
                cfg.SaveToken = true;
               });
            
            services.AddCors(c =>  
            {  
                c.AddPolicy("Cors", options =>
                {
                    options.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });  
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ApiUser", policy => policy.RequireRole("User"));
                opt.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                opt.AddPolicy("Superadmin", policy => policy.RequireRole("Superadmin"));
            });

            var identityBuilder = services.AddIdentityCore<AppUser>(opt => // тут ошибка в IdentityRole
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
            });
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();
            identityBuilder.AddRoleValidator<RoleValidator<IdentityRole>>();
            identityBuilder.AddRoleManager<RoleManager<IdentityRole>>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(
                opt =>
                    opt.Run(async context =>
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                        }
                    }));

            
            app.UseCors("Cors");
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseMvc();
        }
    }
}
