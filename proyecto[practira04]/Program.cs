using Microsoft.EntityFrameworkCore;
using ServicioDLL.Data.Models;
using ServicioDLL.Data.Repositories; // Asegúrate de incluir el espacio de nombres de tu repositorio

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ServiciosDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro del repositorio
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();

