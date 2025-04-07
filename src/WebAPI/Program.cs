using System.Reflection;
using Application.Interfaces;
using Application.UseCases;
using Domain.UoW;
using Infrastructure;
using Infrastructure.UoW;
using Microsoft.OpenApi.Models;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddInfraDataSetup(Configuration);
builder.Services.AddDbContext<DefaultDbContext>();

builder.Services.AddScoped<ICreateDealer, CreateDealerUseCase>();
builder.Services.AddScoped<IReadDealer, ReadDealerUseCase>();
builder.Services.AddScoped<IUpdateDealer, UpdateDealerUseCase>();
builder.Services.AddScoped<IDeleteDealer, DeleteDealerUseCase>();
builder.Services.AddScoped<ISendOrderUseCase, SendOrderUseCase>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Managerment Dealer Order", Version = "v1" });
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Managerment Dealer Order V1");
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware<ProblemDetailsMiddleware>();
app.MapControllers();

app.Run();
