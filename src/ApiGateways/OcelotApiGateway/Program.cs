using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(hostingContext =>
    {
        builder.Configuration.AddJsonFile($"ocelot." +
            $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}" +
            $".json", true,true);

    });
builder.Host.ConfigureLogging(loggingbuilder =>
{
    loggingbuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();
});

builder.Services.AddOcelot().AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
});
await app.UseOcelot();


app.Run();
