using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContextEf>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();