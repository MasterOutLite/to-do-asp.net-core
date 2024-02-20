using System.Text;
using api.OptionSetup;
using Application;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization();

//builder.Services.AddAuthorization(options => options.DefaultPolicy =
//    new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
//        .RequireAuthenticatedUser()
//        .Build());

var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();