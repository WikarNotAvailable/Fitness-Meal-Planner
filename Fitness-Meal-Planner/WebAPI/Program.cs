using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Additional;
using Domain.Interfaces;
using HealthChecks.UI.Client;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Reflection;
using WebAPI.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.InstallServicesInAssembly();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new HealthCheckResponse
        {
            status = report.Status.ToString(),
            healthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
            {
                component = x.Key,
                status = x.Value.Status.ToString(),
                description = x.Value.Description
            }),
            healthCheckDuration = report.TotalDuration
        };
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}); 
app.UseHealthChecks("/healthUI", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI();

app.Run();
