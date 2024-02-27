using api.Middleware;
using api.OptionSetup;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddPresentation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureService(builder.Configuration);


builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionSetup>();
builder.Services.ConfigureOptions<AuthorizationOptionsSetup>();
builder.Services.ConfigureOptions<SwaggerOptionSetup>();
builder.Services.ConfigureOptions<CorsOptionSetup>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddCors();

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

app.UseCors();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();