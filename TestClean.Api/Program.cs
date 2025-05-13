using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TestClean.Api.Filters;
using TestClean.Api.Middleware;
using TestClean.Application;
using TestClean.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add filter to all controllers
builder.Services.AddControllers(opt => opt.Filters.Add<ErrorHandlingFilterAttributes>());

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();


//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

