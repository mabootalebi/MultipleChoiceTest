using API.Extensions;
using API.Middlewares;
using Domains.Entities;
using Infrastructure.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ResolveIoC();

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("appsettings.json");
var configuration = configBuilder.Build();

builder.Services.AddDbContextPool<ApplicationDbContext>(x =>
{
    var connectionString = configuration.GetConnectionString("DataBase");
    x.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure"));
});

// abstract it

// For Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

//abstract it
builder.Host.UseSerilog((hostContext, services, config) =>
{
    config
        .MinimumLevel.Debug()
        .WriteTo.Logger(lc =>
            lc.Filter.ByIncludingOnly(t => t.Level == LogEventLevel.Error || t.Level == LogEventLevel.Fatal)
            .WriteTo.File(path: "/Logs/Errors/log.txt",                      
                          restrictedToMinimumLevel: LogEventLevel.Error,
                          rollingInterval: RollingInterval.Day))

        .WriteTo.File(path: "/Logs/All/log.txt",
              restrictedToMinimumLevel: LogEventLevel.Debug,
              rollingInterval: RollingInterval.Day);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.UseExceptionHandlingMiddleware();

app.Run();
