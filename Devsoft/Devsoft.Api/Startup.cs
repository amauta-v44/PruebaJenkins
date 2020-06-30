using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devsoft.Api.Configuration;
using Devsoft.Api.Entities;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Repositories;
using Devsoft.Api.Repositories.RepositoryImpl;
using Devsoft.Api.Services;
using Devsoft.Api.Services.ServicesImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Devsoft.Api
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

			// Database configuration
			services.AddDbContext<DevsoftContext>(options =>
			{
				options.UseNpgsql(Configuration.GetConnectionString("PostgresqlConnection"));
			});


			// Repositories
			services.AddScoped<IFoodGroupRepository, FoodGroupRepositoryImpl>();
			services.AddScoped<IFoodRepository, FoodRepositoryImpl>();
			services.AddScoped<IPublicationsRepository, PublicationsRepositoryImpl>();
			services.AddScoped<IUsersRepository, UsersRepositoryImpl>();


			// Dependency Injection Configuration
			services.AddScoped<IUsersService, UsersServiceImpl>();
			services.AddScoped<IFoodService, FoodServiceImpl>();
			services.AddScoped<IFoodGroupService, FoodGroupServiceImpl>();
			services.AddScoped<IPublicationService, PublicationServiceImpl>();
			services.AddScoped<IAuthService, AuthServiceImpl>();


			// Exception filters
			services.AddControllers(options =>
				options.Filters.Add(new HttpResponseExceptionFilter()));


			// Object configurations
			IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// JWT Configuration
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});

			// Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v0", new OpenApiInfo()
				{
					Version = "v0",
					Title = "Experimentos API",
					Description = "La api para el curso de Diseño de Experimentos ISW"
				});

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme."
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
						new string[] { }
					}
				});
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			// Handle exceptions
			app.UseExceptionHandler(a => a.Run(async context =>
			{
				var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
				var exception = exceptionHandlerPathFeature.Error;

				var result = JsonConvert.SerializeObject(new
				{
					error = exception.Message
				});
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsync(result);
			}));

			// Swagger
			app.UseSwagger();

			app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v0/swagger.json", "Devsoft API v0"); });

			// Cors policy
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader()
			);


			app.UseRouting();

			// Jwt Authentication
			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}

/*
 * "PostgresqlConnection": "Host=ec2-52-87-58-157.compute-1.amazonaws.com;Port=5432;Username=cpofexiyjwtfiy;Password=4b536677df10ba7074aac7797553bc66211db826a642af9f217aeeb007e7b712;Database=db74hm0kaq7iku;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;"
 */