using Cars.API.Data.DbDataSource.Dapper;
using Cars.API.Data.Interfaces;
using Cars.API.Models;
using Cars.API.Models.AutomapperProfiles;
using Cars.API.Models.DTOs;
using Cars.API.ModelValidators;
using Cars.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace Cars.API;

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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
        ConfigureAuthService(services);
        services.AddHttpContextAccessor();
        services.AddTransient<IValidator<CarDTO>, CarDTOValidator>();
        services.AddTransient<IAsyncRepository<Car>, DapperCarsRepository>();
        services.AddAutoMapper(typeof(CarMapperConfiguration));
        services.AddScoped<ICarsService, CarsService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private void ConfigureAuthService(IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        string? identityUrl = Configuration.GetValue<string>("ExternalIdentityUrl");

        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = "Cars.API";
                });
    }
}