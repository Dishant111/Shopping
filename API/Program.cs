using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionMiddleware();
app.UseStatusCodePagesWithReExecute("/error/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<StoreContext>();
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedData(context);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occured while running migration!");
        throw;
    }
}

app.Run();
