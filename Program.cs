using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using TarefaProcessorApi.Data;
using TarefaProcessorApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurações
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

builder.Services.AddScoped<TarefaProcessor>();

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
