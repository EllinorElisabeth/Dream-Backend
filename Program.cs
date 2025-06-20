using Microsoft.EntityFrameworkCore;
using Dream.Data;
using Dream.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DreamDbContext>( options =>
options.UseSqlite( "Data Source=Dream.db" )); 

builder.Services.AddCors(
    options => {
        options.AddPolicy("AllowAnyOrigin",
        policies => 
        policies
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    }
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowAnyOrigin");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

// TEST DB-tilgang
app.MapGet("/testdb", async (DreamDbContext db) =>
{
    return await db.Thoughts.ToListAsync();
});

app.Run();