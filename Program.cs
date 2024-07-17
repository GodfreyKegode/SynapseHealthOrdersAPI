using Microsoft.EntityFrameworkCore;
using SynapseHealthOrderMonitorAPI.Data.Repositories;
using SynapseHealthOrderMonitorAPI.Models;
using SynapseHealthOrderMonitorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// var loggerFactory = LoggerFactory.Create( builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));

// builder.Logging.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug);

builder.Services.AddLogging();
builder.Services.AddHttpClient();

// Register services to the DI container.
builder.Services.AddScoped<IOrderMonitorService, OrderMonitorService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IOrderMonitorRepo, OrderMonitorRepo>(); 
builder.Services.AddScoped<INotificationRepo, NotificationRepo>();

builder.Services.AddControllers();

// Register DbContext to the DI container
builder.Services.AddDbContext<OrderMonitorContext>(opt =>
    opt.UseInMemoryDatabase("Orders"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
