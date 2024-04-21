using ChatServer.Application.Common.Interfaces;
using ChatServer.Web.Services;
using Microsoft.Extensions.Configuration;
using ChatServer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ICurrentUserService, CurrentUser>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices();

var configuration = builder.Configuration;


builder.Services.AddInfrastructureServices(configuration);





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
