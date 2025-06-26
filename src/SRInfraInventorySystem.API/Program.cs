using Microsoft.EntityFrameworkCore;
using SRInfraInventorySystem.Infrastructure.Persistence;
using SRInfraInventorySystem.Repository.Interfaces;
using SRInfraInventorySystem.Repository.Implementations;
using SRInfraInventorySystem.Application.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Configuration - Using SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper Configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Repository Registrations
builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();
builder.Services.AddScoped<IServerUsageHistoryRepository, ServerUsageHistoryRepository>();
builder.Services.AddScoped<IAccessLogRepository, AccessLogRepository>();

// Service Registrations
builder.Services.AddScoped<IServerService, ServerService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IServerUsageHistoryService, ServerUsageHistoryService>();
builder.Services.AddScoped<IAccessLogService, AccessLogService>();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SR Infrastructure Inventory System API",
        Version = "v1.0",
        Description = "SR Altyapı Envanter Yönetim Sistemi API'si - Sunucu, uygulama, veritabanı ve personel yönetimi için RESTful API",
        Contact = new OpenApiContact
        {
            Name = "SR Infrastructure Team",
            Email = "infrastructure@company.com",
            Url = new Uri("https://company.com/infrastructure")
        }
    });

    // Swagger anotasyonlarını etkinleştir
    c.EnableAnnotations();
});

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SR Infrastructure Inventory System API v1.0");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "SR Infrastructure Inventory System API Documentation";
        
        // Swagger UI'da collapse (daraltılmış) görünümü etkinleştir
        c.DefaultModelsExpandDepth(-1); // Model şemalarını gizle
        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
        c.DisplayRequestDuration();
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Tüm endpoint'leri daralt
        c.ShowExtensions();
        c.ShowCommonExtensions();
    });
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Health Check endpoint
app.MapGet("/health", () => Results.Ok(new { 
    Status = "Healthy", 
    Timestamp = DateTime.UtcNow, 
    Message = "SR Infrastructure Inventory System API is working!",
    Version = "1.0.0"
}));

app.Run();