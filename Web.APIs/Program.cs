using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web.APIs.Models;
using Web.APIs.Repositories;

namespace Web.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🟢 1. Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // 🟢 2. Add Repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            // 🟢 3. Add Authentication with JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7068/",
                    ValidAudience = "https://localhost:7068/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Keep it simple stupid 2003 Ziad Ghoraba 2003 @eng.ziadghoraba@gmail.com"))
                };

                // ✅ Logging for debugging
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("❌ JWT ERROR: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("✅ JWT VALIDATED");
                        return Task.CompletedTask;
                    }
                };
            });

            // 🟢 4. Add Identity without cookie
            builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                // Optional: configure password rules
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<ApplicationUser>>();

            // 🟢 5. Add Controllers
            builder.Services.AddControllers();

            // 🟢 6. Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 🟢 7. CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // 🟢 8. Middlewares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFiles(); // Optional: if you have HTML files

            app.Run();
        }
    }
}
