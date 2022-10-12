using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Recommendation.API.DbContexts;
using Recommendation.API.Interfaces;
using Recommendation.API.Middlewares;
using Recommendation.API.Services;
using System.Numerics;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Recommendation API",
        Description = "An ASP.NET Core Web API for Recommendation Engine for e-commerce platform",       
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


//----------------------------- SQL Server Config ----------------------------------------------------------
builder.Services.AddDbContext<RecommendationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});
//----------------------------------------------------------------------------------------------------------


//----------------------------- DI config -------------------------------------------------------------------
builder.Services.AddScoped<IRecommendationDbContext>(provider => provider.GetService<RecommendationDbContext>());
builder.Services.AddScoped<IHistory, SHistory>();
builder.Services.AddScoped<IOrder, SOrder>();
builder.Services.AddScoped<IOrderItem, SOrderItem>();
builder.Services.AddScoped<IProduct, SProduct>();
builder.Services.AddSingleton<ICustomLogger, CustomConsoleLogger>();
//----------------------------------------------------------------------------------------------------------


//--------------------------automapper config---------------------------------------------------------------
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
//----------------------------------------------------------------------------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//-------------------------- custom middleware -------------------------------------------------------------
app.UseCustomException();
//----------------------------------------------------------------------------------------------------------

app.MapControllers();

app.Run();
