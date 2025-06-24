using Microsoft.EntityFrameworkCore;
using Dream.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

builder.Services.AddDbContext<DreamDbContext>(options =>
    //options.UseSqlite( "Data Source=Dream.db" ));
    options.UseNpgsql(connectionString)
);

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

var port = Environment.GetEnvironmentVariable("PORT");
if (port != null)
{
    app.Urls.Add($"http://*:{port}");
}

app.Run();