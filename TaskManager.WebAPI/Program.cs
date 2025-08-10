using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using TaskManager.Infrastructure.Data.Context;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Repositories;
using TaskManager.Infrastructure.PasswordHasher;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Application.ApplicationProfile;
using TaskManager.Application.Interfaces.JwtProvider;
using TaskManager.Application.Interfaces.PasswordHasher;
using TaskManager.Application.Interfaces.Repositories;
using TaskManager.Infrastructure.Data.Configuration;
using TaskManager.Infrastructure.JwtProvider;
using TaskManager.WebAPI.Filters;
using TaskManager.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager API", Version = "v1" });
    cfg.UseInlineDefinitionsForEnums();
    cfg.SchemaGeneratorOptions.UseAllOfForInheritance = false;
    cfg.SchemaGeneratorOptions.UseAllOfToExtendReferenceSchemas = false;
    cfg.SchemaFilter<EnumSchemaFilter>();

});

builder.Services.AddDbContext<DataContextEf>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddAuthentication(builder.Configuration);

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

builder.Host.UseSerilog((context, cfg) =>
{
    cfg
        .WriteTo.Console()
        .WriteTo.File("log_info.txt",restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
        .WriteTo.File("log_error.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
        .Enrich.FromLogContext();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandler>();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContextEf>();
    dbContext.Database.Migrate();
}

app.Run();