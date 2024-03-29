using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(op => 
{
    op.UseSqlServer(builder.Configuration["ConnectionStrings"]);
});
builder.Services.AddScoped<IProductRepository,ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope =app.Services.CreateScope())
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
        logger.LogError(ex,"An error occured while running migration!");
        throw;
    }
}

app.Run();
