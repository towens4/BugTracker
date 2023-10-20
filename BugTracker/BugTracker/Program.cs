using BugTrackerCore.Interfaces;
using BugTrackerCore.Models;
using BugTrackerCore.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "BugTrackerOrigin";
var ClientUrl = "https://localhost:7211";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddHubOptions<BugTrackerHub>(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BugTrackerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BugTrackerDB"));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(ClientUrl).AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials();
    });
    /*options.AddPolicy(MyAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins(ClientUrl)
        .AllowAnyHeader()
        .WithMethods("GET", "POST")
        .AllowCredentials();
    });*/
        
});

//builder.Services.AddSingleton<IList<string>, List<string>>();

builder.Services.AddScoped<IDbRepository, BugTrackerDbRepository>();
builder.Services.AddSingleton<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<HubConnectionManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();
app.UseCors();
app.UseAuthorization();
//app.MapControllers();
app.UseEndpoints(route =>
{
    route.MapControllers();
    route.MapHub<BugTrackerHub>("/BugTrackerHub");
});
//app.MapHub<BugTrackerHub>("/BugTrackerHub");
app.Run();
