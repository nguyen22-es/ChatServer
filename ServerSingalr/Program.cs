using Application.Common.Interfaces;
using ChatServer.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using ServerSingalr.ChatHub;
using Microsoft.EntityFrameworkCore;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
              options.UseSqlServer(
                  configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
              .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>())
              );

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());


builder.Services.AddSignalR();
builder.Services.AddTransient<ChatHub>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
      // lưu chữ token khi được xác thực
      options.SaveToken = true;
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new TokenValidationParameters()
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = "ValidAudience",
          ValidIssuer = "ValidIssuer",
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Super_Secret_Key_ChatServer123456789"))
      };
      options.Events = new JwtBearerEvents
      {
          OnMessageReceived = context => {
              var accessToken = context.Request.Query["access_token"];
              var path = context.HttpContext.Request.Path;
              if (!string.IsNullOrEmpty(accessToken)
                  && path.StartsWithSegments("/SignalrHub"))
              {
                  context.Token = accessToken;
              }
              return Task.CompletedTask;
          }
      };
  });
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowCredentials()
    );
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{

    endpoints.MapHub<ChatHub>("/SignalrHub", optioins =>
    {
        optioins.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
    } 
    );
});
app.Run();
