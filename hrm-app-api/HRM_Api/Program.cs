using HrmApp.Application.Features.Handlers;
using HrmApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddOpenApi();
builder.Services.AddDbContext<HanaHrmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// FIX: Register IHanaHrmContext interface with its implementation
builder.Services.AddScoped<IHanaHrmContext, HanaHrmContext>();

// Register MediatR from multiple handler assemblies
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetEmployeeByIdQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllEmployeesQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteEmployeeCommandHandler).Assembly);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder
            .WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost:3070")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

var app = builder.Build();
app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();