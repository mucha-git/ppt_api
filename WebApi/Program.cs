﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApi.Authorization;
using WebApi.Factories;
using WebApi.Helpers;
using WebApi.Models.Years;
using WebApi.Repositories;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    //services.AddDbContext<DataContext>();
    IServiceCollection serviceCollection = builder.Services.AddDbContext<DataContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase"))
                              .EnableSensitiveDataLogging(), ServiceLifetime.Transient);

    //services.AddDbContext<DataContextFirebird>(o => o.UseFirebird(builder.Configuration.GetConnectionString("WebApiDatabaseFirebird"))
    //                          .EnableSensitiveDataLogging(), ServiceLifetime.Transient);
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    services.AddCors();
    services.AddSignalR();
    services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "Redis_pielgrzymka_";
    });
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }).AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddSwaggerGen(c =>
    {
        OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
        {
            Name = "Bearer",
            BearerFormat = "JWT",
            Scheme = "bearer",
            Description = "Specify the authorization token.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
        };
        c.AddSecurityDefinition("jwt_auth", securityDefinition);

        // Make sure swagger UI requires a Bearer token specified
        OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Id = "jwt_auth",
                Type = ReferenceType.SecurityScheme
            }
        };
        OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityScheme, new string[] { }},
                };
        c.AddSecurityRequirement(securityRequirements);
        c.OperationFilter<AuthResponsesOperationFilter>();
    });
    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    services.AddHttpContextAccessor();
    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IAccount, AccountService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<IYearsService, YearsService>();
    services.AddScoped<IViewsService, ViewsService>();
    services.AddScoped<IElementsService, ElementsService>();
    services.AddScoped<IMapsService, MapsService>();
    services.AddScoped<IPilgrimagesService, PilgrimagesService>();
    services.AddScoped<IMapPinsService, MapPinsService>();
    services.AddScoped<IOneSignalService, OneSignalService>();
    services.AddScoped<IValidations, Validations>();

    // Repositories
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddTransient<IYearsRepository, YearsRepository>();
    services.AddTransient<IViewsRepository, ViewsRepository>();
    services.AddTransient<IElementsRepository, ElementsRepository>();
    services.AddTransient<IMapsRepository, MapsRepository>();
    services.AddTransient<IPilgrimagesRepository, PilgrimagesRepository>();
    services.AddTransient<IMapPinsRepository, MapPinsRepository>();

    // Factories
    services.AddScoped<IViewsFactory, ViewsFactory>();
    services.AddScoped<IElementsFactory, ElementsFactory>();
    services.AddScoped<IMapsFactory, MapsFactory>();
    services.AddScoped<IPilgrimagesFactory, PilgrimagesFactory>();
    services.AddScoped<IMapPinsFactory, MapPinsFactory>();
    services.AddScoped<IYearsFactory, YearsFactory>();
}

var app = builder.Build();
//app.UseHttpsRedirection();
// migrate any database changes on startup (includes initial db creation)
//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();    
//    dataContext.Database.Migrate();
//}

// configure HTTP request pipeline
{
    // generated swagger json and swagger ui middleware
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Sign-up and Verification API"));

    // global cors policy
    app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();
    //app.UseEndpoints(x =>
    //{
    //    x.MapControllers();
    //    x.MapHub<FrontClientHub>("/hubs/frontClient");

    //});

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();

}
app.Run();
//app.Run("http://0.0.0.0:4000");