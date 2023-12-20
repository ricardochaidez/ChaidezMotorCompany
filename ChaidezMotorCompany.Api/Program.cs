using System.Runtime.CompilerServices;
using ChaidezMotorCompany.Domain;
using ChaidezMotorCompany.Infrastructure;
using ChaidezMotorCompany.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Dependecy Injection
builder.Services.AddScoped<ICarDomain, CarDomain>();
builder.Services.AddScoped<ICarRepository, CarRepository>();

//Output Cache
builder.Services.AddRedisOutputCache(configuration);
builder.Services.AddResponseCacheProfiles(configuration);

builder.Services.AddHealthChecks(configuration);

var app = builder.Build();

app.UseHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//Output Cache
app.UseOutputCache();
app.UseResponseCaching();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();