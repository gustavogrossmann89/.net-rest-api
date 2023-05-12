using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Common.Repositories.Interfaces;
using TrilhaApiDesafio.Common.Repositories;
using TrilhaApiDesafio.Common.Services.Interfaces;
using TrilhaApiDesafio.Common.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<OrganizadorContext>(options =>
    options.UseInMemoryDatabase("TestDb"));

// Services
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddScoped<IPessoaService, PessoaService>();

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
