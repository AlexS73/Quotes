using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Quotes.Core.HelpModel;
using Quotes.Core.Models;
using Quotes.Data;
using Quotes.Interfaces;
using Quotes.Services;
using System;
using System.Text;

namespace Quotes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuotesContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<QuotesContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataService, DataService>();

            var tokenSettingsSection = Configuration.GetSection("TokenSettings");
            services.Configure<TokenModel>(tokenSettingsSection);

            var tokenSettings = tokenSettingsSection.Get<TokenModel>();
            byte[] key = Encoding.ASCII.GetBytes(tokenSettings.Key);

            services.AddAuthentication(_ =>
            {
                _.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                                // валидация издателя
                                ValidateIssuer = true,
                                // издатель
                                ValidIssuer = tokenSettings.Issuer,
                                // валидация потребителя
                                ValidateAudience = true,
                                // потребитель
                                ValidAudience = tokenSettings.Audience,
                                // валидация времени жизни
                                ValidateLifetime = true,
                                // установка ключа безопасности
                                IssuerSigningKey = new SymmetricSecurityKey(key),
                                // валидация ключа безопасности
                                ValidateIssuerSigningKey = true,
                                // по умолчанию время жизни 5 минут
                                ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
