using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MusicAPI.Data;
using MusicAPI.Services;
using MusicAPI.Services.Interfaces;
using MusicAPI.Web.Extensions;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace MusicAPI.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                const string name = "Bearer token";

                options.AddSecurityDefinition(name, new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = HeaderNames.Authorization,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>(true, name);
            });

            builder.Services
           .AddSqlServer<MusicDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"))
           .AddIdentity<IdentityUser, IdentityRole>(options =>
           {
               options.User.RequireUniqueEmail = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.Password.RequireLowercase = false;
           })
           .AddEntityFrameworkStores<MusicDbContext>();

            builder.Services.AddTransient<IGenreService, GenreService>();
            builder.Services.AddTransient<IArtistService, ArtistService>();
            builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();



            var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTSecret"]);

            builder.Services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            await app.SeedRolesAsync();
            await app.SeedUsersAsync();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}