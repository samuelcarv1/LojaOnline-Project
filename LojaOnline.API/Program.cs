using System.Data;
using System.Data.SqlClient;
using LojaOnline.Application.Queries.GetProduct;
using LojaOnline.Domain.Repositories;
using LojaOnline.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// Services
builder.Services.AddHttpClient();
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(GetProductQuery).Assembly); });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("VendasOnlineCs")));

// Build
var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (error is KeyNotFoundException)
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        else
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(new { message = error?.Message });
    });
});

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseAuthentication(); // comentar por enquanto
app.UseAuthorization();

app.MapControllers();

app.Run();
