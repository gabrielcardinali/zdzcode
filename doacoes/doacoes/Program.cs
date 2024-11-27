using doacoes.Data.Context;
using Microsoft.EntityFrameworkCore;
using doacoes.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicione o AppDbContext ao container de serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));

// Adicione os serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicione os serviços dos controladores
builder.Services.AddControllers();

var app = builder.Build();

// Configure o Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure o pipeline HTTP
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();