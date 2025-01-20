using AVIVA.Application;
using AVIVA.Infrastructure.Persistence;
using AVIVA.Infrastructure.Persistence.Contexts;
using AVIVA.Infrastructure.Shared;
using AVIVA.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using AVIVA.Application.Interfaces;
using AVIVA.Infrastructure.Shared.Services;
using Microsoft.EntityFrameworkCore;
using AVIVA.Infrastructure.Persistence.Data;
using System.Collections.Generic;
using AVIVA.Application.Models;

try
{
    var builder = WebApplication.CreateBuilder(args);
    // load up serilog configuration
    Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
    builder.Host.UseSerilog(Log.Logger);

    Log.Information("Application startup services registration");

    builder.Services.Configure<List<PaymentProviderConfig>>(builder.Configuration.GetSection("PaymentProviderConfig"));

    builder.Services.AddApplicationLayer();
    // Current tenant service with scoped lifetime (created per each request)
    builder.Services.AddTransient<IDateTimeService, DateTimeService>();


    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDb"));
    builder.Services.AddPersistenceInfrastructure(builder.Configuration);
    builder.Services.AddSharedInfrastructure(builder.Configuration);
    builder.Services.AddSwaggerExtension();
    builder.Services.AddControllersExtension();

    // Configura PaymentProviderConfig desde appsettings.json


    // CORS
    builder.Services.AddCorsExtension();
    builder.Services.AddHealthChecks();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins", builder =>
            builder.WithOrigins("http://localhost:4200", "http://otro-origen.com")
                   .AllowAnyMethod()
                   .AllowAnyHeader());
    });
    //API Security
    builder.Services.AddJWTAuthentication(builder.Configuration);
    builder.Services.AddAuthorizationPolicies(builder.Configuration);
    // API version
    builder.Services.AddApiVersioningExtension();
    // API explorer
    builder.Services.AddMvcCore()
        .AddApiExplorer();
    // API explorer version
    builder.Services.AddVersionedApiExplorerExtension();
    var app = builder.Build();

    Log.Information("Application startup middleware registration");

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        // for quick database (usually for prototype)
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // use context
            dbContext.Database.EnsureCreated();
            DataSeeder.SeedData(dbContext);
        }


    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    // Add this line; you'll need `using Serilog;` up the top, too
    app.UseSerilogRequestLogging();
    // app.UseHttpsRedirection();
    app.UseRouting();
    //Enable CORS
    app.UseCors("AllowAll");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseStaticFiles();
    app.UseStaticFiles(new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
        RequestPath = new PathString("/Uploads")
    });
    app.UseSwaggerExtension();
    //app.UseErrorHandlingMiddleware();
    app.UseHealthChecks("/health");
    app.MapControllers();

    Log.Information("Application Starting");

    app.Run();
}
catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
    Log.Error(ex, "An error occurred starting the application");
    Console.Error.WriteLine($"An error occurred: {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}
