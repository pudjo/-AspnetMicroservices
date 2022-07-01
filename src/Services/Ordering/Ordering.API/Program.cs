using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Ordering.API.Extensions;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase();

var scope = app.Services.CreateScope();

OrderContext ctx = scope.ServiceProvider.GetRequiredService<OrderContext>();

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());
ILogger<OrderContextSeed> logger = loggerFactory.CreateLogger<OrderContextSeed>();


OrderContextSeed.SeedAsync(ctx, (ILogger<OrderContextSeed>)logger);




app.Run();
