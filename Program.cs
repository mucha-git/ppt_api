using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApi.Authorization;
using WebApi.Factories;
using WebApi.Helpers;
using WebApi.Repositories;
using WebApi.Services;
using WebApi.SignalR.FrontClientHub;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    //services.AddDbContext<DataContext>();
    IServiceCollection serviceCollection = builder.Services.AddDbContext<DataContext>(o => o.UseFirebird(builder.Configuration.GetConnectionString("WebApiDatabase"))
                              .EnableSensitiveDataLogging(), ServiceLifetime.Transient);
    services.AddCors();
    services.AddSignalR();
    services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IEmailService, EmailService>();
    services.AddScoped<ITradeService, TradeService>();
    services.AddScoped<IFrontClientSignalRService, FrontClientSignalRService>();


    // Repositories
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped<ITradeRepository, TradeRepository>();

    // Factories
    services.AddScoped<ITradeFactory, TradeFactory>();

}

var app = builder.Build();

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
    app.MapHub<FrontClientHub>("/hubs/frontClient");

}
app.Run();
//app.Run("http://0.0.0.0:4000");