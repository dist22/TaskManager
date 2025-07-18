using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data.Context;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.PasswordHasher;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.ApplicationProfile;
using TaskManager.Application.Interfaces.JwtProvider;
using TaskManager.Infrastructure.Data.Configuration;
using TaskManager.Infrastructure.JwtProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.UseInlineDefinitionsForEnums();
    cfg.SchemaGeneratorOptions.UseAllOfForInheritance = false;
    cfg.SchemaGeneratorOptions.UseAllOfToExtendReferenceSchemas = false;

});

builder.Services.AddDbContext<DataContextEf>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));


builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITaskService, TaskServices>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserTaskService, UserTaskService>();
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ApplicationProfile>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();