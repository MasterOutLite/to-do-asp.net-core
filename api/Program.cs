using api.Middleware;
using api.OptionSetup;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddPresentation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter Jwt token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List<string>()
        }
    });
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureService(builder.Configuration);


builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionSetup>();
builder.Services.ConfigureOptions<AuthorizationOptionsSetup>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(build => build.WithOrigins("https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());

    cors.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins(
                "https://localhost:4200",
                "http://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();