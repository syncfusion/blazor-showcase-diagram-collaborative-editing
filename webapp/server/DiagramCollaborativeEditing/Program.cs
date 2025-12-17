using DiagramCollaboration.Components;
using SignalRServer.Hubs;
using SignalRServer.Services;
using StackExchange.Redis;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DetailedErrors = true;
    });
// Register services
// Redis Configuration
builder.Services.AddSingleton<IConnectionMultiplexer>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379,abortConnect=false";
    return ConnectionMultiplexer.Connect(connectionString);
});
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IDiagramService, DiagramService>();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
    options.MaximumParallelInvocationsPerClient = 2;
    options.HandshakeTimeout = TimeSpan.FromMinutes(1);
})
.AddJsonProtocol(options =>
{
    options.PayloadSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    options.PayloadSerializerOptions.WriteIndented = false;
    options.PayloadSerializerOptions.MaxDepth = 64;
    options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
    logging.SetMinimumLevel(LogLevel.Information);
});
builder.Services.AddSyncfusionBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapHub<DiagramHub>("/diagramHub").DisableAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseAntiforgery();

app.Run();
