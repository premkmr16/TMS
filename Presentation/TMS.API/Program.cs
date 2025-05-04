using Microsoft.EntityFrameworkCore;
using Serilog;
using TMS.API.Endpoints;
using TMS.API.Middlewares;
using TMS.Application.Common;
using TMS.Application.Common.Helpers;
using TMS.Application.Common.Mapping;
using TMS.Application.Common.Validation;
using TMS.Application.ConnectionFactory;
using TMS.Application.Features.Employees.ValidationHelpers;
using TMS.Application.Repositories.EmployeeRepository;
using TMS.Infrastructure.ConnectionFactory;
using TMS.Infrastructure.Persistence.Context;
using TMS.Infrastructure.Persistence.Repositories.EmployeeRepository;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

builder.Services.AddDbContextPool<TmsDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();
builder.Services.AddSingleton<IEmployeeReadRepository, EmployeeReadRepository>();
builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddTransient<IEmployeeValidator, EmployeeValidator>();

builder.Services.AddTransient<IExcelHelper, ExcelHelper>();

builder.Services.ConfigureMapster();
builder.Services.RegisterValidators();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(MediatorRegister).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  

app.MapToEmployeeTypeEndpoints();
app.MapToEmployeeEndpoints();

app.UseMiddleware<ApiResponseMiddleware>();

app.Run();