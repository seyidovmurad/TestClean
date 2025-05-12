using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TestClean.Api.Middleware;
using TestClean.Application;
using TestClean.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();


app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();

