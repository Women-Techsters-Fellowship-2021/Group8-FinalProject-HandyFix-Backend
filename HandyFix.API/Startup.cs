using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using HandyFix.BusinessLogic;
using HandyFix.DataAcccess;
using HandyFix.Models;
using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;


using System.Text;

namespace HandyFix.API
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
            services.AddCors(o =>
            {
                o.AddPolicy("ReactPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); 

                    });
            });

            services.AddScoped<IAuthentication, Authentication>();
            services.AddScoped<IUserSystem, UserSystem>();
            //services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddDbContext<HandyFixDBContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:DbConnection"]);
            });


            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HandyFixDBContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 7;
                options.User.AllowedUserNameCharacters = null;
            });


            services.AddControllers();
            /* services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidAudience = Configuration["JWTSettings:Audience"],
                       ValidIssuer = Configuration["JWTSettings:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes(Configuration["JWTSettings:SecretKey"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });
 */
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HandyFix.API", Version = "v1" });
                /* c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                }); */
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, HandyFixDBContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Seeder.Seed(userManager, context).Wait();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HandyFix.API v1"));

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
