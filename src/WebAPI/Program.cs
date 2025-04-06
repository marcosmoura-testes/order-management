using Application.Interfaces;
using Application.UseCases;
using Domain.UoW;
using Infrastructure;
using Infrastructure.UoW;
using Microsoft.OpenApi.Models;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var Configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddInfraDataSetup(Configuration);
builder.Services.AddDbContext<DefaultDbContext>();

builder.Services.AddScoped<ICreateDealer, CreateDealerUseCase>();
builder.Services.AddScoped<IReadDealer, ReadDealerUseCase>();
builder.Services.AddScoped<IUpdateDealer, UpdateDealerUseCase>();
builder.Services.AddScoped<IDeleteDealer, DeleteDealerUseCase>();
builder.Services.AddScoped<ISendOrderUseCase, SendOrderUseCase>();
builder.Services.AddScoped<IRequestOrderUseCase, RequestOrderUseCase>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseMiddleware<ProblemDetailsMiddleware>();
app.MapControllers();

app.Run();
