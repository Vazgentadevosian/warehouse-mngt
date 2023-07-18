using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;

using WRMNGT.Application.Handlers.Location.Queries;
using WRMNGT.Application.Locations.Interfaces;

using WRMNGT.Application.Locations.Services;
using WRMNGT.Application.Services;
using WRMNGT.Data.Database;
using WRMNGT.Domain.Commands.Location;
using WRMNGT.Domain.Constants;
using WRMNGT.Domain.Models.Response;
using WRMNGT.Domain.Queries.Location;
using WRMNGT.Infrastructure.Abstractions;
using WRMNGT.Infrastructure.Abstractions.Repository;
using WRMNGT.Infrastructure.Extensions.Services;
using WRMNGT.WebApi.Behaviours;

var builder = WebApplication.CreateBuilder(args);
IConfiguration _configuration = builder.Configuration;
IAssemblyIndicator _assemblyIndicator = new AssemblyIndicator();
var _connectionString = _configuration.GetDBConnectionString();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new LocationProfile());
    mc.AddProfile(new LocationCommandsProfile());
    mc.AddProfile(new LocationQueriesProfile());
});

var mapper = mapperConfig.CreateMapper();


builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(GetAssembly(AssemblyConstants.ApplicationName)));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ILocationService, LocationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddEFCore<WrMngtContext>(_connectionString);

// Add cores and olso add HealthCheck

builder.Services.AddMvc();

#warning Add CORS and also add HealthCheck

builder.Services.AddServices<WrMngtContext>();




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseDbTransactionMiddleware();

app.UseExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

Assembly GetAssembly(string name) => _assemblyIndicator.GetAssembly(name);
