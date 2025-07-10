using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data.Context;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.ApplicationProfile;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContextEf>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITaskService, TaskServices>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApplicationProfile>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();